using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Filters;
using Rhea.UI.Models;
using Rhea.UI.Services;

namespace Rhea.UI.Controllers
{
    [EnhancedAuthorize]
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
            return View();
        }

        /// <summary>
        /// 地图
        /// </summary>
        /// <returns></returns>
        public ActionResult Map()
        {
            return View();
        }

        /// <summary>
        /// 楼群导航
        /// </summary>
        /// <returns></returns>
        [EnhancedAuthorize(Rank = 450)]
        public ActionResult Estate()
        {
            EstateMenuModel data = new EstateMenuModel();

            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
            data.BuildingGroups = buildingGroupBusiness.GetSimpleList(true);

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetList(true);

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
        [EnhancedAuthorize(Rank = 400)]
        public ActionResult Department()
        {
            DepartmentMenuModel data = new DepartmentMenuModel();
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();

            if (User.IsInRole2("Department"))
            {
                IAccountBusiness accountBusiness = new MongoAccountBusiness();
                var user = accountBusiness.GetByUserName(User.Identity.Name);
                data.Single = departmentBusiness.Get(user.DepartmentId);
            }
            else
                data.Departments = departmentBusiness.GetList();

            return View(data);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
    }
}
