using MongoDB.Driver;
using Rhea.Model;
using System;
using System.Configuration;

namespace Rhea.Data.Mongo
{
    /// <summary>
    /// 内部工具类
    /// </summary>
    /// <typeparam name="U">实体主键类型</typeparam>
    internal static class Util<U>
    {
        #region Field
        /// <summary>
        /// 连接字符串键值.
        /// </summary>
        private const string connectionstringName = "mongoConnection";
        #endregion //Field

        #region Function
        /// <summary>
        /// 根据Mongo地址获取数据库
        /// </summary>
        /// <param name="url">数据库URL(包含数据库名)</param>
        /// <returns>数据库对象</returns>
        private static MongoDatabase GetDatabase(MongoUrl url)
        {
            var client = new MongoClient(url);
            var server = client.GetServer();
            return server.GetDatabase(url.DatabaseName); // WriteConcern defaulted to Acknowledged
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="url">数据库URL</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns></returns>
        private static MongoDatabase GetDatabase(MongoUrl url, string databaseName)
        {
            var client = new MongoClient(url);
            var server = client.GetServer();
            return server.GetDatabase(databaseName);
        }

        /// <summary>
        /// 获取类型T的Collection名称
        /// </summary>
        /// <typeparam name="T">Collection类型</typeparam>
        /// <returns>Collection名称</returns>
        private static string GetCollectionName<T>() where T : IEntity<U>
        {
            string collectionName;
            if (typeof(T).BaseType.Equals(typeof(object)))
            {
                collectionName = GetCollectioNameFromInterface<T>();
            }
            else
            {
                collectionName = GetCollectionNameFromType(typeof(T));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }

        /// <summary>
        /// 根据CollectionName属性获取Collection名称
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>Collection名称</returns>
        private static string GetCollectioNameFromInterface<T>()
        {
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                collectionname = typeof(T).Name;
            }

            return collectionname;
        }

        /// <summary>
        /// 根据类型名称获取Collection名称
        /// </summary>
        /// <param name="entitytype">实体类型</param>
        /// <returns>Collection名称</returns>
        private static string GetCollectionNameFromType(Type entitytype)
        {
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(entitytype, typeof(CollectionName));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                // No attribute found, get the basetype
                while (!entitytype.BaseType.Equals(typeof(MongoEntity)))
                {
                    entitytype = entitytype.BaseType;
                }

                collectionname = entitytype.Name;
            }

            return collectionname;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取配置文件连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConfigConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[connectionstringName].ConnectionString;
        }

        /// <summary>
        /// 获取Collection
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connectionString">连接字符串，包含数据库名</param>
        /// <returns></returns>
        public static MongoCollection<T> GetCollection<T>(string connectionString)
            where T : IEntity<U>
        {
            return Util<U>.GetDatabase(new MongoUrl(connectionString)).GetCollection<T>(GetCollectionName<T>());
        }

        /// <summary>
        /// 获取Collection
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名</param>
        /// <returns>Collection对象</returns>
        public static MongoCollection<T> GetCollection<T>(string connectionString, string databaseName)
            where T : IEntity<U>
        {
            return Util<U>.GetCollection<T>(connectionString, databaseName, GetCollectionName<T>());
        }

        /// <summary>
        /// 获取Collection
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="collectionName">Collection名称</param>
        /// <returns>Collection对象</returns>
        public static MongoCollection<T> GetCollection<T>(string connectionString, string databaseName, string collectionName)
            where T : IEntity<U>
        {
            return Util<U>.GetDatabase(new MongoUrl(connectionString), databaseName).GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 获取Collection
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="url">连接字符串，包含数据库名</param>
        /// <returns>Collection对象</returns>
        public static MongoCollection<T> GetCollection<T>(MongoUrl url)
            where T : IEntity<U>
        {
            return Util<U>.GetCollection<T>(url, GetCollectionName<T>());
        }

        /// <summary>
        /// 获取Collection
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="url">连接字符串，包含数据库名</param>
        /// <param name="collectionName">Collection名称</param>
        /// <returns>Collection对象</returns>
        public static MongoCollection<T> GetCollection<T>(MongoUrl url, string collectionName)
            where T : IEntity<U>
        {
            return Util<U>.GetDatabase(url).GetCollection<T>(collectionName);
        }
        #endregion //Method
    }
}
