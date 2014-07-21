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
using System.Data;

namespace Rhea.Business
{
    /// <summary>
    /// 同步原始数据业务
    /// </summary>
    public class SyncBusiness
    {
        #region Field
        /// <summary>
        /// 原始数据房产Repository
        /// </summary>
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

            if (organizeType == 1 || organizeType == 2)
                building.HasChild = true;
            else
                building.HasChild = false;

            building.UseType = doc["useType"].AsInt32;
            building.BuildArea = doc["buildArea"].AsDouble;
            building.BuildCost = (double?)doc.GetValue("buildCost", null);
            building.Sort = sort;
            building.Status = 0;

            MongoBuildingRepository buildingRepository = new MongoBuildingRepository();
            ErrorCode result = buildingRepository.Create(building);

            return result;
        }

        /// <summary>
        /// 同步楼群数据
        /// </summary>
        /// <param name="map">原始建筑映射</param>
        /// <returns></returns>
        public ErrorCode SyncBuildingGroup(OriginBuildingMap map)
        {
            BsonDocument doc = this.originRepository.GetBuildingGroup(map.OldId);

            Building building = new Building();
            building.BuildingId = map.NewId;
            building.CampusId = 100001;
            building.ParentId = map.NewParentId;
            building.Name = doc["name"].AsString;
            building.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            building.OrganizeType = map.OrganizeType;
            building.HasChild = map.HasChild;
            building.UseType = doc["useType"].AsInt32;
            building.BuildArea = doc["buildArea"].AsDouble;
            building.BuildCost = (double?)doc.GetValue("buildCost", null);
            building.Sort = map.Sort;
            building.Status = 0;

            MongoBuildingRepository buildingRepository = new MongoBuildingRepository();
            ErrorCode result = buildingRepository.Create(building);

            return result;
        }


        /// <summary>
        /// 获取原始楼宇列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetOriginBuilding()
        {
            return this.originRepository.GetBuildingList();
        }

        /// <summary>
        /// 同步楼宇数据
        /// </summary>
        /// <param name="map">原始建筑映射</param>
        /// <returns></returns>
        public ErrorCode SyncBuilding(OriginBuildingMap map)
        {
            BsonDocument doc = this.originRepository.GetBuilding(map.OldId);

            Building building = new Building();
            building.BuildingId = map.NewId;
            building.CampusId = 100001;
            building.ParentId = map.NewParentId;
            building.Name = doc["name"].AsString;            
            building.OrganizeType = map.OrganizeType;
            building.HasChild = map.HasChild;
            building.UseType = doc["useType"].AsInt32;           
           
            building.Sort = map.Sort;
            building.Status = 0;

            MongoBuildingRepository buildingRepository = new MongoBuildingRepository();
            ErrorCode result = buildingRepository.Create(building);

            return result;
        }

        public ErrorCode SyncSubregionFloor(OriginBuildingMap map)
        {
            BsonDocument doc = this.originRepository.GetBuilding(map.OldId);

            MongoSubregionRepository subregionRepository = new MongoSubregionRepository();
            Subregion subregion = (Subregion)subregionRepository.Get(map.NewId);

            BsonArray array = doc["floors"].AsBsonArray;
            foreach (BsonDocument row in array)
            {                
                if (row.GetValue("status", 0).AsInt32 == 1)
                    continue;

                Floor floor = new Floor();
                floor.Id = row["id"].AsInt32;
                floor.Number = row["number"].AsInt32;
                floor.Name = row["name"].AsString;
                floor.ImageUrl = row["imageUrl"].AsString;
                floor.Remark = row["remark"].AsString;
                floor.Status = 0;

                subregion.Floors.Add(floor);
            }

            ErrorCode result = subregionRepository.Update(subregion);
            return result;
        }

        public ErrorCode SyncBlockFloor(OriginBuildingMap map)
        {
            BsonDocument doc = this.originRepository.GetBuilding(map.OldId);

            MongoBlockRepository blockRepository = new MongoBlockRepository();
            Block block = (Block)blockRepository.Get(map.NewId);          

            BsonArray array = doc["floors"].AsBsonArray;
            foreach (BsonDocument row in array)
            {
                if (row.GetValue("status", 0).AsInt32 == 1)
                    continue;

                Floor floor = new Floor();
                floor.Id = row["id"].AsInt32;
                floor.Number = row["number"].AsInt32;
                floor.Name = row["name"].AsString;
                floor.ImageUrl = row["imageUrl"].AsString;
                floor.Remark = row["remark"].AsString;
                floor.Status = 0;

                block.Floors.Add(floor);
            }

            ErrorCode result = blockRepository.Update(block);
            return result;
        }

        public ErrorCode SyncCottageFloor(OriginBuildingMap map)
        { 
            BsonDocument doc = this.originRepository.GetChildBuilding(map.OldId);

            if (doc == null)
                return ErrorCode.NotImplement;

            MongoCottageRepository cottageRepository = new MongoCottageRepository();
            Cottage cottage = (Cottage)cottageRepository.Get(map.NewId);

            if (cottage.Floors.Count != 0)
                return ErrorCode.Exception;

            if (!doc.Contains("floors"))
                return ErrorCode.Exception;

            BsonArray array = doc["floors"].AsBsonArray;

            foreach (BsonDocument row in array)
            {
                if (row.GetValue("status", 0).AsInt32 == 1)
                    continue;

                Floor floor = new Floor();
                floor.Id = row["id"].AsInt32;
                floor.Number = row["number"].AsInt32;
                floor.Name = row["name"].AsString;
                floor.ImageUrl = row["imageUrl"].AsString;
                floor.Remark = row["remark"].AsString;
                floor.Status = 0;

                cottage.Floors.Add(floor);
            }

            ErrorCode result = cottageRepository.Update(cottage);
            return result;
        }

        /// <summary>
        /// 从Sqlite里获取关联数据
        /// </summary>
        /// <returns></returns>
        public List<OriginBuildingMap> GetRelateData()
        {
            SqliteRepository repository = new SqliteRepository(@"E:\Test\rheaimport.sqlite");

            string sql = "SELECT * FROM buildingGroup";
            DataTable dt = repository.ExecuteQuery(sql);

            List<OriginBuildingMap> maps = new List<OriginBuildingMap>();

            foreach (DataRow row in dt.Rows)
            {
                OriginBuildingMap map = new OriginBuildingMap();
                map.OldId = Convert.ToInt32(row["原Id"]);
                map.NewId = Convert.ToInt32(row["ID"]);
                map.OrganizeType = Convert.ToInt32(row["组织类型"]);
                map.OldParentId = Convert.ToInt32(row["父级ID"]);
                if (map.OrganizeType == 1 || map.OrganizeType == 2)
                    map.HasChild = true;
                else
                    map.HasChild = false;
                map.Sort = Convert.ToInt32(row["排序"]);

                maps.Add(map);
            }

            foreach (var map in maps)
            {
                if (map.OldParentId == 200000)
                    map.NewParentId = 200000;
                else
                    map.NewParentId = maps.Single(r => r.OldId == map.OldParentId).NewId;
            }

            return maps;
        }
        #endregion //Method
    }
}
