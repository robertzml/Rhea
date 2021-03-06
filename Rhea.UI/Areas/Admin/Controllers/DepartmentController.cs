﻿using Rhea.Business.Personnel;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Personnel;
using Rhea.UI.Filters;
using Rhea.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Model.Account;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 部门控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class DepartmentController : Controller
    {
        #region Field
        /// <summary>
        /// 部门业务对象
        /// </summary>
        private DepartmentBusiness departmentBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (departmentBusiness == null)
            {
                departmentBusiness = new DepartmentBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.departmentBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 部门详细
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.departmentBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 部门编辑
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = this.departmentBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 部门编辑
        /// </summary>
        /// <param name="model">部门对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                //backup
                ErrorCode result = this.departmentBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份部门失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.departmentBusiness.Update(model);

                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "编辑部门失败";
                    ModelState.AddModelError("", result.DisplayName());
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑部门",
                    Time = DateTime.Now,
                    Type = (int)LogType.DepartmentEdit,
                    Content = string.Format("编辑部门, ID:{0}, 名称:{1}。", model.DepartmentId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.departmentBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑部门成功";
                return RedirectToAction("Details", new { controller = "Department", id = model.DepartmentId });
            }

            return View(model);
        }
        #endregion //Action
    }
}