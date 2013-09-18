using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model;
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
                data.Single.ShortName = data.Single.ShortName == "" ? data.Single.Name : data.Single.ShortName;
            }
            else
            {
                data.Departments = departmentBusiness.GetList();
                data.Departments.ForEach(r => r.ShortName = (r.ShortName == "" ? r.Name : r.ShortName));
            }

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
            List<MapPoint> data = new List<MapPoint>();

            MapPoint p1 = new MapPoint
            {
                TargetId = 100023,
                Name = "物联网学院楼",
                Content = "物联网学院楼，共四个分区。",
                PointX = 241,
                PointY = 175,
                Zoom = 3,
                Pin = "pin",
                Symbol = "symbol-airport"
            };
            data.Add(p1);

            MapPoint p2 = new MapPoint
            {
                TargetId = 100006,
                Name = "纺服楼",
                Content = "纺织与服装工程学院。",
                PointX = 249,
                PointY = 148,
                Zoom = 3,
                Pin = "pin",
                Symbol = "symbol-airport"
            };
            data.Add(p2);

            MapPoint p3 = new MapPoint
            {
                TargetId = 100015,
                Name = "生工楼",
                Content = "生物工程学院。",
                PointX = 130,
                PointY = 200,
                Zoom = 3,
                Pin = "pin",
                Symbol = "symbol-airport"
            };
            data.Add(p3);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
