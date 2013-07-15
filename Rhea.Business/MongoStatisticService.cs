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
        public List<CollegeSecondClassifyAreaModel> GetClassifyArea(int departmentId, int firstCode, List<RoomFunctionCode> functionCodes)
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

            List<CollegeSecondClassifyAreaModel> data = new List<CollegeSecondClassifyAreaModel>();
            var function = functionCodes.Where(r => r.FirstCode == firstCode);
            foreach (var f in function)
            {
                BsonDocument doc = result.ResultDocuments.SingleOrDefault(r => r["_id"].AsInt32 == f.SecondCode);

                CollegeSecondClassifyAreaModel model = new CollegeSecondClassifyAreaModel
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
        /// 得到部门分类面积
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public CollegeClassifyAreaModel GetCollegeClassifyArea(int departmentId)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            Department department = departmentBusiness.Get(departmentId);

            //get codes
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var functionCodes = roomBusiness.GetFunctionCodeList();

            CollegeClassifyAreaModel c = new CollegeClassifyAreaModel
            {
                DepartmentId = department.Id,
                CollegeName = department.Name
            };

            c.OfficeDetailArea = GetClassifyArea(department.Id, 1, functionCodes);
            c.OfficeArea = Math.Round(c.OfficeDetailArea.Sum(r => r.Area), 3);
            c.OfficeRoomCount = c.OfficeDetailArea.Sum(r => r.RoomCount);

            c.EducationDetailArea = GetClassifyArea(department.Id, 2, functionCodes);
            c.EducationArea = Math.Round(c.EducationDetailArea.Sum(r => r.Area), 3);
            c.EducationRoomCount = c.EducationDetailArea.Sum(r => r.RoomCount);

            c.ExperimentDetailArea = GetClassifyArea(department.Id, 3, functionCodes);
            c.ExperimentArea = Math.Round(c.ExperimentDetailArea.Sum(r => r.Area), 3);
            c.ExperimentRoomCount = c.ExperimentDetailArea.Sum(r => r.RoomCount);

            c.ResearchDetailArea = GetClassifyArea(department.Id, 4, functionCodes);
            c.ResearchArea = Math.Round(c.ResearchDetailArea.Sum(r => r.Area), 3);
            c.ResearchRoomCount = c.ResearchDetailArea.Sum(r => r.RoomCount);

            c.TotalArea = c.OfficeArea + c.EducationArea + c.ExperimentArea + c.ResearchArea;
            c.TotalRoomCount = c.OfficeRoomCount + c.EducationRoomCount + c.ExperimentRoomCount + c.ResearchRoomCount;

            return c;           
        }

        #endregion //Method
    }
}
