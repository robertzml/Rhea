using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 楼群控制器
    /// </summary>
    public class BuildingGroupController : Controller
    {
        #region Action
        /// <summary>
        /// 楼群主页
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 楼群树形列表
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Tree()
        {
            EstateService estateService = new EstateService();
            List<BuildingGroup> buildingGroups = estateService.GetBuildingGroupList().ToList();
            List<Building> buildings = estateService.GetBuildingList().ToList();

            foreach (var bg in buildingGroups)
            {
                bg.Buildings = buildings.Where(r => r.BuildingGroupId == bg.Id).ToList();
            }

            return View(buildingGroups);
        }

        /// <summary>
        /// 楼群详细
        /// GET: /Estate/BuildingGroup/Details/7
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            EstateService service = new EstateService();
            BuildingGroup data = service.GetBuildingGroup(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;           

            data.Buildings = service.GetBuildingByGroup(id).OrderBy(r => r.Id).ToList();
            
            return View(data);
        }

        /// <summary>
        /// 楼群列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            EstateService service = new EstateService();
            List<BuildingGroup> data = service.GetBuildingGroupList().OrderBy(r => r.Id).ToList();

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
        /// POST: /Estate/BuildingGroup/Create/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(BuildingGroup model)
        {
            if (ModelState.IsValid)
            {
                EstateService service = new EstateService();
                int result = service.CreateBuildingGroup(model);

                if (result != 0)
                {                    
                    return RedirectToAction("Index", "BuildingGroup", new { area = "Estate", id = result });
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
        /// GET: /Estate/BuildingGroup/Edit/7
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            EstateService service = new EstateService();
            BuildingGroup data = service.GetBuildingGroup(id);
            return View(data);
        }

        /// <summary>
        /// 编辑楼群
        /// POST: /Estate/BuildingGroup/Edit/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(BuildingGroup model)
        {
            if (ModelState.IsValid)
            {
                EstateService service = new EstateService();
                bool result = service.UpdateBuildingGroup(model);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Index", "BuildingGroup", new { area = "Estate", id = model.Id });
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
            EstateService service = new EstateService();
            var data = service.GetBuildingGroup(id);
            return View(data);
        }

        /// <summary>
        /// 删除楼群
        /// POST: /Estate/BuilidngGroup/Delete/7
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EstateService service = new EstateService();
            bool result = service.DeleteBuildingGroup(id);

            if (result)
            {
                return RedirectToAction("List", "BuildingGroup", new { area = "Estate" });
            }
            else
                return View("Delete", id);
        }
        #endregion //Action

        #region JSON
        /// <summary>
        /// 得到楼群信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetList()
        {
            EstateService service = new EstateService();
            List<BuildingGroup> bg = service.GetBuildingGroupList().ToList();

            return Json(bg, JsonRequestBehavior.AllowGet);
        }
        #endregion //JSON
    }
}
