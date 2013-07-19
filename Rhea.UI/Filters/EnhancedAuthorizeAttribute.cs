using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Rhea.UI.Filters
{
    /// <summary>
    /// 自定义用户验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class EnhancedAuthorizeAttribute : AuthorizeAttribute
    {
        #region Property
        /// <summary>
        /// 用户角色
        /// </summary>
        public string Roles2 = "";
        #endregion //Property

        #region Function
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
                string[] userRoles = authTicket.UserData.Split(',');
                string[] roles = Roles.Split(',');
                return roles.Any(x => userRoles.Contains(x));
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
                string[] userRoles = fi.Ticket.UserData.Split(',');
                string[] actionRoles = Roles.Split(',');
                if (actionRoles.Any(r => userRoles.Contains(r)))
                    return true; //base.AuthorizeCore(httpContext);
                else
                    return false;
            }

            return false;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            bool result = checkByUser(httpContext);
            return result;
        }
        #endregion //Function
    }
}