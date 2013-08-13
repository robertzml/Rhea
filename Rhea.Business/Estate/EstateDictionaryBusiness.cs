using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Server;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房产字典
    /// </summary>
    public class EstateDictionaryBusiness : IDictionaryBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);
        #endregion //Field

        #region Method
        /// <summary>
        /// 得到字典集
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <returns></returns>
        public Dictionary<int, string> GetCombineDict(string name)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Dictionary, "name", name);

            if (doc == null)
                return null;

            BsonArray array = doc["property"].AsBsonArray;
            Dictionary<int, string> data = new Dictionary<int, string>();

            foreach (var item in array)
            {
                data.Add(item["id"].AsInt32, item["value"].AsString);
            }

            return data;
        }

        /// <summary>
        /// 得到非组合字典集
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <returns></returns>
        public string[] GetDict(string name)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Dictionary, "name", name);

            if (doc == null)
                return null;

            BsonArray array = doc["property"].AsBsonArray;
            string[] data = new string[array.Count];

            for (int i = 0; i < array.Count; i++)
            {
                data[i] = array[i].AsString;
            }

            return data;
        }

        /// <summary>
        /// 得到字典项值
        /// </summary>
        /// <param name="dictName">字典名称</param>
        /// <param name="id">字典项ID</param>
        /// <returns></returns>
        public string GetItemValue(string dictName, int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Dictionary, "name", dictName);

            if (doc == null)
                return null;

            BsonArray array = doc["property"].AsBsonArray;
            BsonValue item = array.First(r => r["id"].AsInt32 == id);
            string value = item["value"].AsString;
            return value;
        }
        #endregion //Method
    }
}
