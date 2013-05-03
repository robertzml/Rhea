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
    /// <summary>
    /// 楼宇业务类
    /// </summary>
    public class MongoBuildingService : IBuildingService
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaContext context = new RheaContext(RheaConstant.CronusDatabase);

        /// <summary>
        /// Collection名称
        /// </summary>
        private readonly string collectionName = "building";
        #endregion //Field

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Building ModelBind(BsonDocument doc)
        {
            Building building = new Building();           
            building.Id = doc["id"].AsInt32;
            building.Name = doc["name"].AsString;
            building.ImageUrl = doc["imageUrl", ""].AsString;
            building.BuildingGroupId = doc["buildingGroupId"].AsInt32;
            building.BuildArea = (double?)doc["buildArea", null];
            building.UsableArea = (double?)doc["usableArea", null];
            building.AboveGroundFloor = (int?)doc["aboveGroundFloor", null];
            building.UnderGroundFloor = (int?)doc["underGroundFloor", null];
            building.Remark = doc["remark", ""].AsString;
            building.Status = doc["status", 0].AsInt32;

            if (doc.Contains("floors"))
            {
                BsonArray array = doc["floors"].AsBsonArray;
                for (int i = 0; i < array.Count; i++)
                {
                    BsonDocument d = array[i].AsBsonDocument;
                    if (d["status", 0].AsInt32 == 1)
                        continue;
                    building.Floors.Add(
                        new Floor
                        {
                            Id = d["id"].AsInt32,
                            Name = d["name"].AsString,
                            Number = d["number"].AsInt32,
                            BuildArea = (double?)d["buildArea", null],
                            UsableArea = (double?)d["usableArea", null],
                            ImageUrl = d["imageUrl", ""].AsString,
                            Remark = d["remark", ""].AsString,
                            Status = d["status", 0].AsInt32
                        }
                    );
                }
            }

            return building;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <returns></returns>
        public List<Building> GetList()
        {
            List<Building> buildings = new List<Building>();
            List<BsonDocument> docs = this.context.FindAll("building");

            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Building building = ModelBind(doc);
                buildings.Add(building);
            }

            return buildings;
        }

        public List<Building> GetListByBuildingGroup(int buildingGroupId)
        {
            throw new NotImplementedException();
        }

        public Building Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Create(Building data)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Building data)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int CreateFloor(int buildingId, Floor floor)
        {
            throw new NotImplementedException();
        }

        public bool EditFloor(int buildingId, Floor floor)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFloor(int buildingId, int floorId)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
