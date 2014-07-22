using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Account;
using Rhea.Model;

namespace Rhea.Data.Account
{
    /// <summary>
    /// 用户组Repository
    /// </summary>
    public interface IUserGroupRepository
    {
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserGroup> Get();

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
    }
}
