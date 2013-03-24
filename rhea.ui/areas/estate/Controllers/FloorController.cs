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
    /// 楼层控制器
    /// </summary>
    public class FloorController : Controller
    {
        #region Action
        /// <summary>
        /// 楼层主页
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult Index(int buildingId, int floor)
        {
            ViewBag.BuildingId = buildingId;
            return View(floor);
        }

        /// <summary>
        /// 楼层详细
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult Details(int buildingId, int floor)
        {
            EstateService service = new EstateService();
            var building = service.GetBuilding(buildingId);
            ViewBag.BuildingId = buildingId;

            var data = building.Floors.Find(r => r.Number == floor);
            return View(data);           
        }

        /// <summary>
        /// 楼层添加
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(int buildingId)
        {
            ViewBag.BuildingId = buildingId;
            return View();
        }

        /// <summary>
        /// 楼层添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            if (ModelState.IsValid)
            {
                EstateService service = new EstateService();                
                int result = service.CreateFloor(buildingId, model);

                if (result != 0)
                {
                    return RedirectToAction("Index", "Floor", new { area = "Estate", buildingId = buildingId, floorId = result });
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
        /// <param name="floor">楼层</param>
        /// <returns></returns>        
        public ActionResult Edit(int buildingId, int floor)
        {
            EstateService service = new EstateService();
            var building = service.GetBuilding(buildingId);

            ViewBag.BuildingId = buildingId;
            var data = building.Floors.Find(r => r.Number == floor);
            return View(data);
        }

        /// <summary>
        /// 楼层编辑
        /// </summary>
        /// <param name="model">楼层数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            if (ModelState.IsValid)
            {
                EstateService service = new EstateService();
                bool result = service.UpdateFloor(buildingId, model);

                if (result)
                {
                    return RedirectToAction("Index", "Floor", new { area = "Estate", buildingId = buildingId, floor = model.Number });
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
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int buildingId, int floor)
        {
            EstateService service = new EstateService();
            var building = service.GetBuilding(buildingId);

            ViewBag.BuildingId = buildingId;
            var data = building.Floors.Find(r => r.Number == floor);
            return View(data);
        }

        /// <summary>
        /// 楼层删除 
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            EstateService service = new EstateService();
            bool result = service.DeleteFloor(buildingId, id);

            if (result)
            {
                return RedirectToAction("Index", "Building", new { area = "Estate", id = buildingId });
            }
            else
                return View("Delete", id);
        }
        #endregion //Action
    }
}
