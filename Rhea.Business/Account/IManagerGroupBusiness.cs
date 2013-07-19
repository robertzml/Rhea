using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Account;

namespace Rhea.Business.Account
{
    public interface IManagerGroupBusiness
    {
        /// <summary>
        /// 获取管理组列表
        /// </summary>
        /// <returns></returns>
        List<ManagerGroup> GetList();

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="id">管理组ID</param>
        /// <returns></returns>
        ManagerGroup Get(int id);

        /// <summary>
        /// 管理组添加
        /// </summary>
        /// <param name="data">管理组数据</param>
        /// <returns></returns>
        int Create(ManagerGroup data);

        /// <summary>
        /// 管理组编辑
        /// </summary>
        /// <param name="data">管理组数据</param>
        /// <returns></returns>
        bool Edit(ManagerGroup data);
    }
}
