﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Business.Personnel;
using Rhea.Model.Account;
using Rhea.UI.Filters;
using Rhea.UI.Services;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [EnhancedAuthorize(Roles = "Root,Administrator")]
    public class AccountController : Controller
    {
        #region Field
        /// <summary>
        /// 管理业务
        /// </summary>
        private IAccountBusiness accountBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (accountBusiness == null)
            {
                accountBusiness = new MongoAccountBusiness();
            }

            base.Initialize(requestContext);
        }

        /// <summary>
        /// 检查是否有权限查看root
        /// </summary>
        /// <param name="user">查看用户</param>
        /// <returns></returns>
        private bool CheckRoot(UserProfile user)
        {
            bool isRoot = User.IsInRole2("Root");
            if (!isRoot && user.UserGroupName == "Root")
                return false;
            else
                return true;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 用户管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="groupId">用户组ID</param>
        /// <returns></returns>       
        public ActionResult List(int groupId = 0)
        {
            bool isRoot = User.IsInRole2("Root");
            if (groupId == 0)
            {
                var data = this.accountBusiness.GetList(isRoot);
                return View(data);
            }
            else
            {
                if (!isRoot && groupId == 100001)
                {
                    return RedirectToAction("Index", "Account");
                }

                var data = this.accountBusiness.GetList(groupId);
                return View(data);
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="id">用户系统ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.accountBusiness.Get(id);

            if (!CheckRoot(data))
                return RedirectToAction("Index", new { controller = "Account", area = "Admin" });

            if (data.DepartmentId == 0)
                ViewBag.DepartmentName = "";
            else
            {
                IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
                ViewBag.DepartmentName = departmentBusiness.GetName(data.DepartmentId);
            }
            return View(data);
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                string result = this.accountBusiness.Create(model);

                if (!string.IsNullOrEmpty(result))
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("List", "Account", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 添加统一身份认证用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateUnity()
        {
            return View();
        }

        /// <summary>
        /// 添加统一身份认证用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUnity(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.accountBusiness.CreateUnity(model);

                if (result)
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("List", "Account", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="id">用户系统ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var data = this.accountBusiness.Get(id);

            if (!CheckRoot(data))
                return RedirectToAction("Index", new { controller = "Account", area = "Admin" });

            return View(data);
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.accountBusiness.Edit(model);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "Account", new { area = "Admin", id = model.Id });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        public ActionResult Disable(string id)
        {
            bool result = this.accountBusiness.Disable(id);
            if (result)
            {
                TempData["Message"] = "禁用成功";
            }
            else
            {
                TempData["Message"] = "禁用失败";
            }

            return RedirectToAction("Details", "Account", new { area = "Admin", id = id });
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        public ActionResult Enable(string id)
        {
            bool result = this.accountBusiness.Enable(id);
            if (result)
            {
                TempData["Message"] = "启用成功";
            }
            else
            {
                TempData["Message"] = "启用失败";
            }

            return RedirectToAction("Details", "Account", new { area = "Admin", id = id });
        }
        #endregion //Action
    }
}