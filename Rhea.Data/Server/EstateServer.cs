using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace Rhea.Data.Server
{
    /// <summary>
    /// 房产服务器
    /// </summary>
    public class EstateServer
    {
         #region Field
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string connectionString = "mongodb://";

        private MongoServer server;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 服务器(本机)
        /// </summary>
        public EstateServer()
            : this("localhost")
        {
        }

        /// <summary>
        /// 服务器
        /// </summary>
        /// <param name="host">地址</param>
        public EstateServer(string host)
        {
            this.connectionString += host;
            Connect();
        }
        #endregion //Constructor

        #region Function
        private void Connect()
        {
            MongoClient client = new MongoClient(this.connectionString);
            server = client.GetServer();
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取已有数据库
        /// </summary>
        /// <returns></returns>
        public List<string> GetDatabaseList()
        {            
            List<string> names = server.GetDatabaseNames().ToList();
            return names;
        }

        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <returns></returns>
        public List<string> GetCollections(string databaseName)
        {
            List<string> names = server.GetDatabase(databaseName).GetCollectionNames().ToList();
            return names;
        }
        #endregion //Method
    }
}
