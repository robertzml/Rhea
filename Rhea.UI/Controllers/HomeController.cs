using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Action
        public ActionResult Index()
        {
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";

            return View();
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            MenuModel data = new MenuModel();

            IDepartmentService departmentService = new MongoDepartmentService();
            List<Department> departments = departmentService.GetList();            
            data.Departments = departments;

            IBuildingGroupService buildingGroupService = new MongoBuildingGroupService();
            IBuildingService buildingService = new MongoBuildingService();
            List<Building> buildings = buildingService.GetList();

            List<BuildingGroup> buildingGroups = buildingGroupService.GetList();
            foreach (var bg in buildingGroups)
            {
                bg.Buildings = buildings.Where(r => r.BuildingGroupId == bg.Id).OrderBy(r => r.Id).ToList();
            }
            data.BuildingGroups = buildingGroups;

            return PartialView(data);
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Nav()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }      

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
        #endregion //Action        
    }
}
