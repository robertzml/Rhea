using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Mongo;
using Rhea.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Rhea.Business
{
    /// <summary>
    /// 备份业务类
    /// </summary>
    public class BackupBusiness
    {
        #region Method
        /// <summary>
        /// 备份记录
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="collection">集合</param>
        /// <param name="doc">备份对象</param>
        /// <returns></returns>
        /// <remarks>
        /// 先取出原纪录，然后插入备份表中
        /// </remarks>
        public ErrorCode Backup(string database, string collection, string backupCollection, string _id)
        {
            MongoRepository repository = new MongoRepository(database, collection);

            ObjectId oid = new ObjectId(_id);
            BsonDocument doc = repository.Collection.FindOneById(oid);

            MongoRepository backupRepository = new MongoRepository(database, backupCollection);
            WriteConcernResult result = backupRepository.Collection.Insert(doc);
            if (!result.Ok)
                return ErrorCode.DatabaseWriteError;

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
