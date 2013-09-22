using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model;
using Rhea.Model.Estate;
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
        /// <summary>
        /// 地图点数据
        /// </summary>
        /// <returns></returns>
        public JsonResult MapPointsData()
        {
            IMapBusiness mapBusiness = new MongoMapBusiness();
            var data = mapBusiness.GetPointList(type: 1);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
            var bgs = buildingGroupBusiness.GetList(false);

            foreach (var item in data)
            {
                BuildingGroup bg = bgs.Single(r => r.Id == item.TargetId);
                string c = string.Format("<p>建筑面积:{0} m<sup>2</sup><br />使用面积:{1} m<sup>2</sup><br />房间数量:{2}</p>", 
                    bg.BuildArea, buildingGroupBusiness.GetUsableArea(bg.Id), roomBusiness.CountByBuildingGroup(bg.Id));
                item.Content += c;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
