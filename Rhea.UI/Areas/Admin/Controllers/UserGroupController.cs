using Rhea.Business.Account;
using Rhea.Common;
using Rhea.Model;
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
    /// 用户组控制器
    /// </summary>
    public class UserGroupController : Controller
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
        /// 用户组列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.userBusiness.GetUserGroup();
            return View(data);
        }

        /// <summary>
        /// 用户组详细
        /// </summary>
        /// <param name="id">用户组ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.userBusiness.GetUserGroup(id);
            return View(data);
        }

        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="model">用户组对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(UserGroup model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.userBusiness.CreateUserGroup(model);

                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "添加用户组成功";
                    return RedirectToAction("List", "UserGroup");
                }
                else
                {
                    ModelState.AddModelError("", "添加用户组失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}