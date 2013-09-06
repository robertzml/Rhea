using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 人事备份类
    /// </summary>
    public class PersonnelBackupBusiness : IBackupBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.PersonnelDatabase);
        #endregion //Field

        #region Method
        /// <summary>
        /// 备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="doc">信息</param>
        /// <returns></returns>
        public bool Backup(string collectionName, BsonDocument doc)
        {
            WriteConcernResult result = this.context.Insert(collectionName, doc);

            if (result.Ok)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 查找首次备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="id">备份对象ID</param>
        /// <returns></returns>
        public BsonDocument FindFirstBackup(string collectionName, BsonValue id)
        {
            var query = Query.EQ("id", id);
            var data = this.context.Find(collectionName, query);
            data = data.SetSortOrder(SortBy.Ascending("log.time")).SetLimit(1);

            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 查找备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="id">备份对象ID</param>
        /// <returns></returns>
        public List<BsonDocument> FindBackup(string collectionName, BsonValue id)
        {
            var data = this.context.Find(collectionName, "id", id);
            return data;
        }

        /// <summary>
        /// 查找备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="id">备份对象ID</param>
        /// <param name="type">信息编辑类型</param>
        /// <returns></returns>
        public List<BsonDocument> FindBackup(string collectionName, BsonValue id, int type)
        {
            var query = Query.And(Query.EQ("id", id),
                Query.EQ("log.type", type));

            var data = this.context.Find(collectionName, query).SetSortOrder(SortBy.Descending("log.time"));
            return data.ToList();
        }

        /// <summary>
        /// 查找备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="query">查找条件</param>
        /// <returns></returns>
        public MongoCursor<BsonDocument> FindBackup(string collectionName, IMongoQuery query)
        {
            var data = this.context.Find(collectionName, query);
            return data;
        }

        /// <summary>
        /// 归档数据
        /// </summary>
        /// <param name="collectionName">collection名称</param>
        /// <param name="docs">数据</param>
        /// <returns></returns>
        public bool Archive(string collectionName, IEnumerable<BsonDocument> docs)
        {
            IEnumerable<WriteConcernResult> result = this.context.InsertBatch(collectionName, docs);

            if (result.All(r => r.Ok == true))
                return true;
            else
                return false;
        }
        #endregion //Method
    }
}
