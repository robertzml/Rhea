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
    /// 房产字典
    /// </summary>
    public class EstateDictionaryBusiness : DictionaryBusiness
    {
        #region Field        
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 房产字典
        /// </summary>
        public EstateDictionaryBusiness()
            :base(RheaServer.EstateDatabase, EstateCollection.Dictionary)
        {
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 得到房间功能列表
        /// </summary>
        /// <returns></returns>
        public List<RoomFunctionCode> GetRoomFunctionCode()
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
        /// 得到学院房间功能列表
        /// </summary>
        /// <remarks>1-7类</remarks>
        /// <returns></returns>
        public List<RoomFunctionCode> GetCollegeRoomFunctionCode()
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Dictionary, "name", "RoomFunctionCode");

            List<RoomFunctionCode> data = new List<RoomFunctionCode>();

            BsonArray array = doc["property"].AsBsonArray;
            for (int i = 0; i < array.Count; i++)
            {               
                BsonDocument d = array[i].AsBsonDocument;
                if (d["firstCode"].AsInt32 > 7)
                    continue;

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
        /// 更新房间功能列表
        /// </summary>
        /// <param name="data">功能属性</param>
        /// <returns></returns>
        public bool EditRoomFunctionCode(List<RoomFunctionCode> data)
        {
            BsonArray array = new BsonArray();
            foreach (var code in data)
            {
                BsonDocument doc = new BsonDocument
                {
                    { "codeId", code.CodeId },
                    { "firstCode", code.FirstCode },
                    { "secondCode", code.SecondCode },
                    { "classifyName", code.ClassifyName },
                    { "functionProperty", code.FunctionProperty },
                    { "remark", code.Remark }
                };
                array.Add(doc);
            }

            var query = Query.EQ("name", "RoomFunctionCode");
            var update = Update.Set("property", array);

            WriteConcernResult result = this.context.Update(EstateCollection.Dictionary, query, update);

            if (result.Ok)
                return true;
            else
                return false;
        }
        #endregion //Method
    }
}
