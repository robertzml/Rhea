using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model;
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
        private RheaMongoContext context;

        /// <summary>
        /// 备份接口
        /// </summary>
        private IBackupBusiness backupBusiness;

        /// <summary>
        /// 日志接口
        /// </summary>
        private ILogBusiness logBusiness;

        /// <summary>
        /// 归档接口
        /// </summary>
        private IArchiveBusiness archiveBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 楼宇业务类
        /// </summary>
        public MongoBuildingBusiness()
        {
            this.context = new RheaMongoContext(RheaServer.EstateDatabase);
            this.backupBusiness = new EstateBackupBusiness();
            this.logBusiness = new MongoLogBusiness();
            this.archiveBusiness = new EstateArchiveBusiness();
        }
        #endregion //Constructor

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

            if (doc.Contains("log"))
            {
                BsonDocument log = doc["log"].AsBsonDocument;
                building.Log._id = log["id"].AsObjectId;
                building.Log.UserName = log["name"].AsString;
                building.Log.Time = log["time"].AsBsonDateTime.ToLocalTime();
                building.Log.Type = log["type"].AsInt32;
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
            var docs = this.context.FindAll(EstateCollection.Building);

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
                if (d.GetValue("status", 0).AsInt32 == 1)
                    continue;

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
        /// <returns>楼宇ID,0:添加失败</returns>
        public int Create(Building data)
        {
            data.Id = this.context.FindSequenceIndex(EstateCollection.Building) + 1;

            BsonDocument doc = new BsonDocument //13
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
        /// <returns></returns>
        public bool Edit(Building data)
        {
            var query = Query.EQ("id", data.Id);

            var update = Update.Set("name", data.Name)  //11
                .Set("imageUrl", data.ImageUrl ?? "")
                .Set("buildingGroupId", data.BuildingGroupId)
                .Set("buildArea", (BsonValue)data.BuildArea)
                .Set("usableArea", (BsonValue)data.UsableArea)
                .Set("aboveGroundFloor", (BsonValue)data.AboveGroundFloor)
                .Set("underGroundFloor", (BsonValue)data.UnderGroundFloor)
                .Set("useType", data.UseType)
                .Set("partMapUrl", data.PartMapUrl ?? "")
                .Set("sort", data.Sort)
                .Set("remark", data.Remark ?? "");

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
        /// <returns></returns>
        public bool Delete(int id)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("status", 1);

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
        public int CreateFloor(int buildingId, Floor floor)
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
            var update = Update.Push("floors", doc);

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
        public bool EditFloor(int buildingId, Floor floor)
        {
            var query = Query.And(Query.EQ("id", buildingId),
               Query.EQ("floors.id", floor.Id));

            var update = Update.Set("floors.$.number", floor.Number)
                .Set("floors.$.name", floor.Name)
                .Set("floors.$.buildArea", (BsonValue)floor.BuildArea)
                .Set("floors.$.usableArea", (BsonValue)floor.UsableArea)
                .Set("floors.$.imageUrl", floor.ImageUrl ?? "")
                .Set("floors.$.remark", floor.Remark ?? "");

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
        public bool DeleteFloor(int buildingId, int floorId)
        {
            var query = Query.And(Query.EQ("id", buildingId),
                Query.EQ("floors.id", floorId));

            var update = Update.Set("floors.$.status", 1);

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

            bool result = this.backupBusiness.Backup(EstateCollection.BuildingBackup, doc);
            return result;
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

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public bool Log(int id, Log log)
        {
            log = this.logBusiness.Insert(log);
            if (log == null)
                return false;

            var query = Query.EQ("id", id);
            var update = Update.Set("log.id", log._id)
                .Set("log.name", log.UserName)
                .Set("log.time", log.Time)
                .Set("log.type", log.Type);

            WriteConcernResult result = this.context.Update(EstateCollection.Building, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 归档楼宇
        /// </summary>
        /// <param name="log">相关日志</param>
        /// <returns></returns>
        public bool Archive(Log log)
        {
            log = this.logBusiness.Insert(log);
            if (log == null)
                return false;

            var docs = this.context.FindAll(EstateCollection.Building);
            List<BsonDocument> newDocs = new List<BsonDocument>();

            foreach (BsonDocument doc in docs)
            {
                doc.Remove("_id");
                doc.Remove("log");
                doc.Add("log", new BsonDocument
                {
                    { "id", log._id },
                    { "name", log.UserName },
                    { "time", log.RelateTime },
                    { "type", log.Type }
                });
                newDocs.Add(doc);
            }

            bool result = this.archiveBusiness.Archive(EstateCollection.BuildingBackup, newDocs);
            return result;
        }

        /// <summary>
        /// 导出楼宇
        /// </summary>
        /// <returns></returns>
        public byte[] Export()
        {
            StringBuilder sb = new StringBuilder();
            var docs = this.context.FindAll(EstateCollection.Building);

            sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                "Id", "名称", "图片", "所属楼群", "建筑面积", "使用面积", "地上楼层数", "地下楼层数",
                "使用类型", "局部导航", "排序", "备注", "编辑人", "编辑时间", "编辑类型", "状态"));

            foreach (var doc in docs)
            {
                BsonDocument editor = doc["editor"].AsBsonDocument;

                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                    doc["id"],
                    doc["name"],
                    doc.GetValue("imageUrl", ""),
                    doc.GetValue("buildingGroupId"),
                    doc.GetValue("buildArea", null),
                    doc.GetValue("usableArea", null),
                    doc.GetValue("aboveGroundFloor", null),
                    doc.GetValue("underGroundFloor", null),
                    doc.GetValue("useType"),
                    doc.GetValue("partMapUrl", ""),
                    doc.GetValue("sort"),
                    doc.GetValue("remark", ""),
                    editor.GetValue("name", ""),
                    editor.GetValue("time", null),
                    editor.GetValue("type", null),
                    doc.GetValue("status", 0)
                ));
            }

            byte[] fileContents = Encoding.Default.GetBytes(sb.ToString());
            return fileContents;
        }
        #endregion //Method
    }
}
