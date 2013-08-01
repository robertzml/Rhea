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
        #endregion //Property
    }
}