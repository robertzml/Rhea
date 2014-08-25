using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data;
using Rhea.Model;

namespace Rhea.Data.Mongo
{
    /// <summary>
    /// MongoDB 日志 Repository
    /// </summary>
    public class MongoLogRepository : ILogRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<Log> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 日志 Repository
        /// </summary>
        public MongoLogRepository()
        {
            this.repository = new MongoRepository<Log>(RheaServer.RheaDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="_id">日志ID</param>
        /// <returns></returns>
        public Log Get(string _id)
        {
            return this.repository.GetById(_id);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="data">日志对象</param>
        /// <returns></returns>
        public ErrorCode Create(Log data)
        {
            try
            {
                this.repository.Add(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="_id">日志ID</param>
        /// <returns></returns>
        public ErrorCode Delete(string _id)
        {
            try
            {
                this.repository.Delete(_id);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 日志数量
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return this.repository.Count();
        }
        #endregion //Method
    }
}
