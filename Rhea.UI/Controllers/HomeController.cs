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
using Rhea.UI.Areas.Estate.Models;
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
        /// 地图点详细
        /// </summary>
        /// <param name="targetId">目标ID</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        public ActionResult MapDetails(int targetId, int targetType)
        {
            MapPointDetailModel model = new MapPointDetailModel();
            model.Id = targetId;
            model.Type = targetType;

            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            IRoomBusiness roomBusiness = new MongoRoomBusiness();

            if (targetType == 1)
            {
                var buildingGroup = buildingGroupBusiness.Get(targetId);

                model.Title = buildingGroup.Name;
                if (!string.IsNullOrEmpty(buildingGroup.ImageUrl))
                    model.ImageUrl = RheaConstant.ImagesRoot + buildingGroup.ImageUrl;
                model.BuildArea = buildingGroup.BuildArea.Value;
                model.UsableArea = buildingGroupBusiness.GetUsableArea(model.Id);
                if (buildingGroup.BuildDate != null)
                    model.BuildDate = buildingGroup.BuildDate.Value;

                model.Departments = new List<BuildingGroupDepartmentModel>();
                var roomList = roomBusiness.GetListByBuildingGroup(model.Id);
                model.RoomCount = roomList.Count;

                // get departments in one building
                var dList = roomList.GroupBy(r => r.DepartmentId).Select(s => new { s.Key, Count = s.Count(), Area = s.Sum(r => r.UsableArea) });

                foreach (var d in dList)
                {
                    BuildingGroupDepartmentModel dmodel = new BuildingGroupDepartmentModel
                    {
                        BuildingGroupId = model.Id,
                        DepartmentId = d.Key,
                        RoomCount = d.Count,
                        TotalUsableArea = Convert.ToDouble(d.Area)
                    };
                    dmodel.DepartmentName = departmentBusiness.GetName(dmodel.DepartmentId);

                    model.Departments.Add(dmodel);
                }
            }

            return View(model);
        }

        /// <summary>
        /// 楼群导航
        /// </summary>
        /// <returns></returns>
        [EnhancedAuthorize(Rank = 450)]
        public ActionResult Estate(int? id)
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

            ViewBag.BuildingGroupId = id;

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
                string c = string.Format("<p>建筑面积:{0} m<sup>2</sup><br />使用面积:{1} m<sup>2</sup></p>",
                    bg.BuildArea, buildingGroupBusiness.GetUsableArea(bg.Id));
                item.Content += c;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
