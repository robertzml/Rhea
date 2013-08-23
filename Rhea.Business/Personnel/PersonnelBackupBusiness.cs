using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Server;

namespace Rhea.Business.Personnel
{
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
            throw new NotImplementedException();
        }

        public BsonDocument FindFirstBackup(string collectionName, BsonValue id)
        {
            throw new NotImplementedException();
        }

        public List<BsonDocument> FindBackup(string collectionName, BsonValue id)
        {
            throw new NotImplementedException();
        }

        public List<BsonDocument> FindBackup(string collectionName, BsonValue id, int type)
        {
            throw new NotImplementedException();
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
