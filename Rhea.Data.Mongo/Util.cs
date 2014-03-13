using MongoDB.Driver;
using System;
using System.Configuration;

namespace Rhea.Data.Mongo
{
    /// <summary>
    /// 内部工具类
    /// </summary>
    /// <typeparam name="U"></typeparam>
    internal static class Util<U>
    {
        /// <summary>
        /// App.config, Web.config 连接字符串键值.
        /// </summary>
        private const string DefaultConnectionstringName = "mongoConnection";

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[DefaultConnectionstringName].ConnectionString;
        }

        /// <summary>
        /// 根据Mongo地址获取数据库
        /// </summary>
        /// <param name="url">数据库URL</param>
        /// <returns>数据库对象</returns>
        private static MongoDatabase GetDatabaseFromUrl(MongoUrl url)
        {
            var client = new MongoClient(url);
            var server = client.GetServer();
            return server.GetDatabase(url.DatabaseName); // WriteConcern defaulted to Acknowledged
        }

        /// <summary>
        /// 根据连接字符串获取Collection
        /// </summary>
        /// <typeparam name="T">Collection类型</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>Collection对象</returns>
        public static MongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString)
            where T : IEntity<U>
        {
            return Util<U>.GetCollectionFromConnectionString<T>(connectionString, GetCollectionName<T>());
        }

        /// <summary>
        /// 根据连接字符串获取Collection
        /// </summary>
        /// <typeparam name="T">Collection类型</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="collectionName">Collection名称</param>
        /// <returns>Collection对象</returns>
        public static MongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString, string collectionName)
            where T : IEntity<U>
        {
            return Util<U>.GetDatabaseFromUrl(new MongoUrl(connectionString))
                .GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 根据MongoDB Url获取Collection
        /// </summary>
        /// <typeparam name="T">Collection类型</typeparam>
        /// <param name="url">MongoDB Url</param>
        /// <returns>Collection对象</returns>
        public static MongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url)
            where T : IEntity<U>
        {
            return Util<U>.GetCollectionFromUrl<T>(url, GetCollectionName<T>());
        }

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        public static MongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url, string collectionName)
            where T : IEntity<U>
        {
            return Util<U>.GetDatabaseFromUrl(url)
                .GetCollection<T>(collectionName);
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
        /// 根据特定类型获取Collection名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
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
        /// 根据特定类型获取Collection名称
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
                while (!entitytype.BaseType.Equals(typeof(Entity)))
                {
                    entitytype = entitytype.BaseType;
                }

                collectionname = entitytype.Name;
            }

            return collectionname;
        }
    }
}
