using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Rhea.Data.Server;

namespace Rhea.Business
{
    /// <summary>
    /// 字典业务
    /// </summary>
    public class DictionaryBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        protected RheaMongoContext context;

        /// <summary>
        /// 集合名称
        /// </summary>
        protected string collectionName;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 字典业务
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="collectionName">集合名称</param>
        protected DictionaryBusiness(string databaseName, string collectionName)
        {
            this.context = new RheaMongoContext(databaseName);
            this.collectionName = collectionName;
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 得到组合字典集
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <returns></returns>
        public Dictionary<int, string> GetCombineDict(string name)
        {
            BsonDocument doc = this.context.FindOne(this.collectionName, "name", name);

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
            BsonDocument doc = this.context.FindOne(this.collectionName, "name", name);

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
            BsonDocument doc = this.context.FindOne(this.collectionName, "name", dictName);

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
