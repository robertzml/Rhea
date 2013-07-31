using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data.Estate;
using Rhea.Data.Personnel;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
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
            data.TotalArea = Convert.ToInt32(rooms.Sum(r => r.UsableArea));

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            data.Buildings = buildingBusiness.GetListByDepartment(id);

            IIndicatorBusiness indicatorBusiness = new MongoIndicatorBusiness();
            DepartmentIndicatorModel indicator = indicatorBusiness.GetDepartmentIndicator(department);

            if (department.Type == (int)DepartmentType.Type1)
            {
                var droom = rooms.Where(r => r.Function.FirstCode == 1 || r.Function.FirstCode == 2 || r.Function.FirstCode == 3 || r.Function.FirstCode == 4);
                data.ExistingArea = Convert.ToDouble(droom.Sum(r => r.UsableArea));
                data.DeservedArea = indicator.DeservedArea;
                if (data.DeservedArea == 0)
                    data.Overproof = 0;
                else
                    data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);
            }
            else
            {
                data.StaffCount = department.PresidentCount + department.VicePresidentCount + department.ChiefCount +
                    department.ViceChiefCount + department.MemberCount;
                data.ExistingArea = data.TotalArea;
                data.DeservedArea = indicator.DeservedArea;
                if (data.DeservedArea == 0)
                    data.Overproof = 0;
                else
                    data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);
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

            if (data.Type == (int)DepartmentType.Type1)
            {
                IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
                DepartmentClassifyAreaModel area = statisticBusiness.GetDepartmentClassifyArea(id);

                double officeArea = area.FirstClassify.Single(r => r.FunctionFirstCode == 1).Area;
                if (data.StaffCount == 0)
                    ViewBag.AvgOfficeArea = 0;
                else
                    ViewBag.AvgOfficeArea = Math.Round(officeArea / data.StaffCount, 2);

                double researchArea = area.FirstClassify.Single(r => r.FunctionFirstCode == 4).Area;
                if (data.GraduateCount + data.DoctorCount == 0)
                    ViewBag.AvgResearchArea = 0;
                else
                    ViewBag.AvgResearchArea = Math.Round(researchArea / (data.GraduateCount + data.DoctorCount), 2);

                ViewBag.OfficeArea = officeArea;
                ViewBag.EducationArea = area.FirstClassify.Single(r => r.FunctionFirstCode == 2).Area;
                ViewBag.ExperimentArea = area.FirstClassify.Single(r => r.FunctionFirstCode == 3).Area;
                ViewBag.ResearchArea = researchArea;

                if (researchArea == 0d)
                    ViewBag.AvgFundsArea = 0;
                else
                    ViewBag.AvgFundsArea = Math.Round((data.LongitudinalFunds + data.TransverseFunds + data.CompanyFunds) / researchArea, 2);
            }
            else
            {
                IRoomBusiness roomBusiness = new MongoRoomBusiness();
                var rooms = roomBusiness.GetListByDepartment(id);

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

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(id);

            if (department.Type == (int)DepartmentType.Type1)
            {
                var droom = rooms.Where(r => r.Function.FirstCode == 1 || r.Function.FirstCode == 2 || r.Function.FirstCode == 3 || r.Function.FirstCode == 4);
                data.ExistingArea = Convert.ToDouble(droom.Sum(r => r.UsableArea));
            }
            else
            {
                data.ExistingArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));
            }

            if (data.DeservedArea == 0)
                data.Overproof = 0;
            else
                data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);

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
        /// 部门分类用房面积
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult ClassifyArea(int id)
        {
            return View(id);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 部门分类用房面积
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
        #endregion //Json
    }
}
