using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Server;
using Rhea.Model;

namespace Rhea.Business
{
    public class MongoLogBusiness : ILogBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);
        #endregion //Field

        #region Method
        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public Log Insert(Log log)
        {
            BsonDocument doc = new BsonDocument
            {
                { "title", log.Title },
                { "content", log.Content },
                { "time", log.Time },
                { "userId", log.UserId },
                { "userName", log.UserName },
                { "type", log.Type },
                { "relateTime", (BsonValue)log.RelateTime }
            };

            WriteConcernResult result = this.context.Insert(RheaCollection.Log, doc);
            if (result.Ok)
            {
                log._id = doc["_id"].AsObjectId;
                return log;
            }
            else
                return null;
        }
        #endregion //Method
    }
}
