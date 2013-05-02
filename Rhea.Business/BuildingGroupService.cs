using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Entities;
using Rhea.Data.Server;

namespace Rhea.Business
{
    public class BuildingGroupService : IBuildingGroupService
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaContext context = new RheaContext(RheaConstant.CronusDatabase);

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

        public int Create(Data.Entities.BuildingGroup data)
        {
            throw new NotImplementedException();
        }

        public bool Update(Data.Entities.BuildingGroup data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
