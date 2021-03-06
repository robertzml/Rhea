﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Plugin;
using Rhea.Data.Mongo.Plugin;
using Rhea.Model;
using Rhea.Model.Plugin;

namespace Rhea.Business.Plugin
{
    /// <summary>
    /// 任务业务类
    /// </summary>
    public class TaskBusiness
    {
        #region Field
        /// <summary>
        /// 任务Repository
        /// </summary>
        private ITaskRepository taskRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 任务业务类
        /// </summary>
        public TaskBusiness()
        {
            this.taskRepository = new MongoTaskRepository();
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
            return this.taskRepository.GetByUser(userId);
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="_id">任务ID</param>
        /// <returns></returns>
        public Task Get(string _id)
        {
            return this.taskRepository.Get(_id);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="data">任务对象</param>
        /// <returns></returns>
        public ErrorCode Create(Task data)
        {
            return this.taskRepository.Create(data);
        }

        /// <summary>
        /// 获取未完成任务
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IEnumerable<Task> GetOpen(string userId)
        {
            var data = this.taskRepository.GetByUser(userId).Where(r => r.Status == 0).OrderBy(r => r.RemindTime);
            return data;
        }

        /// <summary>
        /// 关闭任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        public ErrorCode Close(string id)
        {
            var data = this.taskRepository.Get(id);
            data.CloseTime = DateTime.Now;
            data.Status = (int)EntityStatus.Closed;

            return this.taskRepository.Update(data);
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ErrorCode Delete(string id)
        {
            return this.taskRepository.Delete(id);
        }
        #endregion //Method
    }
}
