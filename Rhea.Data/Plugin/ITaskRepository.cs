using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model;
using Rhea.Model.Plugin;

namespace Rhea.Data.Plugin
{
    public interface ITaskRepository
    {
        /// <summary>
        /// 获取用户任务列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IEnumerable<Task> GetByUser(string userId);

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="_id">任务ID</param>
        /// <returns></returns>
        Task Get(string _id);

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="data">任务对象</param>
        /// <returns></returns>
        ErrorCode Create(Task data);

        /// <summary>
        /// 编辑任务
        /// </summary>
        /// <param name="data">任务对象</param>
        /// <returns></returns>
        ErrorCode Update(Task data);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="_id">任务ID</param>
        /// <returns></returns>
        ErrorCode Delete(string _id);
    }
}
