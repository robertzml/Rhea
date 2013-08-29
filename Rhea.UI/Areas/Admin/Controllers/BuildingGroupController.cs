using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理 - 楼群管理
    /// </summary>
    [EnhancedAuthorize(Rank = 600)]
    public class BuildingGroupController : Controller
    {
        #region Field
        /// <summary>
        /// 楼群业务
        /// </summary>
        private IBuildingGroupBusiness buildingGroupBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingGroupBusiness == null)
            {
                buildingGroupBusiness = new MongoBuildingGroupBusiness();
            }

            base.Initialize(requestContext);
        }

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
        /// 楼群列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.buildingGroupBusiness.GetList().OrderBy(r => r.Id);
            return View(data);
        }

        /// <summary>
        /// 楼群详细
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            BuildingGroup data = this.buildingGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 添加楼群
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加楼群
        /// POST: /Admin/BuildingGroup/Create/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(BuildingGroup model)
        {
            if (ModelState.IsValid)
            {
                //create
                int bid = this.buildingGroupBusiness.Create(model);
                if (bid == 0)
                {
                    ModelState.AddModelError("", "添加失败");
                    return View(model);
                }

                //log
                var user = GetUser();
                Log log = new Log
                {
                    Title = "添加楼群",
                    Content = string.Format("添加楼群, ID:{0}, 名称:{1}.", bid, model.Name),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = 2
                };

                bool logok = this.buildingGroupBusiness.Log(bid, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    return View(model);
                }

                TempData["Message"] = "添加成功";
                return RedirectToAction("Details", "BuildingGroup", new { area = "Admin", id = bid });
            }

            return View(model);
        }

        /// <summary>
        /// 编辑楼群
        /// GET: /Admin/BuildingGroup/Edit/7
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            BuildingGroup data = this.buildingGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 编辑楼群
        /// POST: /Admin/BuildingGroup/Edit/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(BuildingGroup model)
        {
            if (ModelState.IsValid)
            {
                //backup
                bool backok = this.buildingGroupBusiness.Backup(model.Id);
                if (!backok)
                {
                    ModelState.AddModelError("", "备份失败");
                    return View(model);
                }

                //edit
                bool result = this.buildingGroupBusiness.Edit(model);
                if (!result)
                {
                    ModelState.AddModelError("", "保存失败");
                    return View(model);
                }

                //log
                var user = GetUser();
                Log log = new Log
                {
                    Title = "编辑楼群",
                    Content = string.Format("编辑楼群, ID:{0}, 名称:{1}.", model.Id, model.Name),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = 3
                };

                bool logok = this.buildingGroupBusiness.Log(model.Id, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    return View(model);
                }

                TempData["Message"] = "编辑成功";
                return RedirectToAction("Details", "BuildingGroup", new { area = "Admin", id = model.Id });
            }

            return View(model);
        }

        /// <summary>
        /// 删除楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = this.buildingGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 删除楼群
        /// POST: /Admin/BuilidngGroup/Delete/7
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //backup
            bool backok = this.buildingGroupBusiness.Backup(id);
            if (!backok)
            {
                TempData["Message"] = "备份失败";
                return View("Delete", id);
            }

            //delete
            bool result = this.buildingGroupBusiness.Delete(id);
            if (!result)
            {
                TempData["Message"] = "删除失败";
                return View("Delete", id);
            }

            //log
            var user = GetUser();
            string bgName = this.buildingGroupBusiness.GetName(id);
            Log log = new Log
            {
                Title = "删除楼群",
                Content = string.Format("删除楼群, ID:{0}, 名称:{1}.", id, bgName),
                Time = DateTime.Now,
                UserId = user._id,
                UserName = user.Name,
                Type = 4
            };

            bool logok = this.buildingGroupBusiness.Log(id, log);
            if (!logok)
            {
                TempData["Message"] = "记录日志失败";
                return View("Delete", id);
            }

            TempData["Message"] = "删除成功";
            return RedirectToAction("List", "BuildingGroup", new { area = "Admin" });
        }

        /// <summary>
        /// 导出楼群
        /// </summary>
        /// <returns></returns>
        public FileResult Export()
        {
            byte[] fileContents = this.buildingGroupBusiness.Export();
            return File(fileContents, "application/ms-excel", "buildingGroup.csv");
        }
        #endregion //Action
    }
}
