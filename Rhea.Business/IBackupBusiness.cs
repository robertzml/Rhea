using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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
        /// 查找首次备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="id">备份对象ID</param>    
        /// <returns></returns>
        BsonDocument FindFirstBackup(string collectionName, BsonValue id);

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

        /// <summary>
        /// 查找备份
        /// </summary>
        /// <param name="collectionName">目标collection</param>
        /// <param name="query">查找条件</param>
        /// <returns></returns>
        MongoCursor<BsonDocument> FindBackup(string collectionName, IMongoQuery query);

        /// <summary>
        /// 归档数据
        /// </summary>
        /// <param name="collectionName">collection名称</param>
        /// <param name="docs">数据</param>
        /// <returns></returns>
        bool Archive(string collectionName, IEnumerable<BsonDocument> docs);
    }
}
