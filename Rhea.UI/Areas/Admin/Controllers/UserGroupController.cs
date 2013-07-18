using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;

namespace Rhea.UI.Areas.Admin.Controllers
{
    public class UserGroupController : Controller
    {
        #region Field
        /// <summary>
        /// 用户组业务
        /// </summary>
        private IUserGroupBusiness userGroupBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (userGroupBusiness == null)
            {
                userGroupBusiness = new MongoUserGroupBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.userGroupBusiness.GetList();
            return View(data);
        }
        #endregion //Action
    }
}
