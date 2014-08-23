using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.UI.Filters;
using Rhea.UI.Services;
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
    [EnhancedAuthorize(Rank = 900)]
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
            bool isRoot = User.IsInRole2("Root");

            var data = this.userBusiness.Get(isRoot).ToList();
            data.ForEach(r => r.AvatarSmall = RheaConstant.AvatarRoot + r.AvatarSmall);
            return View(data);
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="groupId">所属用户组ID</param>
        /// <returns></returns>
        public ActionResult ListByGroup(int groupId)
        {
            bool isRoot = User.IsInRole2("Root");

            var data = this.userBusiness.GetByGroup(groupId, isRoot).ToList();
            data.ForEach(r => r.AvatarSmall = RheaConstant.AvatarRoot + r.AvatarSmall);

            var group = this.userBusiness.GetUserGroup(groupId);
            ViewBag.GroupName = group.Name;

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

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="Model">用户对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.userBusiness.Create(model);

                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "添加用户成功";
                    return RedirectToAction("List", "User");
                }
                else
                {
                    ModelState.AddModelError("", "添加用户失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            bool isRoot = User.IsInRole2("Root");
            var data = this.userBusiness.Get(id);

            if (!isRoot && data.UserGroupId == 100001)
                return View("List");

            return View(data);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model">用户对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.userBusiness.Update(model);

                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑用户成功";
                    return RedirectToAction("Details", "User", new { id = model._id });
                }
                else
                {
                    ModelState.AddModelError("", "编辑用户失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public ActionResult Enable(string id)
        {
            this.userBusiness.Enable(id);
            return RedirectToAction("Details", "User", new { id = id });
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public ActionResult Disable(string id)
        {
            this.userBusiness.Disable(id);
            return RedirectToAction("Details", "User", new { id = id });
        }
        #endregion //Action
    }
}