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
using Rhea.Data.Personnel;
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
            StatisticHomeModel data = new StatisticHomeModel();
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            data.TotalArea = Math.Round(roomBusiness.TotalArea());
            data.UniversityControlArea = roomBusiness.FunctionRoomArea(6, 2);
            data.UniversityControlPercentage = Math.Round(data.UniversityControlArea / data.TotalArea, 3) * 100;

            return View(data);
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
                if (type == 1)
                    data = data.Where(r => r.Type == 1).ToList();
                else
                    data = data.Where(r => r.Type != 1).ToList();
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

        /// <summary>
        /// 学院分类用房统计
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentClassifyAreaStatistic()
        {
            return View();
        }

        /// <summary>
        /// 学院分类用房比较
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentClassifyAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 学院人均办公用房比较
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentAverageOfficeAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 学院人均科研用房面积
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentAverageResearchAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 年度科研经费用房面积
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentResearchFundsAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 学院用房指标比较
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentIndicatorCompare()
        {
            return View();
        }

        /// <summary>
        /// 行政机关办公用房面积比较
        /// </summary>
        /// <returns></returns>
        public ActionResult InstitutionOfficeAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 行政机关人均办公用房面积比较
        /// </summary>
        /// <returns></returns>
        public ActionResult InstitutionAverageOfficeAreaCompare()
        {
            return View();
        }

        /// <summary>
        /// 行政机关指标比较
        /// </summary>
        /// <returns></returns>
        public ActionResult InstitutionIndicatorCompare()
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
                    UsableArea = Math.Round(buildingGroupBusiness.GetUsableArea(buildingGroup.Id), RheaConstant.AreaDecimalDigits)
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

            for (int i = 1; i <= 5; i++)
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
        /// 所有部门分楼宇用房面积
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public JsonResult DepartmentBuildingAreaStatisticData(int type)
        {
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

        /// <summary>
        /// 所有部门分类用房统计
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public JsonResult DepartmentClassifyAreaData(int type)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var departments = departmentBusiness.GetList().Where(r => r.Type == type);

            List<DepartmentClassifyAreaModel> data = new List<DepartmentClassifyAreaModel>();
            foreach (var department in departments)
            {
                DepartmentClassifyAreaModel model = statisticBusiness.GetDepartmentClassifyArea(department.Id, false);

                data.Add(model);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 所有部门人均办公面积
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public JsonResult DepartmentAverageOfficeAreaCompareData(int type)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var departments = departmentBusiness.GetList().Where(r => r.Type == type);

            List<DepartmentAverageAreaModel> data = new List<DepartmentAverageAreaModel>();

            foreach (var department in departments)
            {
                DepartmentClassifyAreaModel model = statisticBusiness.GetDepartmentClassifyArea(department.Id, false);

                DepartmentAverageAreaModel avg = new DepartmentAverageAreaModel();
                avg.DepartmentId = department.Id;
                avg.DepartmentName = department.Name;
                avg.TotalArea = model.FirstClassify.Where(r => r.FunctionFirstCode == 1).Sum(r => r.Area);

                Department addition = departmentBusiness.Get(department.Id, DepartmentAdditionType.ScaleData);
                avg.PeopleCount = addition.StaffCount;

                if (avg.PeopleCount == 0)
                    avg.AverageArea = 0;
                else
                    avg.AverageArea = Math.Round(avg.TotalArea / avg.PeopleCount, 2);

                data.Add(avg);
            }

            data = data.OrderByDescending(r => r.AverageArea).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 所有部门人均科研面积
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public JsonResult DepartmentAverageResearchAreaCompareData(int type)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var departments = departmentBusiness.GetList().Where(r => r.Type == type);

            List<DepartmentAverageAreaModel> data = new List<DepartmentAverageAreaModel>();

            foreach (var department in departments)
            {
                DepartmentClassifyAreaModel model = statisticBusiness.GetDepartmentClassifyArea(department.Id, false);

                DepartmentAverageAreaModel avg = new DepartmentAverageAreaModel();
                avg.DepartmentId = department.Id;
                avg.DepartmentName = department.Name;
                avg.TotalArea = model.FirstClassify.Where(r => r.FunctionFirstCode == 4).Sum(r => r.Area);

                Department addition = departmentBusiness.Get(department.Id, DepartmentAdditionType.ScaleData);
                avg.PeopleCount = addition.GraduateCount + addition.DoctorCount;

                if (avg.PeopleCount == 0)
                    avg.AverageArea = 0;
                else
                    avg.AverageArea = Math.Round(avg.TotalArea / avg.PeopleCount, 2);

                data.Add(avg);
            }

            data = data.OrderByDescending(r => r.AverageArea).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 年度科研经费用房面积
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public JsonResult DepartmentResearchFundsAreaCompareData(int type)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var departments = departmentBusiness.GetList().Where(r => r.Type == type);

            List<DepartmentAverageAreaModel> data = new List<DepartmentAverageAreaModel>();

            foreach (var department in departments)
            {
                DepartmentClassifyAreaModel model = statisticBusiness.GetDepartmentClassifyArea(department.Id, false);

                DepartmentAverageAreaModel avg = new DepartmentAverageAreaModel();
                avg.DepartmentId = department.Id;
                avg.DepartmentName = department.Name;
                avg.TotalArea = model.FirstClassify.Where(r => r.FunctionFirstCode == 4).Sum(r => r.Area);

                Department addition = departmentBusiness.Get(department.Id, DepartmentAdditionType.ResearchData);
                avg.TotalFunds = addition.LongitudinalFunds + addition.TransverseFunds + addition.CompanyFunds;

                if (avg.TotalArea == 0d)
                    avg.AverageArea = 0;
                else
                    avg.AverageArea = Math.Round(avg.TotalFunds / avg.TotalArea, 2);

                data.Add(avg);
            }

            data = data.OrderByDescending(r => r.AverageArea).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 部门用房指标
        /// </summary>
        /// <param name="type">部门类型，1:学院, 2:其它</param>
        /// <returns></returns>
        public JsonResult DepartmentIndicatorCompareData(int type)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            IIndicatorBusiness indicatorBusiness = new MongoIndicatorBusiness();
            List<DepartmentIndicatorModel> data = new List<DepartmentIndicatorModel>();

            if (type == 1)
            {
                var departments = departmentBusiness.GetList().Where(r => r.Type == 1);
                foreach (var department in departments)
                {
                    Department d = departmentBusiness.Get(department.Id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
                    var indicator = indicatorBusiness.GetDepartmentIndicator(d);
                    indicator.ExistingArea = roomBusiness.DepartmentRoomArea(department.Id);
                    if (indicator.DeservedArea == 0)
                        indicator.Overproof = 0;
                    else
                        indicator.Overproof = Math.Round(indicator.ExistingArea / indicator.DeservedArea * 100, 2);

                    data.Add(indicator);
                }
            }
            else
            {
                var departments = departmentBusiness.GetList().Where(r => r.Type != 1);
                foreach (var department in departments)
                {
                    Department d = departmentBusiness.Get(department.Id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
                    var indicator = indicatorBusiness.GetDepartmentIndicator(d);
                    indicator.ExistingArea = roomBusiness.DepartmentRoomArea(department.Id);
                    if (indicator.DeservedArea == 0)
                        indicator.Overproof = 0;
                    else
                        indicator.Overproof = Math.Round(indicator.ExistingArea / indicator.DeservedArea * 100, 2);

                    data.Add(indicator);
                }
            }

            data = data.OrderByDescending(r => r.Overproof).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 机关办公面积
        /// </summary>
        /// <param name="sort">排序方式,1:总面积, 2:平均面积</param>
        /// <returns></returns>
        public JsonResult InstitutionOfficeAreaData(int sort)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var departments = departmentBusiness.GetList().Where(r => r.Type != 1);

            //get codes
            EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
            var functionCodes = dictionaryBusiness.GetRoomFunctionCode();

            List<DepartmentAverageAreaModel> data = new List<DepartmentAverageAreaModel>();

            foreach (var department in departments)
            {
                RoomFirstClassifyAreaModel first = statisticBusiness.GetFirstClassifyArea("departmentId", department.Id,
                    1, "办公面积", functionCodes, false);

                DepartmentAverageAreaModel model = new DepartmentAverageAreaModel
                {
                    DepartmentId = department.Id,
                    DepartmentName = department.Name,
                    TotalArea = first.Area
                };

                Department addition = departmentBusiness.Get(department.Id, DepartmentAdditionType.ScaleData);
                model.PeopleCount = addition.PresidentCount + addition.VicePresidentCount + addition.ChiefCount +
                    addition.ViceChiefCount + addition.MemberCount;

                if (model.PeopleCount == 0)
                    model.AverageArea = 0;
                else
                    model.AverageArea = Math.Round(model.TotalArea / model.PeopleCount, 2);

                data.Add(model);
            }

            if (sort == 1)
                data = data.OrderByDescending(r => r.TotalArea).ToList();
            else
                data = data.OrderByDescending(r => r.AverageArea).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
