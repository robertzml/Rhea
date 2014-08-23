using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Common;
using Rhea.Data;
using Rhea.Data.Mongo;
using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business
{
    /// <summary>
    /// 日志业务类
    /// </summary>
    public class LogBusiness
    {
        #region Field
        /// <summary>
        /// 日志Repository
        /// </summary>
        private ILogRepository logRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 日志业务类
        /// </summary>
        public LogBusiness()
        {
            this.logRepository = new MongoLogRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log> Get()
        {
            return this.logRepository.Get();
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns></returns>
        public Log Get(string id)
        {
            return this.logRepository.Get(id);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="data">日志对象</param>
        /// <returns></returns>
        public ErrorCode Create(Log data)
        {
            return this.logRepository.Create(data);
        }

        /// <summary>
        /// 更新记录日志
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="collection">集合</param>
        /// <param name="_id">对象ID</param>
        /// <param name="data">日志对象</param>
        /// <returns></returns>
        public ErrorCode Log(string database, string collection, string _id, Log data)
        {
            MongoRepository repository = new MongoRepository(database, collection);
            ObjectId oid = new ObjectId(_id);

            var query = Query.EQ("_id", oid);
            var update = Update.Set("log._id", data._id)
                .Set("log.title", data.Title)
                .Set("log.content", data.Content)
                .Set("log.time", data.Time)
                .Set("log.userId", data.UserId)
                .Set("log.userName", data.UserName)
                .Set("log.type", data.Type)
                .Set("log.tag", data.Tag == null ? "" : data.Tag)
                .Set("log.remark", data.Remark == null ? "" : data.Remark);

            WriteConcernResult result = repository.Collection.Update(query, update);
            if (!result.Ok)
                return ErrorCode.DatabaseWriteError;

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
