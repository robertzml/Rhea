using Rhea.Business.Account;
using Rhea.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : Controller
    {
        #region Field
        /// <summary>
        /// 用户业务对象
        /// </summary>
        private UserBusiness userBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (userBusiness == null)
            {
                userBusiness = new UserBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.userBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 用户详细
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.userBusiness.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}