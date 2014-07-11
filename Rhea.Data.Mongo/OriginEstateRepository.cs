using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Rhea.Data.Mongo
{
    /// <summary>
    /// 原始房产系统Repository
    /// </summary>
    public class OriginEstateRepository
    {
        #region Field
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string connectionString = "mongodb://202.195.145.230/";

        /// <summary>
        /// 数据库
        /// </summary>
        private MongoDatabase database;
        #endregion //Field

        #region Constructor
        public OriginEstateRepository()
        {
        }
        #endregion //Constructor

        #region Function
        private void Open(string databaseName)
        {
            MongoClient client = new MongoClient(this.connectionString);
            MongoServer server = client.GetServer();
            this.database = server.GetDatabase(databaseName);
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取楼群列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetBuildingGroupList()
        {
            Open("estate");
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>("buildingGroup");

            var query = Query.NE("status", 1);
            var docs = collection.Find(query);

            Dictionary<int, string> data = new Dictionary<int, string>();

            foreach (BsonDocument doc in docs)
            {
                int id = doc["id"].AsInt32;
                string name = doc["name"].AsString;
                data.Add(id, name);
            }

            return data;
        }

        /// <summary>
        /// 获取原始楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public BsonDocument GetBuildingGroup(int id)
        {
            Open("estate");
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>("buildingGroup");

            var query = Query.EQ("id", id);
            var doc = collection.FindOne(query);

            return doc;
        }

        /// <summary>
        /// 获取原始楼宇列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetBuildingList()
        {
            Open("estate");
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>("building");

            var query = Query.NE("status", 1);
            var docs = collection.Find(query);

            Dictionary<int, string> data = new Dictionary<int, string>();

            foreach (BsonDocument doc in docs)
            {
                int id = doc["id"].AsInt32;
                string name = doc["name"].AsString;
                data.Add(id, name);
            }

            return data;
        }

        /// <summary>
        /// 获取原始楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public BsonDocument GetBuilding(int id)
        {
            Open("estate");
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>("building");

            var query = Query.EQ("id", id);
            var doc = collection.FindOne(query);

            return doc;
        }
        #endregion //Method
    }
}
