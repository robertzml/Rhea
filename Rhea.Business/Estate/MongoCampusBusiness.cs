using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 校区业务类
    /// </summary>
    public class MongoCampusBusiness : ICampusBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context;

        /// <summary>
        /// 备份接口
        /// </summary>
        private IBackupBusiness backupBusiness;

        /// <summary>
        /// 日志接口
        /// </summary>
        private ILogBusiness logBusiness;

        /// <summary>
        /// 归档接口
        /// </summary>
        private IArchiveBusiness archiveBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 校区业务类
        /// </summary>
        public MongoCampusBusiness()
        {
            this.context = new RheaMongoContext(RheaServer.EstateDatabase);
            this.backupBusiness = new EstateBackupBusiness();
            this.logBusiness = new MongoLogBusiness();
            this.archiveBusiness = new EstateArchiveBusiness();
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Campus ModelBind(BsonDocument doc)
        {
            Campus campus = new Campus();
            campus.Id = doc["id"].AsInt32;
            campus.Name = doc["name"].AsString;
            campus.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            campus.Remark = doc.GetValue("remark", "").AsString;
            campus.Status = doc.GetValue("status", 0).AsInt32;

            if (doc.Contains("log"))
            {
                BsonDocument log = doc["log"].AsBsonDocument;
                campus.Log._id = log["id"].AsObjectId;
                campus.Log.UserName = log["name"].AsString;
                campus.Log.Time = log["time"].AsBsonDateTime.ToLocalTime();
                campus.Log.Type = log["type"].AsInt32;
            }

            return campus;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 得到校区列表
        /// </summary>
        /// <returns></returns>
        public List<Campus> GetList()
        {
            List<Campus> campusList = new List<Campus>();
            var docs = this.context.FindAll(EstateCollection.Campus);

            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Campus campus = ModelBind(doc);
                campusList.Add(campus);
            }

            return campusList;
        }

        /// <summary>
        /// 得到校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public Campus Get(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Campus, "id", id);

            if (doc != null)
            {
                Campus campus = ModelBind(doc);
                if (campus.Status == 1)
                    return null;

                return campus;
            }
            else
                return null;
        }

        /// <summary>
        /// 得到校区名称
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Campus, "id", id);

            if (doc != null)
                return doc["name"].AsString;
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取校区总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.context.Count(EstateCollection.Campus, query);
            return (int)count;
        }

        /// <summary>
        /// 备份楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public bool Backup(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Campus, "id", id);
            doc.Remove("_id");

            bool result = this.backupBusiness.Backup(EstateCollection.CampusBackup, doc);
            return result;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public bool Log(int id, Log log)
        {
            log = this.logBusiness.Insert(log);
            if (log == null)
                return false;

            var query = Query.EQ("id", id);
            var update = Update.Set("log.id", log._id)
                .Set("log.name", log.UserName)
                .Set("log.time", log.Time)
                .Set("log.type", log.Type);

            WriteConcernResult result = this.context.Update(EstateCollection.Campus, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 归档校区
        /// </summary>
        /// <param name="log">相关日志</param>
        /// <returns></returns>
        public bool Archive(Log log)
        {
            log = this.logBusiness.Insert(log);
            if (log == null)
                return false;

            var docs = this.context.FindAll(EstateCollection.Campus);
            List<BsonDocument> newDocs = new List<BsonDocument>();

            foreach (BsonDocument doc in docs)
            {
                doc.Remove("_id");
                doc.Remove("log");
                doc.Add("log", new BsonDocument
                {
                    { "id", log._id },
                    { "name", log.UserName },
                    { "time", log.RelateTime },
                    { "type", log.Type }
                });
                newDocs.Add(doc);
            }

            bool result = this.archiveBusiness.Archive(EstateCollection.CampusBackup, newDocs);
            return result;
        }
        #endregion //Method
    }
}
