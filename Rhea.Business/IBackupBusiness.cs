using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Rhea.Business
{
    /// <summary>
    /// 备份业务
    /// </summary>
    public interface IBackupBusiness
    {
        /// <summary>
        /// 备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="doc">信息</param>
        /// <returns></returns>
        bool Backup(string collectionName, BsonDocument doc);

        /// <summary>
        /// 查找备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="id">备份对象ID</param>    
        /// <returns></returns>
        List<BsonDocument> FindBackup(string collectionName, BsonValue id);

        /// <summary>
        /// 查找备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="id">备份对象ID</param>
        /// <param name="type">信息编辑类型</param>
        /// <returns></returns>
        List<BsonDocument> FindBackup(string collectionName, BsonValue id, int type);
    }
}
