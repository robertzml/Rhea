using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business
{
    /// <summary>
    /// 账户业务
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        User ValidateUser(string userName, string password);
    }
}
