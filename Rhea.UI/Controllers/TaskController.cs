﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Plugin;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Plugin;
using Rhea.UI.Services;
using Rhea.UI.Filters;

namespace Rhea.UI.Controllers
{
    /// <summary>
    /// 任务控制器
    /// </summary>
    [EnhancedAuthorize]
    public class TaskController : Controller
    {
        #region Field
        /// <summary>
        /// 任务业务
        /// </summary>
        private TaskBusiness taskBusiness;
        #endregion //Field

        #region Constructor
        public TaskController()
        {
            this.taskBusiness = new TaskBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 任务主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            User user = PageService.GetCurrentUser(User.Identity.Name);
            var data = this.taskBusiness.GetByUser(user._id);

            return View(data);
        }

        /// <summary>
        /// 任务信息
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.taskBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model">任务模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Task model)
        {
            if (ModelState.IsValid)
            {
                User user = PageService.GetCurrentUser(User.Identity.Name);

                model.CreateTime = DateTime.Now;
                model.UserId = user._id;
                model.Status = 0;

                var result = this.taskBusiness.Create(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "添加任务失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                Log log = new Log
                {
                    Title = "添加任务",
                    Time = DateTime.Now,
                    Type = (int)LogType.TaskCreate,
                    Content = string.Format("添加任务, 标题:{0}，提醒时间:{1}。", model.Title, model.RemindTime),
                    UserId = user._id,
                    UserName = user.Name
                };
                LogBusiness logBusiness = new LogBusiness();
                result = logBusiness.Create(log);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                return RedirectToAction("List");
            }

            return View(model);
        }

        /// <summary>
        /// 关闭任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        public ActionResult Close(string id)
        {
            var result = this.taskBusiness.Close(id);
            if (result == ErrorCode.Success)
            {
                TempData["Message"] = "任务已关闭";
            }
            else
            {
                TempData["Message"] = "任务关闭失败";
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var data = this.taskBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm()
        {
            string id = Request.Form["_id"];
            Task task = this.taskBusiness.Get(id);
            string title = task.Title;

            ErrorCode result = this.taskBusiness.Delete(id);

            if (result == ErrorCode.Success)
            {
                User user = PageService.GetCurrentUser(User.Identity.Name);

                //log
                Log log = new Log
                {
                    Title = "删除任务",
                    Time = DateTime.Now,
                    Type = (int)LogType.TaskDelete,
                    Content = string.Format("删除任务, 标题:{0}。", title),
                    UserId = user._id,
                    UserName = user.Name
                };
                LogBusiness logBusiness = new LogBusiness();
                result = logBusiness.Create(log);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View("Delete", id);
                }

                return RedirectToAction("List");
            }
            else
            {
                TempData["Message"] = "任务删除失败";
                return View("Delete", id);
            }
        }
        #endregion //Action
    }
}