using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Entities;
using Rhea.Data.Server;

namespace Rhea.Business
{
    /// <summary>
    /// MongoDB楼群业务
    /// </summary>
    public class MongoBuildingGroupService : IBuildingGroupService
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaConstant.CronusDatabase);

        /// <summary>
        /// Collection名称
        /// </summary>
        private readonly string collectionName = "buildingGroup";
        #endregion //Field

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private BuildingGroup ModelBind(BsonDocument doc)
        {
            BuildingGroup buildingGroup = new BuildingGroup();
            buildingGroup.Id = doc["id"].AsInt32;
            buildingGroup.Name = doc["name"].AsString;
            buildingGroup.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            buildingGroup.BuildingCount = (int?)doc.GetValue("buildingCount", null);
            buildingGroup.AreaCoeffcient = doc.GetValue("areaCoeffcient", "").AsString;
            buildingGroup.BuildArea = (double?)doc.GetValue("buildArea", null);
            buildingGroup.UsableArea = (double?)doc.GetValue("usableArea", null);
            buildingGroup.Floorage = (double?)doc.GetValue("floorage", null);
            buildingGroup.BuildType = doc.GetValue("buildType", "").AsString;
            buildingGroup.BuildStructure = doc.GetValue("buildStructure", "").AsString;
            buildingGroup.BuildCost = (double?)doc.GetValue("buildCost", null);
            buildingGroup.CurrentValue = (double?)doc.GetValue("currentValue", null);
            buildingGroup.Classified = doc.GetValue("classified", "").AsString;
            buildingGroup.FundsSubject = doc.GetValue("fundsSubject", "").AsString;
            buildingGroup.EquityNumber = doc.GetValue("equityNumber", "").AsString;
            buildingGroup.BuildDate = (DateTime?)doc.GetValue("buildDate", null);
            buildingGroup.FixedYear = (int?)doc.GetValue("fixedYear", null);
            buildingGroup.DesignCompany = doc.GetValue("designCompany", "").AsString;
            buildingGroup.ConstructCompany = doc.GetValue("constructCompany", "").AsString;
            buildingGroup.ManageType = doc.GetValue("manageType", "").AsString;
            buildingGroup.Remark = doc.GetValue("remark", "").AsString;
            buildingGroup.Status = doc.GetValue("status", 0).AsInt32;

            if (buildingGroup.BuildDate != null)
                buildingGroup.BuildDate = ((DateTime)buildingGroup.BuildDate).ToLocalTime();
            return buildingGroup;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取楼群列表
        /// </summary>
        /// <returns></returns>
        public List<BuildingGroup> GetList()
        {
            List<BuildingGroup> buildingGroups = new List<BuildingGroup>();
            List<BsonDocument> doc = this.context.FindAll(this.collectionName);

            foreach (var d in doc)
            {
                if (d.GetValue("status", 0).AsInt32 == 1)
                    continue;
                BuildingGroup buildingGroup = ModelBind(d);
                buildingGroups.Add(buildingGroup);
            }

            return buildingGroups;
        }

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public BuildingGroup Get(int id)
        {
            BsonDocument doc = this.context.FindOne(this.collectionName, "id", id);
            if (doc != null)
            {
                BuildingGroup buildingGroup = ModelBind(doc);
                if (buildingGroup.Status == 1)
                    return null;

                return buildingGroup;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <returns>楼群ID</returns>
        public int Create(BuildingGroup data)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$project", new BsonDocument {
                        { "id", 1 }
                    }}
                },
                new BsonDocument {
                    { "$sort", new BsonDocument {
                        { "id", -1 }
                    }}
                },
                new BsonDocument {
                    { "$limit", 1 }
                }
            };

            AggregateResult max = this.context.Aggregate(this.collectionName, pipeline);
            if (max.ResultDocuments.Count() == 0)
                return 0;

            int maxId = max.ResultDocuments.First()["id"].AsInt32;
            data.Id = maxId + 1;

            BsonDocument doc = new BsonDocument
            {
                { "id", data.Id },
                { "name", data.Name },
                { "imageUrl", data.ImageUrl },
                { "buildingCount", data.BuildingCount },
                { "areaCoeffcient", data.AreaCoeffcient },
                { "buildArea", data.BuildArea },
                { "usableArea", data.UsableArea },
                { "floorage", data.Floorage },
                { "buildType", data.BuildType },
                { "buildStructure", data.BuildStructure },
                { "buildCost", data.BuildCost },
                { "currentValue", data.CurrentValue },
                { "classified", data.Classified },
                { "fundsSubject", data.FundsSubject },
                { "equityNumber", data.EquityNumber },
                { "buildDate", data.BuildDate },
                { "fixedYear", data.FixedYear },
                { "designCompany", data.DesignCompany },
                { "constructCompany", data.ConstructCompany },
                { "manageType", data.ManageType },
                { "remark", data.Remark },
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(this.collectionName, doc);

            if (result.HasLastErrorMessage)
                return 0;
            else
                return data.Id;
        }

        /// <summary>
        /// 更新楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <returns></returns>
        public bool Edit(Data.Entities.BuildingGroup data)
        {
            var query = Query.EQ("id", data.Id);
            var update = Update.Set("name", data.Name)
                .Set("imageUrl", data.ImageUrl ?? "")
                .Set("buildingCount", (BsonValue)data.BuildingCount)
                .Set("areaCoeffcient", data.AreaCoeffcient ?? "")
                .Set("buildArea", (BsonValue)data.BuildArea)
                .Set("usableArea", (BsonValue)data.UsableArea)
                .Set("floorage", (BsonValue)data.Floorage)
                .Set("buildType", data.BuildType ?? "")
                .Set("buildStructure", data.BuildStructure ?? "")
                .Set("buildCost", (BsonValue)data.BuildCost)
                .Set("currentValue", (BsonValue)data.CurrentValue)
                .Set("classified", data.Classified ?? "")
                .Set("fundsSubject", data.FundsSubject ?? "")
                .Set("equityNumber", data.EquityNumber ?? "")
                .Set("buildDate", (BsonValue)data.BuildDate)
                .Set("fixedYear", (BsonValue)data.FixedYear)
                .Set("designCompany", data.DesignCompany ?? "")
                .Set("constructCompany", data.ConstructCompany ?? "")
                .Set("manageType", data.ManageType ?? "")
                .Set("remark", data.Remark ?? "");

            WriteConcernResult result = this.context.Update(this.collectionName, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("status", 1);

            WriteConcernResult result = this.context.Update(this.collectionName, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }
        #endregion //Method
    }
}
