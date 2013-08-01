using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Account;

namespace Rhea.Business.Account
{
    public interface IUserGroupBusiness
    {
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        List<UserGroup> GetList();

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="id">用户组ID</param>
        /// <returns></returns>
        UserGroup Get(int id);

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="name">用户组名称</param>
        /// <returns></returns>
        UserGroup Get(string name);

        /// <summary>
        /// 用户组添加
        /// </summary>
        /// <param name="data">用户组数据</param>
        /// <returns></returns>
        int Create(UserGroup data);

        /// <summary>
        /// 用户组编辑
        /// </summary>
        /// <param name="data">用户组数据</param>
        /// <returns></returns>
        bool Edit(UserGroup data);
    }
}
