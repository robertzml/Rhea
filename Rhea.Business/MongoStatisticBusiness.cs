using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data.Estate;
using Rhea.Data.Server;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;

namespace Rhea.Business
{
    /// <summary>
    /// 统计业务类
    /// </summary>
    public class MongoStatisticBusiness : IStatisticBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);       
        #endregion //Field

        #region Method
        /// <summary>
        /// 根据使用类型得到楼宇建筑面积
        /// </summary>
        /// <param name="useType">楼宇使用类型</param>
        /// <returns></returns>
        public double GetBuildingAreaByType(int useType)
        {
            IBuildingBusiness buildingService = new MongoBuildingBusiness();
            double result = Convert.ToDouble(buildingService.GetList().Where(r => r.UseType == useType).Sum(r => r.BuildArea));
            result = Math.Round(result, 3);

            return result;
        }

        /// <summary>
        /// 获取部门二级分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="functionCodes">功能编码列表</param>
        public List<DepartmentSecondClassifyAreaModel> GetSecondClassifyArea(int departmentId, int firstCode, List<RoomFunctionCode> functionCodes)
        {
            BsonDocument[] pipeline = {
                new BsonDocument { 
                    { "$match", new BsonDocument {
                        { "departmentId", departmentId },
                        { "function.firstCode", firstCode }
                    }}
                },
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "$function.secondCode" },
                        { "area", new BsonDocument {
                            { "$sum", "$usableArea" }
                        }},
                        { "roomCount", new BsonDocument {
                            { "$sum", 1 }
                        }}
                    }}
                }
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);

            List<DepartmentSecondClassifyAreaModel> data = new List<DepartmentSecondClassifyAreaModel>();
            var function = functionCodes.Where(r => r.FirstCode == firstCode);
            foreach (var f in function)
            {
                BsonDocument doc = result.ResultDocuments.SingleOrDefault(r => r["_id"].AsInt32 == f.SecondCode);

                DepartmentSecondClassifyAreaModel model = new DepartmentSecondClassifyAreaModel
                {
                    FunctionFirstCode = firstCode,
                    FunctionSecondCode = f.SecondCode,
                    FunctionProperty = f.FunctionProperty,

                };
                if (doc != null)
                {
                    model.Area = Math.Round(doc["area"].AsDouble, 3);
                    model.RoomCount = doc["roomCount"].AsInt32;
                }
                else
                {
                    model.Area = 0.0;
                    model.RoomCount = 0;
                }

                data.Add(model);
            }

            data = data.OrderByDescending(r => r.Area).ToList();
            return data;
        }

        /// <summary>
        /// 获取部门一级级分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="title">编码名称</param>
        /// <param name="functionCodes">功能编码列表</param>
        /// <returns></returns>
        public DepartmentFirstClassifyAreaModel GetFirstClassifyArea(int departmentId, int firstCode, string title, List<RoomFunctionCode> functionCodes)
        {
            DepartmentFirstClassifyAreaModel m1 = new DepartmentFirstClassifyAreaModel();
            m1.Title = title;
            m1.FunctionFirstCode = firstCode;
            m1.SecondClassify = GetSecondClassifyArea(departmentId, firstCode, functionCodes);
            m1.Area = Math.Round(m1.SecondClassify.Sum(r => r.Area), 3);
            m1.RoomCount = m1.SecondClassify.Sum(r => r.RoomCount);

            return m1;
        }

        /// <summary>
        /// 获取部门分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="sortByFirstArea">是否按一级分类排序</param>
        /// <returns></returns>
        public DepartmentClassifyAreaModel GetDepartmentClassifyArea(int departmentId, bool sortByFirstArea = true)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            Department department = departmentBusiness.Get(departmentId);

            //get codes
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var functionCodes = roomBusiness.GetFunctionCodeList();

            DepartmentClassifyAreaModel data = new DepartmentClassifyAreaModel
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name
            };

            data.FirstClassify = new List<DepartmentFirstClassifyAreaModel>();

            data.FirstClassify.Add(GetFirstClassifyArea(departmentId, 1, "办公用房", functionCodes));
            data.FirstClassify.Add(GetFirstClassifyArea(departmentId, 2, "教学用房", functionCodes));
            data.FirstClassify.Add(GetFirstClassifyArea(departmentId, 3, "实验用房", functionCodes));
            data.FirstClassify.Add(GetFirstClassifyArea(departmentId, 4, "科研用房", functionCodes));

            if (sortByFirstArea)
                data.FirstClassify = data.FirstClassify.OrderByDescending(r => r.Area).ToList();

            data.TotalArea = data.FirstClassify.Sum(r => r.Area);

            return data;
        }

        /// <summary>
        /// 获取部门总面积模型
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public DepartmentTotalAreaModel GetDepartmentTotalArea(int departmentId)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            Department department = departmentBusiness.Get(departmentId);

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            List<Building> buildings = buildingBusiness.GetListByDepartment(departmentId);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();

            DepartmentTotalAreaModel data = new DepartmentTotalAreaModel();
            data.DepartmentId = departmentId;
            data.DepartmentName = department.Name;
            data.BuildingArea = new List<DepartmentBuildingAreaModel>();

            //分楼宇统计面积
            foreach (var building in buildings)
            {
                var rooms = roomBusiness.GetListByDepartment(departmentId, building.Id);

                DepartmentBuildingAreaModel model = new DepartmentBuildingAreaModel();
                model.BuildingId = building.Id;
                model.BuildingName = building.Name;
                model.RoomCount = rooms.Count();
                model.UsableArea = Math.Round(Convert.ToDouble(rooms.Sum(r => r.UsableArea)), RheaConstant.AreaDecimalDigits);

                data.BuildingArea.Add(model);
            }

            data.TotalRoomCount = data.BuildingArea.Sum(r => r.RoomCount);
            data.UsableArea = data.BuildingArea.Sum(r => r.UsableArea);

            return data;
        }
        #endregion //Method
    }
}
