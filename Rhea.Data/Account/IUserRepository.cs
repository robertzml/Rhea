using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;
using Rhea.Model.Account;

namespace Rhea.Data.Account
{
    /// <summary>
    /// 用户Repository接口
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> Get();

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="_id">用户系统ID</param>
        /// <returns></returns>
        User Get(string _id);

        /// <summary>
        /// 根据登录名获取用户
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <returns></returns>
        User GetByUserName(string userName);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <returns></returns>
        ErrorCode Create(User data);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <returns></returns>
        ErrorCode Update(User data);
    }
}
