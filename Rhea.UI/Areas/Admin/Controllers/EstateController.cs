using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.UI.Areas.Admin.Models;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [EnhancedAuthorize(Rank = 600)]
    public class EstateController : Controller
    {
        #region Function
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        private UserProfile GetUser()
        {
            IAccountBusiness accountBusiness = new MongoAccountBusiness();
            UserProfile user = accountBusiness.GetByUserName(User.Identity.Name);
            return user;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房产管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导出任务
        /// </summary>
        /// <returns></returns>
        public ActionResult Export()
        {
            return View();
        }

        /// <summary>
        /// 归档任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Archive()
        {
            return View();
        }

        /// <summary>
        /// 归档任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Archive(ArchiveModel model)
        {
            if (ModelState.IsValid)
            {
                var user = GetUser();
                Log log = new Log
                {
                    Content = model.ArchiveContent,
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    RelateTime = model.ArchiveDate
                };

                bool result = false;
                switch (model.ArchiveType)
                {
                    case 1:
                        log.Title = "归档校区";
                        log.Type = 23;
                        ICampusBusiness campusBusiness = new MongoCampusBusiness();
                        result = campusBusiness.Archive(log);
                        break;
                    case 2:
                        log.Title = "归档楼群";
                        log.Type = 22;
                        IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
                        result = buildingGroupBusiness.Archive(log);
                        break;
                    case 3:
                        log.Title = "归档楼宇";
                        log.Type = 21;
                        IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
                        result = buildingBusiness.Archive(log);
                        break;
                    case 4:
                        log.Title = "归档房间";
                        log.Type = 20;
                        IRoomBusiness roomBusiness = new MongoRoomBusiness();
                        result = roomBusiness.Archive(log);
                        break;
                }

                if (!result)
                {
                    ModelState.AddModelError("", "归档失败");
                }
                else
                {
                    TempData["Message"] = "归档成功";
                    return RedirectToAction("ArchiveList");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 归档列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ArchiveList()
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            int[] types = { 20, 21, 22, 23 };
            var data = logBusiness.GetList(types);
            return View(data);
        }

        /// <summary>
        /// 日志列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LogList()
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            var data = logBusiness.GetList().OrderBy(r => r.Time);

            return View(data);
        }
        #endregion //Action
    }
}
