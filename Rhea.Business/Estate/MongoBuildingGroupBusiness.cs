using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// MongoDB楼群业务
    /// </summary>
    public class MongoBuildingGroupBusiness : IBuildingGroupBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);
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
            buildingGroup.PartMapUrl = doc.GetValue("partMapUrl", "").AsString;
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
            buildingGroup.UseType = doc.GetValue("useType", 1).AsInt32;
            buildingGroup.Remark = doc.GetValue("remark", "").AsString;            
            buildingGroup.Status = doc.GetValue("status", 0).AsInt32;

            //buildingGroup.UsableArea = GetUsableArea(buildingGroup.Id);

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
            List<BsonDocument> doc = this.context.FindAll(EstateCollection.BuildingGroup);

            foreach (var d in doc)
            {
                if (d.GetValue("status", 0).AsInt32 == 1)
                    continue;
                BuildingGroup buildingGroup = ModelBind(d);
                buildingGroups.Add(buildingGroup);
            }

            buildingGroups = buildingGroups.OrderBy(r => r.Id).ToList();
            return buildingGroups;
        }

        /// <summary>
        /// 得到楼群简单列表
        /// </summary>
        /// <returns></returns>
        public List<BuildingGroup> GetSimpleList()
        {
            List<BuildingGroup> buildingGroups = new List<BuildingGroup>();
            List<BsonDocument> docs = this.context.FindAll(EstateCollection.BuildingGroup);

            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                BuildingGroup buildingGroup = new BuildingGroup
                {
                    Id = doc["id"].AsInt32,
                    Name = doc["name"].AsString
                };
                buildingGroups.Add(buildingGroup);
            }

            buildingGroups = buildingGroups.OrderBy(r => r.Id).ToList();
            return buildingGroups;
        }

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public BuildingGroup Get(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.BuildingGroup, "id", id);
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
        /// 得到楼群名称
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.BuildingGroup, "id", id);

            if (doc != null)
                return doc["name"].AsString;
            else
                return string.Empty;
        }

        /// <summary>
        /// 添加楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <returns>楼群ID</returns>
        public int Create(BuildingGroup data)
        {            
            data.Id = this.context.FindSequenceIndex(EstateCollection.BuildingGroup) + 1;

            BsonDocument doc = new BsonDocument
            {
                { "id", data.Id },
                { "name", data.Name },
                { "imageUrl", data.ImageUrl },
                { "partMapUrl", data.PartMapUrl },
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

            WriteConcernResult result = this.context.Insert(EstateCollection.BuildingGroup, doc);

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
        public bool Edit(BuildingGroup data)
        {
            var query = Query.EQ("id", data.Id);
            var update = Update.Set("name", data.Name)
                .Set("imageUrl", data.ImageUrl ?? "")
                .Set("partMapUrl", data.PartMapUrl ?? "")
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

            WriteConcernResult result = this.context.Update(EstateCollection.BuildingGroup, query, update);

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

            WriteConcernResult result = this.context.Update(EstateCollection.BuildingGroup, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.context.Count(EstateCollection.BuildingGroup, query);
            return (int)count;
        }

        /// <summary>
        /// 获取使用面积
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public double GetUsableArea(int id)
        {
            //Optimize
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetListByBuildingGroup(id);
            double area = 0;

            foreach (var b in buildings)
            {
                double a = buildingBusiness.GetUsableArea(b.Id);
                area += a;
            }

            return area;
        }
        #endregion //Method
    }
}
