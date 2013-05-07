using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Data.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 用房统计
    /// </summary>
    public class StatisticController : Controller
    {
        #region Field
        /// <summary>
        /// 统计业务
        /// </summary>
        private IStatisticService statisticService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (statisticService == null)
            {
                statisticService = new MongoStatisticService();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 统计首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.BuildingGroupCount = 0;// service.GetEntitySize(11);
            ViewBag.RoomCount = 0;// service.GetEntitySize(12);

            return View();
        }

        /// <summary>
        /// 学院分类用房统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CollegeRoomStatistic()
        {
            return View();
        }

        /// <summary>
        /// 学院分类用房比较
        /// </summary>
        /// <returns></returns>
        public ActionResult CollegeRoomCompare()
        {
            return View();
        }

        /// <summary>
        /// 学院楼宇用房统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CollegeBuildingRoom()
        {
            return View();
        }
        #endregion //Action

        #region JSON
        /// <summary>
        /// 学院各类用房数据
        /// </summary>
        /// <returns></returns>
        public JsonResult CollegeClassifyData()
        {
            //get departments
            IDepartmentService departmentService = new MongoDepartmentService();
            var departments = departmentService.GetList().Where(r => r.Type == 1);

            //get codes
            IRoomService roomService = new MongoRoomService();
            var functionCodes = roomService.GetFunctionCodeList();

            List<CollegeClassifyAreaModel> data = new List<CollegeClassifyAreaModel>();
            //get area by department
            foreach (var department in departments)
            {
                CollegeClassifyAreaModel c = new CollegeClassifyAreaModel
                {
                    Id = department.Id,
                    CollegeName = department.Name
                };

                c.OfficeDetailArea = this.statisticService.GetClassifyArea(department.Id, 1, functionCodes);
                c.OfficeArea = Math.Round(c.OfficeDetailArea.Sum(r => r.Area), 3);

                c.EducationDetailArea = this.statisticService.GetClassifyArea(department.Id, 2, functionCodes);
                c.EducationArea = Math.Round(c.EducationDetailArea.Sum(r => r.Area), 3);

                c.ExperimentDetailArea = this.statisticService.GetClassifyArea(department.Id, 3, functionCodes);
                c.ExperimentArea = Math.Round(c.ExperimentDetailArea.Sum(r => r.Area), 3);

                c.ResearchDetailArea = this.statisticService.GetClassifyArea(department.Id, 4, functionCodes);
                c.ResearchArea = Math.Round(c.ResearchDetailArea.Sum(r => r.Area), 3);

                data.Add(c);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 学院楼宇用房面积
        /// </summary>
        /// <returns></returns>
        public JsonResult CollegeBuildingData()
        {
            //get departments
            IDepartmentService departmentService = new MongoDepartmentService();
            var departments = departmentService.GetList().Where(r => r.Type == 1);

            //get buildings        
            IBuildingService buildingService = new MongoBuildingService();
            var buildings = buildingService.GetList();

            List<CollegeBuildingAreaModel> data = new List<CollegeBuildingAreaModel>();

            //get area by department
            foreach (var department in departments)
            {
                List<BuildingAreaModel> model = this.statisticService.GetBuildingArea(department.Id, buildings);

                CollegeBuildingAreaModel c = new CollegeBuildingAreaModel
                {
                    Id = department.Id,
                    CollegeName = department.Name,
                    BuildingArea = model
                };

                data.Add(c);
            }
           
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //JSON
    }
}
