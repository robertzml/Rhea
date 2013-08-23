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
    /// 房间业务类
    /// </summary>
    public class MongoRoomBusiness : IRoomBusiness
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
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 房间业务类
        /// </summary>
        public MongoRoomBusiness()
        {
            this.context = new RheaMongoContext(RheaServer.EstateDatabase);
            this.backupBusiness = new EstateBackupBusiness();
            this.logBusiness = new MongoLogBusiness();
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Room ModelBind(BsonDocument doc)
        {
            Room room = new Room();
            room.Id = doc["id"].AsInt32;
            room.Name = doc["name"].AsString;
            room.Number = doc["number"].AsString;
            room.Floor = doc["floor"].AsInt32;
            room.Span = (double?)doc.GetValue("span", null);
            room.Orientation = doc.GetValue("orientation", "").AsString;
            room.BuildingId = doc["buildingId"].AsInt32;
            room.DepartmentId = doc["departmentId"].AsInt32;
            room.BuildArea = (double?)doc.GetValue("buildArea", null);
            room.UsableArea = (double?)doc.GetValue("usableArea", null);
            room.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            room.StartDate = (DateTime?)doc.GetValue("startDate", null);
            room.FixedYear = (int?)doc.GetValue("fixedYear", null);
            //room.Manager = doc.GetValue("manager", "").AsString;
            room.PersonNumber = (int?)doc.GetValue("personNumber", null);
            room.RoomStatus = doc.GetValue("roomStatus", "").AsString;
            room.Remark = doc.GetValue("remark", "").AsString;
            room.Status = doc.GetValue("status", 0).AsInt32;

            room.Heating = (bool?)doc.GetValue("heating", null);
            room.FireControl = doc.GetValue("fireControl", "").AsString;
            room.Height = (double?)doc.GetValue("height", null);
            room.EWWidth = (double?)doc.GetValue("ewWidth", null);
            room.SNWidth = (double?)doc.GetValue("snWidth", null);
            room.InternationalId = (int?)doc.GetValue("internationalId", null);
            room.EducationId = (int?)doc.GetValue("educationId", null);
            room.PowerSupply = doc.GetValue("powerSupply", "").AsString;
            room.AirCondition = doc.GetValue("airCondition", "").AsString;
            room.HasSecurity = (bool?)doc.GetValue("hasSecurity", null);
            room.HasChemical = (bool?)doc.GetValue("hasChemical", null);
            room.HasTrash = doc.GetValue("hasTrash", "").AsString;
            room.HasSecurityCheck = (bool?)doc.GetValue("hasSecurityCheck", null);
            room.PressureContainer = (int?)doc.GetValue("pressureContainer", null);
            room.Cylinder = (int?)doc.GetValue("cylinder", null);
            room.HeatingInAeration = (bool?)doc.GetValue("heatingInAeration", null);
            room.HasTestBed = (bool?)doc.GetValue("hasTestBed", null);
            room.UsageCharge = (double?)doc.GetValue("usageCharge", null);

            if (doc.Contains("function"))
            {
                BsonDocument fun = doc["function"].AsBsonDocument;
                room.Function.FirstCode = fun["firstCode"].AsInt32;
                room.Function.SecondCode = fun["secondCode"].AsInt32;
                room.Function.ClassifyName = fun["classifyName"].AsString;
                room.Function.Property = fun["property"].AsString;
            }
            else
                room.Function = null;

            if (doc.Contains("log"))
            {
                BsonDocument log = doc["log"].AsBsonDocument;
                room.Log._id = log["id"].AsObjectId;
                room.Log.UserName = log["name"].AsString;
                room.Log.Time = log["time"].AsBsonDateTime.ToLocalTime();
                room.Log.Type = log["type"].AsInt32;
            }

            if (room.StartDate != null)
                room.StartDate = ((DateTime)room.StartDate).ToLocalTime();

            return room;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns></returns>
        public List<Room> GetList()
        {
            List<Room> data = new List<Room>();
            var docs = this.context.FindAll(EstateCollection.Room);

            foreach (BsonDocument doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Room room = ModelBind(doc);
                data.Add(room);
            }

            return data;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public List<Room> GetListByBuildingGroup(int buildingGroupId)
        {
            var query = Query.EQ("buildingGroupId", buildingGroupId);
            var buildings = this.context.Find(EstateCollection.Building, query).SetFields("id");

            BsonArray bids = new BsonArray();
            foreach (var building in buildings)
            {
                bids.Add(building["id"]);
            }

            query = Query.In("buildingId", bids);

            var docs = this.context.Find(EstateCollection.Room, query);

            List<Room> rooms = new List<Room>();
            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Room room = ModelBind(doc);
                rooms.Add(room);
            }

            return rooms;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public List<Room> GetListByBuilding(int buildingId)
        {
            List<BsonDocument> docs = this.context.Find(EstateCollection.Room, "buildingId", buildingId);

            List<Room> rooms = new List<Room>();
            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Room room = ModelBind(doc);
                rooms.Add(room);
            }

            return rooms;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public List<Room> GetListByBuilding(int buildingId, int floor)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "buildingId", buildingId },
                        { "floor", floor }
                    }}}
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);
            List<Room> data = new List<Room>();
            foreach (var r in result.ResultDocuments)
            {
                if (r.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Room room = ModelBind(r);
                data.Add(room);
            }

            return data;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        public List<Room> GetListByFloor(int floorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public List<Room> GetListByDepartment(int departmentId)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "departmentId", departmentId }
                    }}}
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);
            List<Room> data = new List<Room>();
            foreach (var r in result.ResultDocuments)
            {
                if (r.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Room room = ModelBind(r);
                data.Add(room);
            }

            return data;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public List<Room> GetListByDepartment(int departmentId, int buildingId)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "departmentId", departmentId },
                        { "buildingId", buildingId }
                    }}}
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);
            List<Room> data = new List<Room>();
            foreach (var r in result.ResultDocuments)
            {
                if (r.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Room room = ModelBind(r);
                data.Add(room);
            }

            return data;
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public Room Get(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Room, "id", id);

            if (doc != null)
            {
                Room room = ModelBind(doc);
                return room;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="data">房间数据</param>   
        /// <returns>房间ID，0:添加失败</returns>
        public int Create(Room data)
        {
            data.Id = this.context.FindSequenceIndex(EstateCollection.Room) + 1;

            BsonDocument doc = new BsonDocument //39
            {
                { "id", data.Id },                
                { "name" , data.Name },   
                { "number", data.Number },
                { "floor", data.Floor },
                { "span", (BsonValue)data.Span },
                { "orientation", (BsonValue)data.Orientation },
                { "buildArea", (BsonValue)data.BuildArea },
                { "usableArea", (BsonValue)data.UsableArea },
                { "imageUrl", (BsonValue)data.ImageUrl },
                { "function", new BsonDocument {
                    { "firstCode", data.Function.FirstCode },
                    { "secondCode", data.Function.SecondCode },
                    { "classifyName", data.Function.ClassifyName },
                    { "property", data.Function.Property }
                }},
                { "buildingId", data.BuildingId },
                { "departmentId", data.DepartmentId },
                { "startDate", (BsonValue)data.StartDate },
                { "fixedYear", (BsonValue)data.FixedYear },
                //{ "manager", data.Manager ?? ""},
                { "personNumber", (BsonValue)data.PersonNumber },
                { "roomStatus", data.RoomStatus },
                { "remark", data.Remark ?? "" },                
                { "status", 0 },
                { "heating", (BsonValue)data.Heating },
                { "fireControl", data.FireControl ?? "" },
                { "height", (BsonValue)data.Height },
                { "ewWidth", (BsonValue)data.EWWidth },
                { "snWidth", (BsonValue)data.SNWidth },
                { "internationalId", (BsonValue)data.InternationalId },
                { "educationId", (BsonValue)data.EducationId },
                { "powerSupply", data.PowerSupply ?? "" },
                { "airCondition", data.AirCondition ?? "" },
                { "hasSecurity", (BsonValue)data.HasSecurity },
                { "hasChemical", (BsonValue)data.HasChemical },
                { "hasTrash", data.HasTrash ?? "" },
                { "hasSecurityCheck", (BsonValue)data.HasSecurityCheck },
                { "pressureContainer", (BsonValue)data.PressureContainer },
                { "cylinder", (BsonValue)data.Cylinder },
                { "heatingInAeration", (BsonValue)data.HeatingInAeration },
                { "hasTestBed", (BsonValue)data.HasTestBed },
                { "usageCharge", (BsonValue)data.UsageCharge }
            };

            WriteConcernResult result = this.context.Insert(EstateCollection.Room, doc);

            if (result.HasLastErrorMessage)
                return 0;
            else
                return data.Id;
        }

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="data">房间数据</param>     
        /// <returns></returns>
        public bool Edit(Room data)
        {
            var query = Query.EQ("id", data.Id);

            var update = Update.Set("name", data.Name)  //37
                .Set("number", data.Number)
                .Set("floor", data.Floor)
                .Set("span", (BsonValue)data.Span)
                .Set("orientation", data.Orientation ?? "")
                .Set("buildArea", (BsonValue)data.BuildArea)
                .Set("usableArea", (BsonValue)data.UsableArea)
                .Set("imageUrl", data.ImageUrl ?? "")
                .Set("function.firstCode", data.Function.FirstCode)
                .Set("function.secondCode", data.Function.SecondCode)
                .Set("function.classifyName", data.Function.ClassifyName)
                .Set("function.property", data.Function.Property)
                .Set("buildingId", data.BuildingId)
                .Set("departmentId", data.DepartmentId)
                .Set("startDate", (BsonValue)data.StartDate)
                .Set("fixedYear", (BsonValue)data.FixedYear)
                //.Set("manager", data.Manager ?? "")
                .Set("personNumber", (BsonValue)data.PersonNumber)
                .Set("roomStatus", data.RoomStatus)
                .Set("remark", data.Remark ?? "")
                .Set("heating", (BsonValue)data.Heating)
                .Set("fireControl", data.FireControl ?? "")
                .Set("height", (BsonValue)data.Height)
                .Set("ewWidth", (BsonValue)data.EWWidth)
                .Set("snWidth", (BsonValue)data.SNWidth)
                .Set("internationalId", (BsonValue)data.InternationalId)
                .Set("educationId", (BsonValue)data.EducationId)
                .Set("powerSupply", data.PowerSupply ?? "")
                .Set("airCondition", data.AirCondition ?? "")
                .Set("hasSecurity", (BsonValue)data.HasSecurity)
                .Set("hasChemical", (BsonValue)data.HasChemical)
                .Set("hasTrash", data.HasTrash ?? "")
                .Set("hasSecurityCheck", (BsonValue)data.HasSecurityCheck)
                .Set("pressureContainer", (BsonValue)data.PressureContainer)
                .Set("cylinder", (BsonValue)data.Cylinder)
                .Set("heatingInAeration", (BsonValue)data.HeatingInAeration)
                .Set("hasTestBed", (BsonValue)data.HasTestBed)
                .Set("usageCharge", (BsonValue)data.UsageCharge);

            WriteConcernResult result = this.context.Update(EstateCollection.Room, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("status", 1);

            WriteConcernResult result = this.context.Update(EstateCollection.Room, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取房间属性列表
        /// </summary>
        /// <returns></returns>
        public List<RoomFunctionCode> GetFunctionCodeList()
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Dictionary, "name", "RoomFunctionCode");

            List<RoomFunctionCode> data = new List<RoomFunctionCode>();

            BsonArray array = doc["property"].AsBsonArray;
            for (int i = 0; i < array.Count; i++)
            {
                BsonDocument d = array[i].AsBsonDocument;
                RoomFunctionCode code = new RoomFunctionCode
                {
                    CodeId = d["codeId"].AsString,
                    FirstCode = d["firstCode"].AsInt32,
                    SecondCode = d["secondCode"].AsInt32,
                    ClassifyName = d["classifyName"].AsString,
                    FunctionProperty = d["functionProperty"].AsString,
                    Remark = d["remark"].AsString
                };
                data.Add(code);
            }

            return data;
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.context.Count(EstateCollection.Room, query);
            return (int)count;
        }

        /// <summary>
        /// 楼宇内房间数量
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public int CountByBuilding(int buildingId)
        {
            var query = Query.And(Query.EQ("buildingId", buildingId), Query.NE("status", 1));
            long count = this.context.Count(EstateCollection.Room, query);
            return (int)count;
        }

        /// <summary>
        /// 楼层内房间数量
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public int CountByFloor(int buildingId, int floor)
        {
            var query = Query.And(Query.EQ("buildingId", buildingId), Query.EQ("floor", floor), Query.NE("status", 1));
            long count = this.context.Count(EstateCollection.Room, query);
            return (int)count;
        }

        /// <summary>
        /// 部门房间数量
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public int CountByDepartment(int departmentId)
        {
            var query = Query.And(Query.EQ("departmentId", departmentId), Query.NE("status", 1));
            long count = this.context.Count(EstateCollection.Room, query);
            return (int)count;
        }

        /// <summary>
        /// 导出房间
        /// </summary>
        /// <returns></returns>
        public byte[] Export()
        {
            StringBuilder sb = new StringBuilder();
            var docs = this.context.FindAll(EstateCollection.Room);

            sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}," +
                "{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}," +
                "{33},{34},{35},{36},{37},{38}",
                "Id", "房间名称", "房间编号", "楼层", "跨数", "朝向", "建筑面积", "使用面积", "图片", "功能编码",
                "所属楼宇", "所属部门", "开始使用日期", "使用期限", "房间总人数", "房间状态", "备注",
                "供热情况", "消防情况", "房间高度", "房间东西长度", "房间南北长度", "国际分类编号", "教育部分类编号",
                "供电情况", "空调情况", "是否有安全制度", "是否有危险化学品", "是否有废液处理", "是否有安全教育检查",
                "压力容器数量", "钢瓶数量", "通风是否有取暖", "是否有试验台", "使用费用",
                "编辑人", "编辑时间", "编辑类型", "状态"));

            foreach (var doc in docs)
            {
                BsonDocument function = doc["function"].AsBsonDocument;
                BsonDocument editor = doc["editor"].AsBsonDocument;

                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}," +
                    "{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32}," +
                    "{33},{34},{35},{36},{37},{38}",
                    doc["id"],
                    doc["name"],
                    doc["number"],
                    doc["floor"],
                    doc.GetValue("span", null),
                    doc.GetValue("orientation", ""),
                    doc.GetValue("buildArea", null),
                    doc.GetValue("usableArea", null),
                    doc.GetValue("imageUrl", ""),
                    function["firstCode"].ToString() + "." + function["secondCode"].ToString(),
                    doc["buildingId"],
                    doc["departmentId"],
                    doc.GetValue("startDate", null),
                    doc.GetValue("fixedYear", null),
                    doc.GetValue("personNumber", null),
                    doc.GetValue("roomStatus", ""),
                    doc.GetValue("remark", ""),
                    doc.GetValue("heating", null),
                    doc.GetValue("fireControl", ""),
                    doc.GetValue("height", null),
                    doc.GetValue("ewWidth", null),
                    doc.GetValue("snWidth", null),
                    doc.GetValue("internationalId", null),
                    doc.GetValue("educationId", null),
                    doc.GetValue("powerSupply", ""),
                    doc.GetValue("airCondition", ""),
                    doc.GetValue("hasSecurity", null),
                    doc.GetValue("hasChemical", null),
                    doc.GetValue("hasTrash", ""),
                    doc.GetValue("hasSecurityCheck", null),
                    doc.GetValue("pressureContainer", null),
                    doc.GetValue("cylinder", null),
                    doc.GetValue("heatingInAeration", null),
                    doc.GetValue("hasTestBed", null),
                    doc.GetValue("usageCharge", null),
                    editor.GetValue("name", ""),
                    editor.GetValue("time", null),
                    editor.GetValue("type", null),
                    doc.GetValue("status", 0)
                ));
            }

            byte[] fileContents = Encoding.Default.GetBytes(sb.ToString());
            return fileContents;
        }

        /// <summary>
        /// 备份房间
        /// </summary>
        /// <param name="id">房间ID</param>   
        /// <returns></returns>
        public bool Backup(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Room, "id", id);
            doc.Remove("_id");

            bool result = this.backupBusiness.Backup(EstateCollection.RoomBackup, doc);
            return result;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="id">房间ID</param>
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

            WriteConcernResult result = this.context.Update(EstateCollection.Room, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 归档房间
        /// </summary>
        /// <param name="log">相关日志</param>
        /// <returns></returns>
        public bool Archive(Log log)
        {
            log = this.logBusiness.Insert(log);
            if (log == null)
                return false;

            var docs = this.context.FindAll(EstateCollection.Room);
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

            bool result = this.backupBusiness.Archive(EstateCollection.RoomArchive, newDocs);
            return result;
        }

        /// <summary>
        /// 分配房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="newDepartmentId">新部门ID</param>
        /// <returns></returns>
        public bool Assign(int id, int newDepartmentId)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("departmentId", newDepartmentId);

            WriteConcernResult result = this.context.Update(EstateCollection.Room, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 查找分配历史
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public List<Room> GetAssignHistory(int id)
        {
            List<Room> data = new List<Room>();
            int assignType = 10;

            var currentDoc = this.context.FindOne(EstateCollection.Room, "id", id);
            Room current = ModelBind(currentDoc);
            if (current.Log.Type == assignType)
                data.Add(current);

            var backups = this.backupBusiness.FindBackup(EstateCollection.RoomBackup, id, assignType);
            foreach (var doc in backups)
            {
                Room room = ModelBind(doc);
                data.Add(room);
            }

            var first = this.backupBusiness.FindFirstBackup(EstateCollection.RoomBackup, id);
            if (first != null && data.Count != 0)
            {
                Room firstRoom = ModelBind(first);
                if (firstRoom.Log.Type != assignType)
                    data.Add(firstRoom);
            }

            return data;
        }
        #endregion //Method
    }
}
