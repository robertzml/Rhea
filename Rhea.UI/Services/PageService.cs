using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Business.Account;
using Rhea.Model.Account;

namespace Rhea.UI.Services
{
    /// <summary>
    /// 页面服务
    /// </summary>
    public class PageService
    {
        #region Method
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static User GetCurrentUser(string userName)
        {
            UserBusiness userBusiness = new UserBusiness();
            var user = userBusiness.GetByUserName(userName);
            return user;
        }
        #endregion //Method
    }
}