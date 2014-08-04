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
using Rhea.Data.Mongo.Personnel;
using Rhea.Model.Personnel;

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

        /// <summary>
        /// 从Sqlite里获取关联部门
        /// </summary>
        /// <returns></returns>
        public List<OriginDepartmentMap> GetRelateDepartment()
        {
            SqliteRepository repository = new SqliteRepository(@"E:\Test\rheaimport.sqlite");

            string sql = "SELECT * FROM department";
            DataTable dt = repository.ExecuteQuery(sql);

            List<OriginDepartmentMap> maps = new List<OriginDepartmentMap>();

            foreach(DataRow row in dt.Rows)
            {
                OriginDepartmentMap map = new OriginDepartmentMap();
                map.OldId = Convert.ToInt32(row["原编码"]);
                map.NewId = Convert.ToInt32(row["编码"]);
                map.NewName = row["单位名称"].ToString();
                map.Type = Convert.ToInt32(row["类型"]);

                maps.Add(map);
            }

            return maps;
        }
        
        public ErrorCode SyncDepartment(OriginDepartmentMap map)
        {
            BsonDocument doc = this.originRepository.GetDepartment(map.OldId);

            Department department = new Department();
            department.DepartmentId = map.NewId;
            department.Name = map.NewName;
            department.Type = map.Type;
            if (map.OldId != 0)
            {
                department.ShortName = doc.GetValue("shortName", "").AsString;
                department.ImageUrl = doc.GetValue("imageUrl", "").AsString;
                department.Description = doc.GetValue("description", "").AsString;
            }
            department.Status = 0;

            MongoDepartmentRepository departmentRepository = new MongoDepartmentRepository();          
            ErrorCode result = departmentRepository.Create(department);

            return result;
        }

        /// <summary>
        /// 获取原始房间列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetOriginRoom()
        {
            return this.originRepository.GetRoomList();
        }

        /// <summary>
        /// 同步房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="buildingMap"></param>
        /// <param name="departmentMap"></param>
        /// <returns></returns>
        public ErrorCode SyncRoom(int id, List<OriginBuildingMap> buildingMap, List<OriginDepartmentMap> departmentMap)
        {
            BsonDocument doc = this.originRepository.GetRoom(id);

            Room room = new Room();
            room.RoomId = id;
            room.Name = doc["name"].AsString;
            room.Number = doc["number"].AsString;
            room.Floor = doc["floor"].AsInt32;
            room.UsableArea = doc["usableArea"].AsDouble;
            room.RoomStatus = doc["roomStatus"].AsString;
            room.Status = 0;

            room.DepartmentId = departmentMap.Single(r => r.OldId == doc["departmentId"].AsInt32).NewId;
            
            int oldBuildingId = doc["buildingId"].AsInt32;

            if (buildingMap.Any(r => r.OldId == oldBuildingId))
            {
                room.BuildingId = buildingMap.Single(r => r.OldId == oldBuildingId).NewId;
            }
            else
            {
                BsonDocument building = this.originRepository.GetBuilding(doc["buildingId"].AsInt32);
                int bgId = building["buildingGroupId"].AsInt32;
                room.BuildingId = buildingMap.Single(r => r.OldId == bgId).NewId;
            }



            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
