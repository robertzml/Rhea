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
        #region Action
        /// <summary>
        /// 主页地图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("Map");
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
        /// 地图导航
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
        #endregion //Action

        #region Json
        public JsonResult MapPointsData()
        {
            List<MapPointModel> data = new List<MapPointModel>();

            MapPointModel p1 = new MapPointModel
            {
                Id = "100023",
                Name = "物联网学院楼",
                Content = "物联网学院楼，共四个分区。",
                PointX = 241,
                PointY = 175,
                Zoom = 3,
                Pin = "pin-green",
                Symbol = "symbol-airport"
            };
            data.Add(p1);

            MapPointModel p2 = new MapPointModel
            {
                Id = "100006",
                Name = "纺服楼",
                Content = "纺织与服装工程学院。",
                PointX = 249,
                PointY = 148,
                Zoom = 3,
                Pin = "pin-blue",
                Symbol = "symbol-airport"
            };
            data.Add(p2);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
