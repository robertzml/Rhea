using Rhea.Data;
using Rhea.Model;
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
    /// MongoDB 字典Repository类
    /// </summary>
    public class MongoDictionaryRepository : IDictionaryRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<Dictionary> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 字典Repository类
        /// </summary>
        public MongoDictionaryRepository()
        {
            this.repository = new MongoRepository<Dictionary>(RheaServer.RheaDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns>返回简单型字典集</returns>
        public IEnumerable<Dictionary> Get()
        {
            return this.repository;
        }

        /// <summary>
        /// 获取字典集
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        public Dictionary Get(string name)
        {
            return this.repository.Where(r => r.Name == name).First();
        }

        /// <summary>
        /// 获取文本属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        public List<String> GetTextProperty(string name)
        {
            List<string> data = new List<string>();

            MongoRepository repository = new MongoRepository(RheaServer.RheaDatabase, RheaCollection.Dictionary);

            var query = Query.EQ("name", name);
            var doc = repository.Collection.FindOne(query);

            if (doc["type"].AsInt32 != (int)DictionaryType.Text)
                return data;

            if (!doc.Contains("property"))
                return data;

            BsonArray array = doc["property"].AsBsonArray;
            foreach (BsonString row in array)
            {
                data.Add(row.AsString);
            }

            return data;
        }

        /// <summary>
        /// 获取键值属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        public Dictionary<int, string> GetPairProperty(string name)
        {
            Dictionary<int, string> data = new Dictionary<int, string>();

            MongoRepository repository = new MongoRepository(RheaServer.RheaDatabase, RheaCollection.Dictionary);

            var query = Query.EQ("name", name);
            var doc = repository.Collection.FindOne(query);

            if (doc["type"].AsInt32 != (int)DictionaryType.Pair)
                return data;

            if (!doc.Contains("property"))
                return data;

            BsonArray array = doc["property"].AsBsonArray;
            foreach (BsonDocument row in array)
            {
                data.Add(row["key"].AsInt32, row["value"].AsString);
            }

            return data;
        }

        /// <summary>
        /// 添加字典集
        /// </summary>
        /// <param name="data">字典对象</param>
        /// <returns></returns>
        public ErrorCode Create(Dictionary data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.Name == data.Name);
                if (dup)
                    return ErrorCode.DuplicateName;

                this.repository.Add(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 保存字典集
        /// </summary>
        /// <param name="data">字典对象</param>
        /// <returns></returns>
        public ErrorCode Edit(Dictionary data)
        {
            try
            {
                this.repository.Update(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 更新文本属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <param name="property">属性</param>
        /// <returns></returns>
        public ErrorCode UpdateTextProperty(string name, List<string> property)
        {
            try
            {
                MongoRepository repository = new MongoRepository(RheaServer.RheaDatabase, RheaCollection.Dictionary);

                BsonArray array = new BsonArray();
                foreach (var item in property)
                {
                    array.Add(item);
                }

                var query = Query.EQ("name", name);
                var update = Update.Set("property", array);

                WriteConcernResult result = repository.Collection.Update(query, update);

                if (result.Ok)
                    return ErrorCode.Success;
                else
                    return ErrorCode.DatabaseWriteError;
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }
        }

        /// <summary>
        /// 更新键值属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <param name="property">属性</param>
        /// <returns></returns>
        public ErrorCode UpdatePairProperty(string name, Dictionary<int, string> property)
        {
            try
            {
                MongoRepository repository = new MongoRepository(RheaServer.RheaDatabase, RheaCollection.Dictionary);

                BsonArray array = new BsonArray();
                foreach (var item in property)
                {
                    array.Add(new BsonDocument
                    {
                        { "key", item.Key },
                        { "value", item.Value }
                    });
                }

                var query = Query.EQ("name", name);
                var update = Update.Set("property", array);

                WriteConcernResult result = repository.Collection.Update(query, update);

                if (result.Ok)
                    return ErrorCode.Success;
                else
                    return ErrorCode.DatabaseWriteError;
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }
        }
        #endregion //Method
    }
}
