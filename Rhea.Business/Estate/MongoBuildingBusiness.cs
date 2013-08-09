using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model.Account;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 楼宇业务类
    /// </summary>
    public class MongoBuildingBusiness : IBuildingBusiness
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
        private Building ModelBind(BsonDocument doc)
        {
            Building building = new Building();
            building.Id = doc["id"].AsInt32;
            building.Name = doc["name"].AsString;
            building.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            building.BuildingGroupId = doc.GetValue("buildingGroupId").AsInt32;
            building.BuildArea = (double?)doc.GetValue("buildArea", null);
            building.UsableArea = (double?)doc.GetValue("usableArea", null);
            building.AboveGroundFloor = (int?)doc.GetValue("aboveGroundFloor", null);
            building.UnderGroundFloor = (int?)doc.GetValue("underGroundFloor", null);
            building.UseType = doc["useType"].AsInt32;
            building.PartMapUrl = doc.GetValue("partMapUrl", "").AsString;
            building.Remark = doc.GetValue("remark", "").AsString;
            building.Sort = doc.GetValue("sort").AsInt32;
            building.Status = doc.GetValue("status", 0).AsInt32;
            //building.UsableArea = GetUsableArea(building.Id);

            if (doc.Contains("floors"))
            {
                BsonArray array = doc["floors"].AsBsonArray;
                for (int i = 0; i < array.Count; i++)
                {
                    BsonDocument d = array[i].AsBsonDocument;
                    if (d.GetValue("status", 0).AsInt32 == 1)
                        continue;

                    Floor f = new Floor
                    {
                        Id = d["id"].AsInt32,
                        Name = d["name"].AsString,
                        Number = d["number"].AsInt32,
                        BuildArea = (double?)d.GetValue("buildArea", null),
                        UsableArea = (double?)d.GetValue("usableArea", null),
                        ImageUrl = d.GetValue("imageUrl", "").AsString,
                        Remark = d.GetValue("remark", "").AsString,
                        Status = d.GetValue("status", 0).AsInt32
                    };
                    //f.UsableArea = GetFloorUsableArea(building.Id, f.Number);

                    building.Floors.Add(f);
                }
            }

            if (doc.Contains("editor"))
            {
                BsonDocument editor = doc["editor"].AsBsonDocument;
                building.Editor.Id = editor["id"].AsObjectId.ToString();
                building.Editor.Name = editor["name"].AsString;
                building.Editor.Time = editor["time"].AsBsonDateTime.ToLocalTime();
                building.Editor.Type = editor["type"].AsInt32;
            }

            return building;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <returns></returns>
        public List<Building> GetList(bool sort = false)
        {
            List<Building> buildings = new List<Building>();
            List<BsonDocument> docs = this.context.FindAll(EstateCollection.Building);

            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Building building = ModelBind(doc);
                buildings.Add(building);
            }

            if (sort)
                buildings = buildings.OrderBy(r => r.Sort).ToList();

            return buildings;
        }

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public List<Building> GetListByBuildingGroup(int buildingGroupId, bool sort = false)
        {
            List<BsonDocument> docs = this.context.Find(EstateCollection.Building, "buildingGroupId", buildingGroupId);

            List<Building> buildings = new List<Building>();
            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Building building = ModelBind(doc);
                buildings.Add(building);
            }

            if (sort)
                buildings = buildings.OrderBy(r => r.Sort).ToList();

            return buildings;
        }

        /// <summary>
        /// 获取部门相关楼宇
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public List<Building> GetListByDepartment(int departmentId)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "departmentId", departmentId }
                    }
                }},
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "$buildingId" }
                    }
                }},
                new BsonDocument {
                    { "$sort", new BsonDocument {
                        { "_id", 1 }
                    }
                }}
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);

            List<Building> buildings = new List<Building>();
            foreach (var doc in result.ResultDocuments)
            {
                int bid = doc["_id"].AsInt32;
                BsonDocument d = this.context.FindOne(EstateCollection.Building, "id", bid);
                Building building = ModelBind(d);

                buildings.Add(building);
            }

            return buildings;
        }

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public Building Get(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Building, "id", id);

            if (doc != null)
            {
                Building building = ModelBind(doc);
                if (building.Status == 1)
                    return null;

                return building;
            }
            else
                return null;
        }

        /// <summary>
        /// 得到楼宇名称
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Building, "id", id);

            if (doc != null)
                return doc["name"].AsString;
            else
                return string.Empty;
        }

        /// <summary>
        /// 添加楼宇
        /// </summary>
        /// <param name="data">楼宇数据</param>
        /// <param name="user">相关用户</param>
        /// <returns>楼宇ID,0:添加失败</returns>
        public int Create(Building data, UserProfile user)
        {
            data.Id = this.context.FindSequenceIndex(EstateCollection.Building) + 1;

            BsonDocument doc = new BsonDocument
            {
                { "id", data.Id },                
                { "name" , data.Name },                
                { "imageUrl", data.ImageUrl ?? "" },
                { "buildingGroupId", data.BuildingGroupId },
                { "buildArea", (BsonValue)data.BuildArea },
                { "usableArea", (BsonValue)data.UsableArea },
                { "aboveGroundFloor", (BsonValue)data.AboveGroundFloor },
                { "underGroundFloor", (BsonValue)data.UnderGroundFloor },
                { "useType", data.UseType },
                { "parMapUrl", data.PartMapUrl ?? "" },
                { "remark", data.Remark ?? "" },
                { "sort", data.Sort },
                { "editor.id", user._id },
                { "editor.name", user.UserName },
                { "editor.time", DateTime.Now },
                { "editor.type", 1 },
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(EstateCollection.Building, doc);

            if (result.HasLastErrorMessage)
                return 0;
            else
                return data.Id;
        }

        /// <summary>
        /// 编辑楼宇
        /// </summary>
        /// <param name="data">楼宇数据</param>
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        public bool Edit(Building data, UserProfile user)
        {
            var query = Query.EQ("id", data.Id);

            var update = Update.Set("name", data.Name)
                .Set("imageUrl", data.ImageUrl ?? "")
                .Set("buildingGroupId", data.BuildingGroupId)
                .Set("buildArea", (BsonValue)data.BuildArea)
                .Set("usableArea", (BsonValue)data.UsableArea)
                .Set("aboveGroundFloor", (BsonValue)data.AboveGroundFloor)
                .Set("underGroundFloor", (BsonValue)data.UnderGroundFloor)
                .Set("useType", data.UseType)
                .Set("partMapUrl", data.PartMapUrl ?? "")
                .Set("sort", data.Sort)
                .Set("remark", data.Remark ?? "")
                .Set("editor.id", user._id)
                .Set("editor.name", user.UserName)
                .Set("editor.time", DateTime.Now)
                .Set("editor.type", 2);

            WriteConcernResult result = this.context.Update(EstateCollection.Building, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        public bool Delete(int id, UserProfile user)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("status", 1)
                .Set("editor.id", user._id)
                .Set("editor.name", user.UserName)
                .Set("editor.time", DateTime.Now)
                .Set("editor.type", 3);

            WriteConcernResult result = this.context.Update(EstateCollection.Building, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取楼层
        /// </summary>
        /// <param name="id">楼层ID</param>
        /// <returns></returns>
        public Floor GetFloor(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Building, "floors.id", id);

            if (doc != null)
            {
                Building building = ModelBind(doc);
                if (building.Status == 1)
                    return null;

                return building.Floors.Single(r => r.Id == id);
            }
            else
                return null;
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns></returns>
        public int CreateFloor(int buildingId, Floor floor, UserProfile user)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$project", new BsonDocument {
                        { "id", 1 },
                        { "name", 1 },
                        { "floors", 1 }
                    }}
                },
                new BsonDocument {
                    { "$unwind", "$floors" }
                },
                new BsonDocument {
                    { "$sort", new BsonDocument {
                        { "floors.id", -1 }
                    }}
                },
                new BsonDocument {
                    { "$limit", 1 }
                }
            };

            AggregateResult max = this.context.Aggregate(EstateCollection.Building, pipeline);
            if (max.ResultDocuments.Count() == 0)
                return 0;

            BsonDocument fd = max.ResultDocuments.First();
            int maxId = fd["floors"].AsBsonDocument["id"].AsInt32;
            floor.Id = maxId + 1;

            BsonDocument doc = new BsonDocument
            {
                { "id", floor.Id },
                { "number", floor.Number },
                { "name", floor.Name },
                { "buildArea", (BsonValue)floor.BuildArea },
                { "usableArea", (BsonValue)floor.UsableArea },
                { "imageUrl", floor.ImageUrl ?? "" },
                { "remark", floor.Remark ?? "" },
                { "status", 0 }
            };

            var query = Query.EQ("id", buildingId);
            var update = Update.Push("floors", doc)
                .Set("editor.id", user._id)
                .Set("editor.name", user.UserName)
                .Set("editor.time", DateTime.Now)
                .Set("editor.type", 4);

            WriteConcernResult result = this.context.Update(EstateCollection.Building, query, update);
            if (result.HasLastErrorMessage)
                return 0;
            else
                return floor.Id;
        }

        /// <summary>
        /// 编辑楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns></returns>
        public bool EditFloor(int buildingId, Floor floor, UserProfile user)
        {
            var query = Query.And(Query.EQ("id", buildingId),
               Query.EQ("floors.id", floor.Id));

            var update = Update.Set("floors.$.number", floor.Number)
                .Set("floors.$.name", floor.Name)
                .Set("floors.$.buildArea", (BsonValue)floor.BuildArea)
                .Set("floors.$.usableArea", (BsonValue)floor.UsableArea)
                .Set("floors.$.imageUrl", floor.ImageUrl ?? "")
                .Set("floors.$.remark", floor.Remark ?? "")
                .Set("editor.id", user._id)
                .Set("editor.name", user.UserName)
                .Set("editor.time", DateTime.Now)
                .Set("editor.type", 5);

            WriteConcernResult result = this.context.Update(EstateCollection.Building, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        public bool DeleteFloor(int buildingId, int floorId, UserProfile user)
        {
            var query = Query.And(Query.EQ("id", buildingId),
                Query.EQ("floors.id", floorId));

            var update = Update.Set("floors.$.status", 1)
                .Set("editor.id", user._id)
                .Set("editor.name", user.UserName)
                .Set("editor.time", DateTime.Now)
                .Set("editor.type", 6);

            WriteConcernResult result = this.context.Update(EstateCollection.Building, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 备份楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public bool Backup(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Building, "id", id);
            doc.Remove("_id");

            WriteConcernResult result = this.context.Insert(EstateCollection.BuildingBackup, doc);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取楼宇总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.context.Count(EstateCollection.Building, query);
            return (int)count;
        }

        /// <summary>
        /// 楼层总数
        /// </summary>
        /// <returns></returns>
        public int FloorCount()
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$project", new BsonDocument {
                        { "id", 1 },
                        { "floors", 1 }
                    }}
                }, new BsonDocument {
                    { "$unwind", "$floors" }
                }, new BsonDocument {
                    { "$match", new BsonDocument {
                        { "floors.status", new BsonDocument {
                            { "$ne", 1 }
                        }}
                    }}
                }
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Building, pipeline);

            return result.ResultDocuments.Count();
        }

        /// <summary>
        /// 获取楼宇使用面积
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public double GetUsableArea(int buildingId)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "buildingId", buildingId }
                    }}
                },
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "id" },
                        { "area", new BsonDocument {
                            { "$sum", "$usableArea" }
                        }}
                    }}
                }
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);

            var doc = result.ResultDocuments.SingleOrDefault();

            if (doc != null)
                return Math.Round(doc["area"].AsDouble, 2);
            else
                return 0;
        }

        /// <summary>
        /// 获取楼层使用面积
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public double GetFloorUsableArea(int buildingId, int floor)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "buildingId", buildingId },
                        { "floor", floor }
                    }}
                },
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "id" },
                        { "area", new BsonDocument {
                            { "$sum", "$usableArea" }
                        }}
                    }}
                }
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);

            var doc = result.ResultDocuments.SingleOrDefault();

            if (doc != null)
                return Math.Round(doc["area"].AsDouble, 2);
            else
                return 0;
        }
        #endregion //Method
    }
}
