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
    /// 房产管理主控制器
    /// </summary>
    public class HomeController : Controller
    {
        //
        // GET: /Estate/Home/

        public ActionResult Index()
        {
            EstateService estateService = new EstateService();
            List<BuildingGroup> buildingGroups = estateService.GetBuildingGroupList().ToList();
            List<Building> buildings = estateService.GetBuildingList().ToList();

            foreach (var bg in buildingGroups)
            {
                bg.Buildings = buildings.Where(r => r.BuildingGroupId == bg.Id).ToList();
            }

            ViewBag.BuildingGroups = buildingGroups;
            ViewBag.Buildings = buildings;

            return View();
        }

        public JsonResult GetBuildingGroupList()
        {
            EstateService estateService = new EstateService();
            var data = estateService.GetBuildingGroupList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
