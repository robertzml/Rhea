using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Entities;
using Rhea.Data.Estate;
using Rhea.Data.Server;

namespace Rhea.Business.Estate
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
        private RheaMongoContext context = new RheaMongoContext(RheaConstant.CronusDatabase);       
        #endregion //Field  

        #region Function
        /// <summary>
        /// 获取部门分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="functionCodes">功能编码列表</param>
        private List<SecondClassifyAreaModel> GetClassifyArea(int departmentId, int firstCode, List<RoomFunctionCode> functionCodes)
        {
            BsonDocument[] pipeline = {
                new BsonDocument { 
                    { "$match", new BsonDocument {
                        { "department.id", departmentId },
                        { "function.firstCode", firstCode }
                    }}
                },
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "$function.secondCode" },
                        { "area", new BsonDocument {
                            { "$sum", "$usableArea" }
                        }}
                    }}
                }
            };

            AggregateResult result = this.context.Aggregate("room", pipeline);

            List<SecondClassifyAreaModel> data = new List<SecondClassifyAreaModel>();
            var function = functionCodes.Where(r => r.FirstCode == firstCode);
            foreach (var f in function)
            {
                BsonDocument doc = result.ResultDocuments.SingleOrDefault(r => r["_id"].AsInt32 == f.SecondCode);

                SecondClassifyAreaModel model = new SecondClassifyAreaModel
                {
                    FunctionFirstCode = firstCode,
                    FunctionSecondCode = f.SecondCode,
                    FunctionProperty = f.FunctionProperty,

                };
                if (doc != null)
                    model.Area = Math.Round(doc["area"].AsDouble, 3);
                else
                    model.Area = 0.0;

                data.Add(model);
            }

            return data;
        }

        /// <summary>
        /// 获取学院楼宇面积
        /// </summary>
        /// <param name="departmentId">学院ID</param>
        /// <param name="buildingList">楼宇列表</param>
        /// <returns></returns>
        private List<BuildingAreaModel> GetBuildingArea(int departmentId, List<Building> buildingList)
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
                    Area = Math.Round(doc["area"].AsDouble, 3)
                };

                data.Add(model);
            }

            return data;
        }

        /// <summary>
        /// 得到学院分类用房面积
        /// </summary>
        /// <returns></returns>
        private List<CollegeClassifyAreaModel> GetCollegeClassifyArea()
        {
            //get departments
            IDepartmentService departmentService = new MongoDepartmentService();
            var departments = departmentService.GetList().Where(r => r.Type == 1);

            //get codes
            IRoomService roomService = new MongoRoomService();
            var functionCodes = roomService.GetFunctionCodeList();

            List<CollegeClassifyAreaModel> data = new List<CollegeClassifyAreaModel>();
            //get area by department
            foreach (var department in departments)
            {
                CollegeClassifyAreaModel c = new CollegeClassifyAreaModel
                {
                    Id = department.Id,
                    CollegeName = department.Name
                };

                c.OfficeDetailArea = GetClassifyArea(department.Id, 1, functionCodes);
                c.OfficeArea = Math.Round(c.OfficeDetailArea.Sum(r => r.Area), 3);

                c.EducationDetailArea = GetClassifyArea(department.Id, 2, functionCodes);
                c.EducationArea = Math.Round(c.EducationDetailArea.Sum(r => r.Area), 3);

                c.ExperimentDetailArea = GetClassifyArea(department.Id, 3, functionCodes);
                c.ExperimentArea = Math.Round(c.ExperimentDetailArea.Sum(r => r.Area), 3);

                c.ResearchDetailArea = GetClassifyArea(department.Id, 4, functionCodes);
                c.ResearchArea = Math.Round(c.ResearchDetailArea.Sum(r => r.Area), 3);

                data.Add(c);
            }

            return data;
        }

        /// <summary>
        /// 得到学院分楼宇面积
        /// </summary>
        /// <returns></returns>
        private List<CollegeBuildingAreaModel> GetCollegeBuildingArea()
        {
            //get departments
            IDepartmentService departmentService = new MongoDepartmentService();
            var departments = departmentService.GetList().Where(r => r.Type == 1);

            //get buildings        
            IBuildingService buildingService = new MongoBuildingService();
            var buildings = buildingService.GetList();
           
            List<CollegeBuildingAreaModel> data = new List<CollegeBuildingAreaModel>();

            //get area by department
            foreach (var department in departments)
            {
                List<BuildingAreaModel> model = GetBuildingArea(department.Id, buildings);

                CollegeBuildingAreaModel c = new CollegeBuildingAreaModel
                {
                    Id = department.Id,
                    CollegeName = department.Name,
                    BuildingArea = model
                };

                data.Add(c);
            }

            return data;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取统计面积数据
        /// </summary>
        /// <param name="type">统计类型</param>
        /// <remarks>
        /// type=1:学院分类用房面积
        /// type=2:学院分楼宇用房面积
        /// </remarks>
        /// <returns></returns>
        public T GetStatisticArea<T>(int type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取对象数量
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public int GetEntitySize(int type)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
