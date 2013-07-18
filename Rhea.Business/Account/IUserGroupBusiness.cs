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
    }
}
