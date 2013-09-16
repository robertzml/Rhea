﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Log ModelBind(BsonDocument doc)
        {
            Log log = new Log();
            log._id = doc["_id"].AsObjectId;
            log.Title = doc["title"].AsString;
            log.Content = doc.GetValue("content", "").AsString;
            log.Time = doc["time"].AsBsonDateTime.ToLocalTime();
            log.UserId = doc["userId"].AsObjectId;
            log.UserName = doc["userName"].AsString;
            log.Type = doc["type"].AsInt32;
            log.Tag = doc.GetValue("tag", "").AsString;

            BsonValue rtime = doc.GetValue("relateTime", null);
            if (rtime == null || rtime.IsBsonNull)
                log.RelateTime = null;
            else
                log.RelateTime = rtime.AsBsonDateTime.ToLocalTime();         

            return log;
        }
        #endregion //Function

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
                { "relateTime", (BsonValue)log.RelateTime },
                { "tag", log.Tag }
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

        /// <summary>
        /// 得到日志
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns></returns>
        public Log Get(string id)
        {
            ObjectId _id = new ObjectId(id);
            var doc = this.context.FindByKey(RheaCollection.Log, _id);
            if (doc != null)
            {
                Log log = ModelBind(doc);
                return log;
            }
            else
                return null;
        }

        /// <summary>
        /// 得到日志
        /// </summary>
        /// <param name="relateTime">相关时间</param>
        /// <param name="type">日志类型</param> 
        /// <returns></returns>
        /// <remarks>通过归档日期找到相关日志</remarks>
        public Log Get(DateTime relateTime, int type)
        {
            var query = Query.And(Query.EQ("type", type), Query.EQ("relateTime", relateTime));
            var result = this.context.Find(RheaCollection.Log, query);

            if (result.Count() == 0)
                return null;
            else
            {
                Log log = ModelBind(result.First());
                return log;
            }
        }

        /// <summary>
        /// 显示所有日志
        /// </summary>
        /// <returns></returns>
        public List<Log> GetList()
        {
            List<Log> data = new List<Log>();

            var docs = this.context.FindAll(RheaCollection.Log);

            foreach (BsonDocument doc in docs)
            {
                Log log = ModelBind(doc);
                data.Add(log);
            }

            return data;
        }

        /// <summary>
        /// 显示所有日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <returns></returns>
        public List<Log> GetList(int type)
        {
            List<Log> data = new List<Log>();

            var query = Query.EQ("type", type);
            var docs = this.context.Find(RheaCollection.Log, query).SetSortOrder(SortBy.Ascending("time"));

            foreach (BsonDocument doc in docs)
            {
                Log log = ModelBind(doc);
                data.Add(log);
            }

            return data;
        }

        /// <summary>
        /// 显示所有日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <returns></returns>
        public List<Log> GetList(int[] type)
        {
            List<Log> data = new List<Log>();

            BsonArray array = new BsonArray();
            array.AddRange(type);

            var query = Query.In("type", array);
            var docs = this.context.Find(RheaCollection.Log, query).SetSortOrder(SortBy.Ascending("time"));

            foreach (BsonDocument doc in docs)
            {
                Log log = ModelBind(doc);
                data.Add(log);
            }

            return data;
        }
        #endregion //Method
    }
}