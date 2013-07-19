using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Rhea.Model.Account;

namespace Rhea.Business.Account
{
    /// <summary>
    /// 账户业务
    /// </summary>
    public interface IAccountBusiness
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserProfile Login(string userName, string password);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        UserProfile Get(string id);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        UserProfile GetByUserName(string userName);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        List<UserProfile> GetList();

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns></returns>
        bool Edit(UserProfile data);

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool ValidatePassword(string userName, string password);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
