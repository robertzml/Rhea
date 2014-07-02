using MongoDB.Bson;
using Rhea.Data;
using Rhea.Data.Mongo;
using Rhea.Data.Mongo.Estate;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;

namespace Rhea.Business
{
    /// <summary>
    /// 同步原始数据业务
    /// </summary>
    public class SyncBusiness
    {
        #region Field
        private OriginEstateRepository originRepository;
        #endregion //Field

        #region Constructor
        public SyncBusiness()
        {
            this.originRepository = new OriginEstateRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取原始楼群列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetOriginBuildingGroup()
        {
            return this.originRepository.GetBuildingGroupList();
        }

        /// <summary>
        /// 同步楼群
        /// </summary>
        /// <param name="oldBuildingGroupId">原楼群ID</param>
        /// <param name="newId"></param>
        /// <param name="organizeType"></param>
        /// <param name="hasChild"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ErrorCode SyncBuildingGroup(int oldBuildingGroupId, int newId, int organizeType, bool hasChild, int sort)
        {
            BsonDocument doc = this.originRepository.GetBuildingGroup(oldBuildingGroupId);

            Building building = new Building();
            building.BuildingId = newId;
            building.CampusId = 100001;
            building.ParentId = 200000;
            building.Name = doc["name"].AsString;
            building.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            building.OrganizeType = organizeType;
            building.HasChild = hasChild;
            building.UseType = doc["useType"].AsInt32;
            building.BuildArea = doc["buildArea"].AsDouble;
            building.BuildCost = (double?)doc.GetValue("buildCost", null);
            building.Sort = sort;
            building.Status = 0;

            MongoBuildingRepository buildingRepository = new MongoBuildingRepository();
            ErrorCode result = buildingRepository.Create(building);

            return result;
        }
        #endregion //Method
    }
}
