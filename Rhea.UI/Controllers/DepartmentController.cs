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
        /// 部门主页
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            ViewBag.Title = this.departmentBusiness.GetName(id);
            return View(id);
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
        public ActionResult Summary(int id)
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
            var data = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData);

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

            var department = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData);
            DepartmentIndicatorModel data = indicatorBusiness.GetDepartmentIndicator(department);

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
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("DepartmentId", id);
            data.Add("BuildingId", buildingId);

            return View(data);
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
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
