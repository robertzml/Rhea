using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页地图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }       

        /// <summary>
        /// 导航栏
        /// </summary>
        /// <returns></returns>
        public ActionResult Nav()
        {
            /*NavModel data = new NavModel();
            
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            data.Departments = departmentBusiness.GetList();

            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
            data.BuildingGroups = buildingGroupBusiness.GetSimpleList();

            return View(data);*/
            return View();
        }

        /// <summary>
        /// 楼群导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Estate()
        {
            EstateMenuModel data = new EstateMenuModel();

            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
            data.BuildingGroups = buildingGroupBusiness.GetSimpleList();

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetList().OrderBy(r => r.Id);

            foreach (var bg in data.BuildingGroups)
            {
                bg.Buildings = buildings.Where(r => r.BuildingGroupId == bg.Id).ToList();
            }

            return View(data);
        }

        /// <summary>
        /// 部门导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Department()
        {
            DepartmentMenuModel data = new DepartmentMenuModel();

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            data.Departments = departmentBusiness.GetList();

            return View(data);
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

        public ActionResult Test()
        {
            return View();
        }
    }
}
