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
    /// 房间业务类
    /// </summary>
    public class MongoRoomBusiness : IRoomBusiness
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

            if (doc.Contains("editor"))
            {
                BsonDocument editor = doc["editor"].AsBsonDocument;
                room.Editor.Id = editor["id"].AsObjectId.ToString();
                room.Editor.Name = editor["name"].AsString;
                room.Editor.Time = editor["time"].AsBsonDateTime.ToLocalTime();
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
            List<BsonDocument> docs = this.context.FindAll(EstateCollection.Room);

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
        /// <param name="user">相关用户</param>
        /// <returns>房间ID，0:添加失败</returns>
        public int Create(Room data, UserProfile user)
        {
            data.Id = this.context.FindSequenceIndex(EstateCollection.Room) + 1;

            BsonDocument doc = new BsonDocument //42
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
                { "editor.id", user._id },
                { "editor.name", user.UserName },
                { "editor.time", DateTime.Now },
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
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        public bool Edit(Room data, UserProfile user)
        {
            var query = Query.EQ("id", data.Id);

            var update = Update.Set("name", data.Name)  //40
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
                .Set("editor.id", user._id)
                .Set("editor.name", user.UserName)
                .Set("editor.time", DateTime.Now)
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
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        public bool Delete(int id, UserProfile user)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("status", 1)
                .Set("editor.id", user._id)
                .Set("editor.name", user.UserName)
                .Set("editor.time", DateTime.Now);

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
            var query = Query.EQ("buildingId", buildingId);
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
            var query = Query.And(Query.EQ("buildingId", buildingId), Query.EQ("floor", floor));
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
            var query = Query.EQ("departmentId", departmentId);
            long count = this.context.Count(EstateCollection.Room, query);
            return (int)count;
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

            WriteConcernResult result = this.context.Insert(EstateCollection.RoomBackup, doc);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }
        #endregion //Method
    }
}
