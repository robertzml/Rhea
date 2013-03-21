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
        /// 楼层详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int buildingId, int floorId)
        {
            EstateService service = new EstateService();
            var building = service.GetBuilding(buildingId);
            ViewBag.BuildingId = buildingId;

            var data = building.Floors.Find(r => r.Id == floorId);
            return View(data);           
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">所属楼宇</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(int buildingId)
        {
            ViewBag.BuildingId = buildingId;
            return View();
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Floor model)
        {
            if (ModelState.IsValid)
            {
                EstateService service = new EstateService();
                int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
                int result = service.CreateFloor(buildingId, model);

                if (result != 0)
                {
                    return RedirectToAction("Details", "Floor", new { area = "Estate", buildingId = buildingId, floorId = result });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}
