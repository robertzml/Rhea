using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Configuration;

namespace Rhea.Data.Server
{
    /// <summary>
    /// 系统数据库连接
    /// </summary>
    public class RheaMongoContext
    {
        #region Field
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string connectionString;// = "mongodb://210.28.24.171";

        /// <summary>
        /// 数据库名称
        /// </summary>
        private string databaseName;

        /// <summary>
        /// 数据库
        /// </summary>
        private MongoDatabase database;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 数据库连接
        /// </summary>
        public RheaMongoContext()
            : this("rhea")
        {
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <param name="databaseName">数据库</param>
        public RheaMongoContext(string databaseName)
        {
            this.databaseName = databaseName;
            this.LoadConnectionString();
            this.Open();
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 打开连接
        /// </summary>
        private void Open()
        {
            MongoClient client = new MongoClient(this.connectionString);
            MongoServer server = client.GetServer();
            this.database = server.GetDatabase(this.databaseName);
        }

        private void LoadConnectionString()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["mongoConnection"].ConnectionString;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 得到所有集合
        /// </summary>
        /// <returns></returns>
        public string[] GetCollections()
        {
            string[] names = this.database.GetCollectionNames().ToArray();
            return names;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public WriteConcernResult Insert(string collectionName, BsonDocument data)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            WriteConcernResult result = collection.Insert(data);
            return result;            
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public IEnumerable<WriteConcernResult> InsertBatch(string collectionName, BsonDocument[] data)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            IEnumerable<WriteConcernResult> result = collection.InsertBatch(data);
            return result;
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public BsonDocument FindOne(string collectionName, string key, BsonValue value)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            var query = Query.EQ(key, value);
            
            BsonDocument d = collection.FindOne(query);               
            return d;
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public List<BsonDocument> Find(string collectionName, string key, BsonValue value)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            var query = Query.EQ(key, value);            

            var data = collection.Find(query);

            List<BsonDocument> document = new List<BsonDocument>();
            foreach (BsonDocument d in data)
            {
                document.Add(d);
            }
            
            return document;
        }

        /// <summary>
        /// 根据主键查找
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public BsonDocument FindByKey(string collectionName, ObjectId id)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            var query = Query.EQ("_id", id);

            BsonDocument d = collection.FindOne(query);            
            return d;
        }

        /// <summary>
        /// 查找所有数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public List<BsonDocument> FindAll(string collectionName)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            
            List<BsonDocument> document = new List<BsonDocument>();
            foreach (BsonDocument d in collection.FindAll())
            {               
                document.Add(d);
            }
            
            return document;
        }

        /// <summary>
        /// 计数
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public long Count(string collectionName)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            long result = collection.Count();
            return result;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="document">数据</param>
        /// <returns></returns>
        public WriteConcernResult Save(string collectionName, BsonDocument document)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            WriteConcernResult result = collection.Save(document);            
            return result;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询</param>
        /// <param name="update">更新</param>
        /// <returns></returns>
        public WriteConcernResult Update(string collectionName, IMongoQuery query, IMongoUpdate update)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            WriteConcernResult result = collection.Update(query, update);
            return result;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="query">查询</param>
        /// <param name="update">更新</param>
        /// <returns></returns>
        public WriteConcernResult UpdateMulti(string collectionName, IMongoQuery query, IMongoUpdate update)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            WriteConcernResult result = collection.Update(query, update, UpdateFlags.Multi);
            return result;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="id">主键_id</param>
        /// <returns></returns>
        public WriteConcernResult Remove(string collectionName, ObjectId id)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            var query = Query.EQ("_id", id);
            WriteConcernResult result = collection.Remove(query);
            return result;
        }

        /// <summary>
        /// 聚集查询
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="pipeline">聚集</param>
        /// <returns></returns>
        public AggregateResult Aggregate(string collectionName, BsonDocument[] pipeline)
        {
            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            AggregateResult result = collection.Aggregate(pipeline); 
            return result;
        }

        /// <summary>
        /// 查找递增序列当前值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public int FindSequenceIndex(string collectionName)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$project", new BsonDocument {
                        { "id", 1 }
                    }}
                },
                new BsonDocument {
                    { "$sort", new BsonDocument {
                        { "id", -1 }
                    }}
                },
                new BsonDocument {
                    { "$limit", 1 }
                }
            };

            MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
            AggregateResult max = collection.Aggregate(pipeline);
            if (max.ResultDocuments.Count() == 0)
                return 0;
            else
            {
                int maxId = max.ResultDocuments.First()["id"].AsInt32;
                return maxId;
            }
        }
        #endregion //Method

        #region Property
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName
        {
            get
            {
                return this.databaseName;
            }
        }
        #endregion //Property
    }
}
