using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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

        #region Function
        /// <summary>
        /// 得到楼群房间二级分类
        /// </summary>
        /// <param name="buildingIds">楼群下属楼宇ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="functionCodes">功能编码列表</param>
        /// <returns></returns>
        private List<RoomSecondClassifyAreaModel> GetSecondClassifyArea(IEnumerable<int> buildingIds, int firstCode, List<RoomFunctionCode> functionCodes)
        {
            BsonDocument[] pipeline = {
                new BsonDocument { 
                    { "$match", new BsonDocument {
                        { "buildingId", new BsonDocument {
                            { "$in", new BsonArray(buildingIds)  }
                        }},
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

            List<RoomSecondClassifyAreaModel> data = new List<RoomSecondClassifyAreaModel>();
            var function = functionCodes.Where(r => r.FirstCode == firstCode);
            foreach (var f in function)
            {
                BsonDocument doc = result.ResultDocuments.SingleOrDefault(r => r["_id"].AsInt32 == f.SecondCode);

                RoomSecondClassifyAreaModel model = new RoomSecondClassifyAreaModel
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

            return data;
        }

        /// <summary>
        /// 得到楼群房间一级分类面积
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="title">编码名称</param>
        /// <param name="functionCodes">功能编码列表</param>
        /// <param name="sort">是否排序</param>
        /// <returns></returns>
        private RoomFirstClassifyAreaModel GetBuildingGroupFirstClassifyArea(int buildingGroupId, int firstCode, string title, List<RoomFunctionCode> functionCodes, bool sort)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetListByBuildingGroup(buildingGroupId);
            var bids = buildings.Select(r => r.Id);

            RoomFirstClassifyAreaModel m1 = new RoomFirstClassifyAreaModel();
            m1.Title = title;
            m1.FunctionFirstCode = firstCode;
            m1.SecondClassify = GetSecondClassifyArea(bids, firstCode, functionCodes);
            m1.Area = Math.Round(m1.SecondClassify.Sum(r => r.Area), 3);
            m1.RoomCount = m1.SecondClassify.Sum(r => r.RoomCount);

            if (sort)
                m1.SecondClassify = m1.SecondClassify.OrderByDescending(r => r.Area).ToList();

            return m1;
        }
        #endregion //Function

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
        /// 获取房间二级分类面积
        /// </summary>
        /// <param name="matchId">匹配字段</param>
        /// <param name="id">ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="functionCodes">功能编码列表</param>
        /// <remarks>
        /// 部门或楼宇
        /// </remarks>
        public List<RoomSecondClassifyAreaModel> GetSecondClassifyArea(string matchId, int id, int firstCode, List<RoomFunctionCode> functionCodes)
        {
            BsonDocument[] pipeline = {
                new BsonDocument { 
                    { "$match", new BsonDocument {
                        { matchId, id },
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

            List<RoomSecondClassifyAreaModel> data = new List<RoomSecondClassifyAreaModel>();
            var function = functionCodes.Where(r => r.FirstCode == firstCode);
            foreach (var f in function)
            {
                BsonDocument doc = result.ResultDocuments.SingleOrDefault(r => r["_id"].AsInt32 == f.SecondCode);

                RoomSecondClassifyAreaModel model = new RoomSecondClassifyAreaModel
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

            return data;
        }

        /// <summary>
        /// 获取房间一级级分类面积
        /// </summary>
        /// <param name="matchId">匹配字段</param>
        /// <param name="id">iD</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="title">编码名称</param>
        /// <param name="functionCodes">功能编码列表</param>
        /// <param name="sortSecondArea">二级分类是否排序</param>
        /// <remarks>
        /// 部门或楼宇
        /// </remarks>
        /// <returns></returns>
        public RoomFirstClassifyAreaModel GetFirstClassifyArea(string matchId, int id, int firstCode, string title, List<RoomFunctionCode> functionCodes, bool sortSecond = true)
        {
            RoomFirstClassifyAreaModel m1 = new RoomFirstClassifyAreaModel();
            m1.Title = title;
            m1.FunctionFirstCode = firstCode;
            m1.SecondClassify = GetSecondClassifyArea(matchId, id, firstCode, functionCodes);
            m1.Area = Math.Round(m1.SecondClassify.Sum(r => r.Area), 3);
            m1.RoomCount = m1.SecondClassify.Sum(r => r.RoomCount);

            if (sortSecond)
                m1.SecondClassify = m1.SecondClassify.OrderByDescending(r => r.Area).ToList();
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
            EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
            var functionCodes = dictionaryBusiness.GetRoomFunctionCode();

            DepartmentClassifyAreaModel data = new DepartmentClassifyAreaModel
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name
            };

            data.FirstClassify = new List<RoomFirstClassifyAreaModel>();        

            var fc = functionCodes.GroupBy(r => new { r.FirstCode, r.ClassifyName }).Select(g => new { g.Key.FirstCode, g.Key.ClassifyName });

            foreach (var f in fc)
            {
                var c = GetFirstClassifyArea("departmentId", departmentId, f.FirstCode, f.ClassifyName, functionCodes, sortByFirstArea);
                data.FirstClassify.Add(c);
            }

            if (sortByFirstArea)
                data.FirstClassify = data.FirstClassify.OrderByDescending(r => r.Area).ToList();

            data.TotalArea = data.FirstClassify.Sum(r => r.Area);

            return data;
        }

        /// <summary>
        /// 获取楼宇分类面积
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="sortByFirstArea">是否按一级分类排序</param>
        /// <returns></returns>
        public BuildingClassifyAreaModel GetBuildingClassifyArea(int buildingId, bool sortByFirstArea = true)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            Building building = buildingBusiness.Get(buildingId);

            //get codes
            EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
            var functionCodes = dictionaryBusiness.GetRoomFunctionCode();

            BuildingClassifyAreaModel data = new BuildingClassifyAreaModel
            {
                Id = buildingId,
                Name = building.Name
            };

            data.FirstClassify = new List<RoomFirstClassifyAreaModel>();
            var fc = functionCodes.GroupBy(r => new { r.FirstCode, r.ClassifyName }).Select(g => new { g.Key.FirstCode, g.Key.ClassifyName });

            foreach (var f in fc)
            {
                var c = GetFirstClassifyArea("buildingId", buildingId, f.FirstCode, f.ClassifyName, functionCodes, sortByFirstArea);
                data.FirstClassify.Add(c);
            }

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

        /// <summary>
        /// 获取楼群总面积模型
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <remarks>包括分类用房数据</remarks>
        /// <returns></returns>
        public BuildingGroupTotalAreaModel GetBuildingGroupTotalArea(int buildingGroupId)
        {
            BuildingGroupTotalAreaModel model = new BuildingGroupTotalAreaModel();

            IBuildingGroupBusiness buildingGroupBuiness = new MongoBuildingGroupBusiness();
            var buildingGroup = buildingGroupBuiness.Get(buildingGroupId);

            model.BuildingGroupId = buildingGroupId;
            model.BuildingGroupName = buildingGroup.Name;
            model.BuildArea = Convert.ToDouble(buildingGroup.BuildArea);
            model.UsableArea = Convert.ToDouble(buildingGroup.UsableArea);

            //get codes
            EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
            var functionCodes = dictionaryBusiness.GetRoomFunctionCode();

            //get classify            
            model.FirstClassify = new List<RoomFirstClassifyAreaModel>();
            var fc = functionCodes.GroupBy(r => new { r.FirstCode, r.ClassifyName }).Select(g => new { g.Key.FirstCode, g.Key.ClassifyName });

            foreach (var f in fc)
            {
                var first = GetBuildingGroupFirstClassifyArea(buildingGroupId, f.FirstCode, f.ClassifyName, functionCodes, false);
                model.FirstClassify.Add(first);
            }

            model.RoomCount = model.FirstClassify.Sum(r => r.RoomCount);

            return model;
        }
        #endregion //Method
    }
}
