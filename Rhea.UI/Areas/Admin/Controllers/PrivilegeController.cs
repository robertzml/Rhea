using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Account;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root")]
    public class PrivilegeController : Controller
    {
        #region Field
        /// <summary>
        /// 权限业务对象
        /// </summary>
        private PrivilegeBusiness privilegeBusiness;
        #endregion //Field

        #region Constructor
        public PrivilegeController()
        {
            this.privilegeBusiness = new PrivilegeBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.privilegeBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Allocate()
        {
            UserBusiness userBusiness = new UserBusiness();
            var userGroups = userBusiness.GetUserGroup().ToList();
            userGroups.Insert(0, new UserGroup { UserGroupId = 0, Name = "--- 请选择 ---" });

            ViewBag.UserGroups = userGroups;

            var data = this.privilegeBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AllocateSave()
        {
            int id = Convert.ToInt32(Request.Form["userGroupId"]);
            string privileges = Request.Form["privilege"];

            UserBusiness userBusiness = new UserBusiness();
            ErrorCode result = userBusiness.UpdateUserGroupPrivilege(id, privileges);

            if (result == ErrorCode.Success)
            {
                TempData["Message"] = "编辑权限成功";
                return RedirectToAction("List", "Privilege");
            }
            else
            {
                ModelState.AddModelError("", "编辑权限失败");
                ModelState.AddModelError("", result.DisplayName());
            }

            return View("Allocate");
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Privilege model)
        {
            if (ModelState.IsValid)
            {               
                ErrorCode result = this.privilegeBusiness.Create(model);

                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "添加权限成功";
                    return RedirectToAction("List", "Privilege");
                }
                else
                {
                    ModelState.AddModelError("", "添加权限失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}