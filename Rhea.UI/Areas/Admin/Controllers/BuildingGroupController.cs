using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Filters;
using Rhea.Model.Account;
using Rhea.Business.Account;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [EnhancedAuthorize]
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
            List<BuildingGroup> data = this.buildingGroupBusiness.GetList().OrderBy(r => r.Id).ToList();
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
                var user = GetUser();
                int result = this.buildingGroupBusiness.Create(model, user);

                if (result != 0)
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("Details", "BuildingGroup", new { area = "Admin", id = result });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
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
                var user = GetUser();

                bool result = this.buildingGroupBusiness.Backup(model.Id);
                if (!result)
                {
                    ModelState.AddModelError("", "备份失败");
                    return View(model);
                }

                result = this.buildingGroupBusiness.Edit(model, user);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "BuildingGroup", new { area = "Admin", id = model.Id });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
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
            var user = GetUser();

            bool result = this.buildingGroupBusiness.Backup(id);
            if (!result)
            {
                ModelState.AddModelError("", "备份失败");
                return View("Delete", id);
            }

            result = this.buildingGroupBusiness.Delete(id, user);

            if (result)
            {
                return RedirectToAction("List", "BuildingGroup", new { area = "Admin" });
            }
            else
                return View("Delete", id);
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
