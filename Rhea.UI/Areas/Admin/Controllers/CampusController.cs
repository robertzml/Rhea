using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Estate;
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
    /// 校区控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class CampusController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private CampusBusiness campusBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (campusBusiness == null)
            {
                campusBusiness = new CampusBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 校区列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            //var data = this.campusBusiness.Get();
            //return View(data);
            return View();
        }

        /// <summary>
        /// 校区信息
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.campusBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 添加校区
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加校区
        /// </summary>
        /// <param name="model">校区对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Campus model)
        {
            if (ModelState.IsValid)
            {
                //create
                model.Status = 0;
                ErrorCode result = this.campusBusiness.Create(model);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "添加校区失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "添加校区",
                    Time = DateTime.Now,
                    Type = (int)LogType.CampusCreate,
                    Content = string.Format("添加校区, ID:{0}, 名称:{1}。", model.CampusId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.campusBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "添加校区成功";
                return RedirectToAction("Details", new { controller = "Campus", id = model.CampusId });
            }

            return View(model);
        }

        /// <summary>
        /// 校区编辑
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = this.campusBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 校区编辑
        /// </summary>
        /// <param name="model">校区对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Campus model)
        {
            if (ModelState.IsValid)
            {
                //backup
                ErrorCode result = this.campusBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份校区失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.campusBusiness.Update(model);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "编辑校区失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "编辑校区",
                    Time = DateTime.Now,
                    Type = (int)LogType.CampusEdit,
                    Content = string.Format("编辑校区: {0}。", model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.campusBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑校区成功";
                return RedirectToAction("Details", new { controller = "Campus", id = model.CampusId });
            }

            return View(model);
        }
        #endregion //Action
    }
}