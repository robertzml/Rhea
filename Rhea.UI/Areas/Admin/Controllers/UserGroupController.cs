using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Model.Account;

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

        /// <summary>
        /// 用户组信息
        /// </summary>
        /// <param name="id">用户组ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.userGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 用户组编辑
        /// </summary>
        /// <param name="id">管理组ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = this.userGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 用户组编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(UserGroup model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.userGroupBusiness.Edit(model);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "UserGroup", new { area = "Admin", id = model.Id });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}
