using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data.Personnel;
using Rhea.Model;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Estate.Models;
using Rhea.UI.Filters;
using Rhea.UI.Models;
using Rhea.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                        TotalUsableArea = Math.Round(Convert.ToDouble(d.Area), RheaConstant.AreaDecimalDigits)
                    };
                    dmodel.DepartmentName = departmentBusiness.GetName(dmodel.DepartmentId);

                    model.Departments.Add(dmodel);
                }
            }

            return View(model);
        }

        /// <summary>
        /// 自定义主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {
            if (User.IsInRole2("Department"))
            {
                IAccountBusiness accountBusiness = new MongoAccountBusiness();
                var user = accountBusiness.GetByUserName(User.Identity.Name);
                int departmentId = user.DepartmentId;
                IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();

                DepartmentSectionModel data = new DepartmentSectionModel();

                var department = departmentBusiness.Get(departmentId, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
                data.DepartmentId = departmentId;
                data.DepartmentName = department.Name;
                data.StaffCount = department.StaffCount;

                IRoomBusiness roomBusiness = new MongoRoomBusiness();
                var rooms = roomBusiness.GetListByDepartment(departmentId);
                data.RoomCount = rooms.Count;
                data.TotalArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));
                data.DepartmentType = department.Type;

                IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
                data.Buildings = buildingBusiness.GetListByDepartment(departmentId);

                IIndicatorBusiness indicatorBusiness = new MongoIndicatorBusiness();
                DepartmentIndicatorModel indicator = indicatorBusiness.GetDepartmentIndicator(department);

                if (department.Type == (int)DepartmentType.Type1)   //院系
                {
                    data.ExistingArea = indicator.ExistingArea;
                    data.DeservedArea = indicator.DeservedArea;
                    data.Overproof = indicator.Overproof;

                    double offArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 1).Sum(r => r.UsableArea));
                    data.OfficeAreaRatio = Math.Round(offArea / data.TotalArea * 100, 2);

                    double expArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 3).Sum(r => r.UsableArea));
                    data.ExperimentAreaRatio = Math.Round(expArea / data.TotalArea * 100, 2);

                    double resArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 4).Sum(r => r.UsableArea));
                    data.ResearchAreaRatio = Math.Round(resArea / data.TotalArea * 100, 2);
                }
                else
                {
                    data.StaffCount = department.PresidentCount + department.VicePresidentCount + department.ChiefCount +
                        department.ViceChiefCount + department.MemberCount;
                    data.ExistingArea = indicator.ExistingArea;
                    data.DeservedArea = indicator.DeservedArea;
                    data.Overproof = indicator.Overproof;

                    double offArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 1).Sum(r => r.UsableArea));
                    data.OfficeAreaRatio = Math.Round(offArea / data.TotalArea * 100, 2);
                }

                return View(data);
            }
            else
            {
                return View("Map");
            }            
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

        /// <summary>
        /// FrontView远程调用页面
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Front(int id)
        {
            FrontPluginModel model = new FrontPluginModel();
            model.Id = id;

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var department = departmentBusiness.Get(id,
                Data.Personnel.DepartmentAdditionType.ResearchData | Data.Personnel.DepartmentAdditionType.ScaleData | Data.Personnel.DepartmentAdditionType.SpecialAreaData);

            if (!string.IsNullOrEmpty(department.ImageUrl))
                model.ImageUrl = RheaConstant.ImagesRoot + department.ImageUrl;
            model.Description = department.Description;

            IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
            model.Area = statisticBusiness.GetDepartmentTotalArea(id);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(id);
            model.OfficeArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 1).Sum(r => r.UsableArea)), 2);
            model.EducationArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 2).Sum(r => r.UsableArea)), 2);
            model.ExperimentArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 3).Sum(r => r.UsableArea)), 2);
            model.ResearchArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 4).Sum(r => r.UsableArea)), 2);

            return View(model);
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

        [AllowAnonymous]
        public JsonResult FrontData(int id)
        {
            FrontPluginModel model = new FrontPluginModel();
            model.Id = id;

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var department = departmentBusiness.Get(id,
                Data.Personnel.DepartmentAdditionType.ResearchData | Data.Personnel.DepartmentAdditionType.ScaleData | Data.Personnel.DepartmentAdditionType.SpecialAreaData);

            if (!string.IsNullOrEmpty(department.ImageUrl))
                model.ImageUrl = RheaConstant.ImagesRoot + department.ImageUrl;
            model.Description = department.Description;

            IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
            model.Area = statisticBusiness.GetDepartmentTotalArea(id);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(id);
            model.OfficeArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 1).Sum(r => r.UsableArea)), 2);
            model.EducationArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 2).Sum(r => r.UsableArea)), 2);
            model.ExperimentArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 3).Sum(r => r.UsableArea)), 2);
            model.ResearchArea = Math.Round(Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 4).Sum(r => r.UsableArea)), 2);


            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
