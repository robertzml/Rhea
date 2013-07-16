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
    public class MongoStatisticService : IStatisticService
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);       
        #endregion //Field  

        #region Old
        /// <summary>
        /// 获取学院楼宇面积
        /// </summary>
        /// <param name="departmentId">学院ID</param>
        /// <param name="buildingList">楼宇列表</param>
        /// <returns></returns>
        public List<BuildingAreaModel> GetBuildingArea(int departmentId, List<Building> buildingList)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "department.id", departmentId }
                    }}
                },
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "$building.id" },
                        { "area", new BsonDocument {
                            { "$sum", "$usableArea" }
                        }},
                        { "count", new BsonDocument {
                            { "$sum", 1 }
                        }}
                    }
                }}
            };

            AggregateResult result = this.context.Aggregate("room", pipeline);

            List<BuildingAreaModel> data = new List<BuildingAreaModel>();
            foreach (var doc in result.ResultDocuments)
            {
                Building building = buildingList.Single(r => r.Id == doc["_id"].AsInt32);

                BuildingAreaModel model = new BuildingAreaModel
                {
                    Id = building.Id,
                    BuildingName = building.Name,
                    Area = Math.Round(doc["area"].AsDouble, 3),
                    RoomCount = doc["count"].AsInt32
                };

                data.Add(model);
            }

            return data;
        }

        /// <summary>
        /// 得到部门分楼宇面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public CollegeBuildingAreaModel GetCollegeBuildingArea(int departmentId)
        {
            /*IDepartmentService departmentService = new MongoDepartmentService();
            Department department = departmentService.Get(departmentId);

            //get buildings        
            IBuildingService buildingService = new MongoBuildingService();
            var buildings = buildingService.GetList();

            List<BuildingAreaModel> model = GetBuildingArea(department.Id, buildings);

            CollegeBuildingAreaModel c = new CollegeBuildingAreaModel
            {
                Id = department.Id,
                CollegeName = department.Name,
                BuildingArea = model
            };

            return c;*/
            throw new NotImplementedException();
        }

        
        #endregion //Old

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
        /// <returns></returns>
        public DepartmentClassifyAreaModel GetDepartmentClassifyArea(int departmentId)
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

            data.FirstClassify = data.FirstClassify.OrderByDescending(r => r.Area).ToList();

            return data;
        }        
        #endregion //Method
    }
}
