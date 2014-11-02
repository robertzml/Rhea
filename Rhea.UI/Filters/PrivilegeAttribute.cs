using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Rhea.Business.Account;
using Rhea.Model.Account;

namespace Rhea.UI.Filters
{
    /// <summary>
    /// 用户权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PrivilegeAttribute : AuthorizeAttribute
    {
        #region Field
        /// <summary>
        /// 要求级别
        /// </summary>
        private int rank = 0;

        /// <summary>
        /// 要求权限
        /// </summary>
        private string require = "";

        /// <summary>
        /// 用户业务对象
        /// </summary>
        private UserBusiness userBusiness;

        /// <summary>
        /// Root用户组ID
        /// </summary>
        private int rootId = 100001;
        #endregion //Field

        #region Function
        /// <summary>
        /// 根据用户检查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private bool CheckUser(HttpContextBase httpContext)
        {
            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                this.userBusiness = new UserBusiness();
                User user = this.userBusiness.GetByUserName(httpContext.User.Identity.Name);
                UserGroup group = this.userBusiness.GetUserGroup(user.UserGroupId);
                if (group.UserGroupId == this.rootId)
                    return true;

                if (group.UserGroupPrivilege.Any(r => r.Name == this.require))
                    return true;
                else
                    return false;
            }

            return false;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 用户检查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            bool result = CheckUser(httpContext);
            return result;
        }
        #endregion //Method

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

        /// <summary>
        /// 要求权限
        /// </summary>
        public string Require
        {
            get
            {
                return this.require;
            }
            set
            {
                this.require = value;
            }
        }
        #endregion //Property
    }
}