using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Common;
using Rhea.Data.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
    public class StatisticController : Controller
    {
        #region Field
        /// <summary>
        /// 统计业务
        /// </summary>
        private IStatisticBusiness statisticBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (statisticBusiness == null)
            {
                statisticBusiness = new MongoStatisticBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 统计主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 部门筛选
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public ActionResult DepartmentFilter(int? type)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var data = departmentBusiness.GetList();

            if (type != null)
            {
                data = data.Where(r => r.Type == Convert.ToInt16(type)).ToList();
            }

            return View(data);
        }

        /// <summary>
        /// 学校总体建筑分类面积
        /// </summary>
        /// <returns></returns>
        public ActionResult TotalUseTypeArea()
        {
            return View();
        }

        /// <summary>
        /// 土地类型
        /// </summary>
        /// <returns></returns>
        public ActionResult LandType()
        {
            return View();
        }

        /// <summary>
        /// 楼群使用面积比较
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildingGroupUsableAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 楼群建筑面积比较
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildingGroupBuildAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 学院使用面积比较
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentUsableAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 学院建筑面积比较
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentBuildAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 学院楼宇用房统计
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentBuildingAreaStatistic()
        {
            return View();
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 楼群面积比较
        /// </summary>
        /// <param name="areaType">面积类型, 1:使用面积, 2:建筑面积</param>
        /// <returns></returns>
        public JsonResult BuildingGroupTotalAreaCompareData(int areaType)
        {
            List<BuildingGroupTotalAreaModel> data = new List<BuildingGroupTotalAreaModel>();

            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();

            var buildingGroups = buildingGroupBusiness.GetList();
            foreach (var buildingGroup in buildingGroups)
            {
                BuildingGroupTotalAreaModel model = new BuildingGroupTotalAreaModel
                {
                    BuildingGroupId = buildingGroup.Id,
                    BuildingGroupName = buildingGroup.Name,
                    BuildArea = Math.Round(Convert.ToDouble(buildingGroup.BuildArea), RheaConstant.AreaDecimalDigits),
                    UsableArea = Math.Round(Convert.ToDouble(buildingGroup.UsableArea), RheaConstant.AreaDecimalDigits)
                };
                data.Add(model);
            }

            if (areaType == 1)
            {
                data = data.OrderByDescending(r => r.UsableArea).ToList();
            }
            else
                data = data.OrderByDescending(r => r.BuildArea).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 学校总体建筑分类面积数据
        /// </summary>
        /// <returns></returns>
        public JsonResult TotalUseTypeAreaData()
        {
            List<UseTypeAreaModel> model = new List<UseTypeAreaModel>();

            for (int i = 1; i <= 4; i++)
            {
                UseTypeAreaModel data = new UseTypeAreaModel();
                data.BuildArea = this.statisticBusiness.GetBuildingAreaByType(i);
                data.TypeName = ((BuildingUseType)i).DisplayName();

                model.Add(data);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 部门总面积比较数据
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <param name="areaType">面积类型, 1:使用面积, 2:建筑面积</param>
        /// <returns></returns>
        public JsonResult DepartmentTotalAreaCompareData(int type, int areaType)
        {
            List<DepartmentTotalAreaModel> data = new List<DepartmentTotalAreaModel>();

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var departments = departmentBusiness.GetList().Where(r => r.Type == type);

            foreach (var department in departments)
            {
                IRoomBusiness roomBusiness = new MongoRoomBusiness();
                var rooms = roomBusiness.GetListByDepartment(department.Id);

                DepartmentTotalAreaModel model = new DepartmentTotalAreaModel
                {
                    DepartmentId = department.Id,
                    DepartmentName = department.Name,
                    BuildArea = Math.Round(Convert.ToDouble(rooms.Sum(r => r.BuildArea)), RheaConstant.AreaDecimalDigits),
                    UsableArea = Math.Round(Convert.ToDouble(rooms.Sum(r => r.UsableArea)), RheaConstant.AreaDecimalDigits)
                };

                data.Add(model);
            }

            if (areaType == 1)
                data = data.OrderByDescending(r => r.UsableArea).ToList();
            else
                data = data.OrderByDescending(r => r.BuildArea).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 部门楼宇用房面积
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public JsonResult DepartmentBuildingAreaStatisticData(int type)
        {
            //get departments
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var departments = departmentBusiness.GetList().Where(r => r.Type == type);

            List<DepartmentTotalAreaModel> data = new List<DepartmentTotalAreaModel>();
            foreach (var department in departments)
            {
                DepartmentTotalAreaModel m = this.statisticBusiness.GetDepartmentTotalArea(department.Id);
                data.Add(m);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}