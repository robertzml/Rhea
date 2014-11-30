using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Plugin;
using Rhea.Model;
using Rhea.Model.Plugin;

namespace Rhea.Data.Mongo.Plugin
{
    /// <summary>
    /// MongoDB 任务 Repository
    /// </summary>
    public class MongoTaskRepository : ITaskRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<Task> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 任务 Repository
        /// </summary>
        public MongoTaskRepository()
        {
            this.repository = new MongoRepository<Task>(RheaServer.RheaDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取用户任务列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IEnumerable<Task> GetByUser(string userId)
        {
            return this.repository.Where(r => r.UserId == userId);
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="_id">任务ID</param>
        /// <returns></returns>
        public Task Get(string _id)
        {
            return this.repository.GetById(_id);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="data">任务对象</param>
        /// <returns></returns>
        public ErrorCode Create(Task data)
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
        /// 编辑任务
        /// </summary>
        /// <param name="data">任务对象</param>
        /// <returns></returns>
        public ErrorCode Update(Task data)
        {
            try
            {
                this.repository.Update(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="_id">任务ID</param>
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
        #endregion //Method
    }
}
