using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data;
using Rhea.Data.Estate;
using Rhea.Data.Personnel;
using Rhea.Model;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Filters;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
    [EnhancedAuthorize(Rank = 400)]
    public class DepartmentController : Controller
    {
        #region Field
        /// <summary>
        /// 部门业务
        /// </summary>
        private IDepartmentBusiness departmentBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (departmentBusiness == null)
            {
                departmentBusiness = new MongoDepartmentBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 部门摘要
        /// </summary>
        /// <returns></returns>
        public ActionResult Summary()
        {
            return View();
        }

        /// <summary>
        /// 部门主页
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            DepartmentSectionModel data = new DepartmentSectionModel();

            var department = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
            data.DepartmentId = id;
            data.DepartmentName = department.Name;
            data.StaffCount = department.StaffCount;

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(id);
            data.RoomCount = rooms.Count;
            data.TotalArea = rooms.Sum(r => r.UsableArea.Value);
            data.DepartmentType = department.Type;

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            data.Buildings = buildingBusiness.GetListByDepartment(id);

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

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.departmentBusiness.GetList();
            return View(data);
        }

        /// <summary>
        /// 部门摘要
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Intro(int id)
        {
            Department data = this.departmentBusiness.Get(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;
            return View(data);
        }

        /// <summary>
        /// 部门详细
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(id);

            if (data.Type == (int)DepartmentType.Type1)
            {
                double officeArea = rooms.Where(r => r.Function.FirstCode == 1).Sum(r => r.UsableArea.Value);
                if (data.StaffCount == 0)
                    ViewBag.AvgOfficeArea = 0;
                else
                    ViewBag.AvgOfficeArea = Math.Round(officeArea / data.StaffCount, 2);

                double researchArea = rooms.Where(r => r.Function.FirstCode == 4).Sum(r => r.UsableArea.Value);
                if (data.GraduateCount + data.DoctorCount == 0)
                    ViewBag.AvgResearchArea = 0;
                else
                    ViewBag.AvgResearchArea = Math.Round(researchArea / (data.GraduateCount + data.DoctorCount), 2);

                ViewBag.OfficeArea = officeArea;
                ViewBag.EducationArea = rooms.Where(r => r.Function.FirstCode == 2).Sum(r => r.UsableArea.Value);
                ViewBag.ExperimentArea = rooms.Where(r => r.Function.FirstCode == 3).Sum(r => r.UsableArea.Value);
                ViewBag.ResearchArea = researchArea;

                if (researchArea == 0d)
                    ViewBag.AvgFundsArea = 0;
                else
                    ViewBag.AvgFundsArea = Math.Round((data.LongitudinalFunds + data.TransverseFunds + data.CompanyFunds) / researchArea, 2);
            }
            else
            {
                ViewBag.TotalArea = Math.Round(Convert.ToDouble(rooms.Sum(r => r.UsableArea)), 2);

                int totalPerson = data.PresidentCount + data.VicePresidentCount + data.ChiefCount + data.ViceChiefCount + data.MemberCount;
                if (totalPerson == 0)
                    ViewBag.AvgArea = 0;
                else
                    ViewBag.AvgArea = Math.Round(ViewBag.TotalArea / totalPerson, 2);
            }

            return View(data);
        }

        /// <summary>
        /// 部门指标
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Indicator(int id)
        {
            IIndicatorBusiness indicatorBusiness = new MongoIndicatorBusiness();

            var department = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
            DepartmentIndicatorModel data = indicatorBusiness.GetDepartmentIndicator(department);

            //IRoomBusiness roomBusiness = new MongoRoomBusiness();
            //var rooms = roomBusiness.GetListByDepartment(id);
            //data.ExistingArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));

            //if (data.DeservedArea == 0)
            //    data.Overproof = 0;
            //else
            //    data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);

            ViewBag.Type = department.Type;
            return View(data);
        }

        /// <summary>
        /// 部门分楼宇信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult BuildingSummary(int id)
        {
            IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
            DepartmentTotalAreaModel data = statisticBusiness.GetDepartmentTotalArea(id);

            return View(data);
        }

        /// <summary>
        /// 部门相关楼宇
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Building(int id, int buildingId)
        {
            ViewBag.DepartmentId = id;
            ViewBag.BuildingId = buildingId;

            return View();
        }

        /// <summary>
        /// 部门统计
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Statistic(int id)
        {
            Department data = this.departmentBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 部门详细分类用房
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Classify(int id)
        {
            IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
            DepartmentClassifyAreaModel data = statisticBusiness.GetDepartmentClassifyArea(id, false);

            var department = this.departmentBusiness.Get(id);
            if (department.Type == (int)DepartmentType.Type1)
                data.FirstClassify = data.FirstClassify.Where(r => r.FunctionFirstCode <= 7).ToList();
            else
                data.FirstClassify = data.FirstClassify.Where(r => r.FunctionFirstCode <= 7).ToList();

            return View(data);
        }

        /// <summary>
        /// 部门主要分类用房面积
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <remarks>四类柱状图</remarks>
        /// <returns></returns>
        public ActionResult ClassifyArea(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 部门历史数据
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult History(int id)
        {
            DepartmentHistoryModel data = new DepartmentHistoryModel();

            var department = this.departmentBusiness.Get(id);
            data.DepartmentId = id;
            data.DepartmentName = department.Name;
            data.DepartmentType = department.Type;

            ILogBusiness logBusiness = new MongoLogBusiness();
            data.DepartmentArchiveList = logBusiness.GetList((int)LogType.DepartmentArchive)
                .OrderByDescending(r => r.RelateTime).ToList();

            return View(data);
        }

        /// <summary>
        /// 得到归档部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="logId">日志ID</param>
        /// <returns></returns>
        public ActionResult GetArchiveDepartment(int id, string logId)
        {
            Department data;
            List<Room> rooms;
            IRoomBusiness roomBusiness = new MongoRoomBusiness();

            if (string.IsNullOrEmpty(logId))
            {
                data = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
                rooms = roomBusiness.GetListByDepartment(id);
            }
            else
            {
                data = this.departmentBusiness.GetArchive(id, logId);

                ILogBusiness logBusiness = new MongoLogBusiness();
                Log departmentlog = logBusiness.Get(logId);
                DateTime archiveDate = departmentlog.RelateTime.Value;
                Log roomLog = logBusiness.Get(archiveDate, (int)LogType.RoomArchive);
                rooms = roomBusiness.GetArchiveListByDepartment(id, roomLog._id.ToString());
            }

            if (data.Type == (int)DepartmentType.Type1)
            {
                double officeArea = rooms.Where(r => r.Function.FirstCode == 1).Sum(r => r.UsableArea.Value);
                if (data.StaffCount == 0)
                    ViewBag.AvgOfficeArea = 0;
                else
                    ViewBag.AvgOfficeArea = Math.Round(officeArea / data.StaffCount, 2);

                double researchArea = rooms.Where(r => r.Function.FirstCode == 4).Sum(r => r.UsableArea.Value);
                if (data.GraduateCount + data.DoctorCount == 0)
                    ViewBag.AvgResearchArea = 0;
                else
                    ViewBag.AvgResearchArea = Math.Round(researchArea / (data.GraduateCount + data.DoctorCount), 2);

                ViewBag.OfficeArea = officeArea;
                ViewBag.EducationArea = rooms.Where(r => r.Function.FirstCode == 2).Sum(r => r.UsableArea.Value);
                ViewBag.ExperimentArea = rooms.Where(r => r.Function.FirstCode == 3).Sum(r => r.UsableArea.Value);
                ViewBag.ResearchArea = researchArea;

                if (researchArea == 0d)
                    ViewBag.AvgFundsArea = 0;
                else
                    ViewBag.AvgFundsArea = Math.Round((data.LongitudinalFunds + data.TransverseFunds + data.CompanyFunds) / researchArea, 2);
            }
            else
            {
                ViewBag.TotalArea = Math.Round(rooms.Sum(r => r.UsableArea.Value), 2);

                int totalPerson = data.PresidentCount + data.VicePresidentCount + data.ChiefCount + data.ViceChiefCount + data.MemberCount;
                if (totalPerson == 0)
                    ViewBag.AvgArea = 0;
                else
                    ViewBag.AvgArea = Math.Round(ViewBag.TotalArea / totalPerson, 2);
            }

            return View("Details", data);
        }

        /// <summary>
        /// 得到归档部门指标信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="logId">日志ID</param>
        /// <returns></returns>
        public ActionResult GetArchiveIndicator(int id, string logId)
        {
            Department department;
            DepartmentIndicatorModel data;
            if (string.IsNullOrEmpty(logId))
            {
                department = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);

                IIndicatorBusiness indicatorBusiness = new MongoIndicatorBusiness();
                data = indicatorBusiness.GetDepartmentIndicator(department);
            }
            else
            {
                department = this.departmentBusiness.GetArchive(id, logId);

                ILogBusiness logBusiness = new MongoLogBusiness();
                Log log = logBusiness.Get(logId);
                DateTime archiveDate = log.RelateTime.Value;
                Log roomLog = logBusiness.Get(archiveDate, (int)LogType.RoomArchive);

                IRoomBusiness roomBusiness = new MongoRoomBusiness();
                var rooms = roomBusiness.GetArchiveListByDepartment(id, roomLog._id.ToString());

                IIndicatorBusiness indicatorBusiness = new MongoIndicatorBusiness();
                data = indicatorBusiness.GetDepartmentIndicator(department, rooms);
            }

            return View("IndicatorSummary", data);
        }

        /// <summary>
        /// 部门下拉选择框
        /// </summary>
        /// <returns></returns>
        public ActionResult DropdownList()
        {
            var data = this.departmentBusiness.GetList();
            return View(data);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 部门主要分类用房面积
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public JsonResult GetClassifyArea(int id)
        {
            IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
            DepartmentClassifyAreaModel data = statisticBusiness.GetDepartmentClassifyArea(id);
            data.FirstClassify = data.FirstClassify.Where(r => r.FunctionFirstCode == 1 || r.FunctionFirstCode == 2 ||
                r.FunctionFirstCode == 3 || r.FunctionFirstCode == 4).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 按部门得到楼宇楼层
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public JsonResult GetFloors(int id, int buildingId)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            Building building = buildingBusiness.Get(buildingId);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(id, buildingId);
            var floorNumbers = rooms.Select(r => r.Floor).Distinct();

            var data = building.Floors.Where(r => floorNumbers.Contains(r.Number)).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 得到部门使用总面积
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public JsonResult GetArea(int id)
        {
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var drooms = roomBusiness.GetListByDepartment(id);

            var data = new
            {
                Area = Math.Round(Convert.ToDouble(drooms.Sum(r => r.UsableArea)), 2)
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 得到归档房间
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="logId">日志ID</param>
        /// <returns></returns>
        public JsonResult GetArchiveRoom(int id, string logId)
        {
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            List<Room> data;

            if (string.IsNullOrEmpty(logId))
            {
                data = roomBusiness.GetListByDepartment(id);
            }
            else
            {
                data = roomBusiness.GetArchiveListByDepartment(id, logId);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 得到归档部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="logId">日志ID</param>
        /// <returns></returns>
        public JsonResult GetArchiveDepartmentData(int id, string logId)
        {
            Department data;
            if (string.IsNullOrEmpty(logId))
                data = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
            else
                data = this.departmentBusiness.GetArchive(id, logId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
