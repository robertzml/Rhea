using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Rhea.Business.Account;

namespace Rhea.UI.Filters
{
    /// <summary>
    /// 自定义用户验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class EnhancedAuthorizeAttribute : AuthorizeAttribute
    {
        #region Field
        /// <summary>
        /// 要求级别
        /// </summary>
        private int rank = 0;
        #endregion //Field

        #region Function
        /// <summary>
        /// 判断角色
        /// </summary>
        /// <param name="userRoles">用户角色</param>
        /// <param name="actionRoles">要求角色</param>
        /// <returns></returns>
        private bool CheckRole(string[] userRoles, string[] actionRoles)
        {
            return actionRoles.Any(r => userRoles.Contains(r));
        }

        /// <summary>
        /// 判断级别
        /// </summary>
        /// <param name="userRoles">用户角色</param>
        /// <returns></returns>
        private bool CheckRank(string[] userRoles)
        {
            IUserGroupBusiness business = new MongoUserGroupBusiness();
            var group = business.Get(userRoles[0]);

            if (group == null)
                return false;
            else
                return group.Rank >= rank;
        }

        /// <summary>
        /// 根据cookie检查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private bool CheckByCookie(HttpContextBase httpContext)
        {
            //获得当前的验证cookie  
            HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            //如果当前的cookie为空，则返回。  
            if (authCookie == null || authCookie.Value == "")
            {
                return false;
            }
            FormsAuthenticationTicket authTicket;
            try
            {
                //对当前的cookie进行解密  
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return false;
            }

            if (authTicket != null)
            {
                bool result;
                string[] userRoles = authTicket.UserData.Split(',');
                string[] roles = Roles.Split(',');

                if (string.IsNullOrEmpty(Roles))
                    result = true;
                else
                    result = CheckRole(userRoles, roles);

                return result;
            }
            return false;
        }

        /// <summary>
        /// 根据用户检查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private bool checkByUser(HttpContextBase httpContext)
        {
            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                FormsIdentity fi = (FormsIdentity)httpContext.User.Identity;

                bool result;
                string[] userRoles = fi.Ticket.UserData.Split(',');
                string[] actionRoles = Roles.Split(',');

                if (string.IsNullOrEmpty(Roles))
                    result = true;
                else
                    result = CheckRole(userRoles, actionRoles);

                if (rank != 0)
                    result &= CheckRank(userRoles);

                return result;
            }

            return false;
        }

        /// <summary>
        /// 用户检查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            bool result = checkByUser(httpContext);
            return result;
        }
        #endregion //Function

        #region Property
        /// <summary>
        /// 要求级别
        /// </summary>
        public int Rank
        {
            get
            {
                return this.rank;
            }
            set
            {
                this.rank = value;
            }
        }
        #endregion //Property
    }
}