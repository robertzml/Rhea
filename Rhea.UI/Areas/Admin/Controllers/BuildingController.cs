using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Admin.Controllers
{
    public class BuildingController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private IBuildingBusiness buildingBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingBusiness == null)
            {
                buildingBusiness = new MongoBuildingBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <param name="buildingGroupId"></param>
        /// <returns></returns>
        public ActionResult List(int? buildingGroupId)
        {
            if (buildingGroupId == null)
            {
                var data = this.buildingBusiness.GetList();
                return View(data);
            }
            else
            {
                var data = this.buildingBusiness.GetListByBuildingGroup((int)buildingGroupId);
                return View(data);
            }
        }

        /// <summary>
        /// 楼宇信息
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 楼宇添加
        /// </summary>
        /// <param name="model">楼宇模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Building model)
        {
            if (ModelState.IsValid)
            {
                int result = this.buildingBusiness.Create(model);

                if (result != 0)
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("Details", "Building", new { area = "Admin", id = result });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 楼宇编辑
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Building model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.buildingBusiness.Edit(model);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "Building", new { area = "Admin", id = model.Id });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 楼宇删除
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇删除
        /// POST: /Estate/Builidng/Delete/7
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = this.buildingBusiness.Delete(id);

            if (result)
            {
                TempData["Message"] = "删除成功";
                return RedirectToAction("List", "Building", new { area = "Admin" });
            }
            else
                return View("Delete", id);
        }

        /// <summary>
        /// 楼层添加
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateFloor(int buildingId)
        {
            ViewBag.BuildingId = buildingId;
            return View();
        }

        /// <summary>
        /// 楼层添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateFloor(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            if (ModelState.IsValid)
            {
                int result = this.buildingBusiness.CreateFloor(buildingId, model);

                if (result != 0)
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("Details", "Building", new { area = "Admin", id = buildingId });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            ViewBag.BuildingId = buildingId;
            return View(model);
        }

        /// <summary>
        /// 楼层编辑
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>        
        public ActionResult EditFloor(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(floorId);

            return View(data);
        }

        /// <summary>
        /// 楼层编辑
        /// </summary>
        /// <param name="model">楼层数据</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditFloor(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            if (ModelState.IsValid)
            {
                bool result = this.buildingBusiness.EditFloor(buildingId, model);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "Building", new { area = "Admin", id = buildingId });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            ViewBag.BuildingId = buildingId;
            return View(model);
        }

        /// <summary>
        /// 楼层删除
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteFloor(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(floorId);
           
            return View(data);
        }

        /// <summary>
        /// 楼层删除 
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteFloor")]
        public ActionResult DeleteFloorConfirm(int id)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            bool result = this.buildingBusiness.DeleteFloor(buildingId, id);

            if (result)
            {
                TempData["Message"] = "删除成功";
                return RedirectToAction("Details", "Building", new { area = "Admin", id = buildingId });
            }
            else
                return View("Delete", id);
        }
        #endregion //Action
    }
}
