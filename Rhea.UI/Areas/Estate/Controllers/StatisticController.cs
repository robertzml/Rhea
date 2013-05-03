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
        private IStatisticService statisticService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (statisticService == null)
            {
                statisticService = new RemoteStatisticService();
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
            //EstateService service = new EstateService();

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
            var data = statisticService.GetStatisticArea<List<CollegeClassifyAreaModel>>(1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 学院楼宇用房面积
        /// </summary>
        /// <returns></returns>
        public JsonResult CollegeBuildingData()
        {            
            var data = statisticService.GetStatisticArea<List<CollegeBuildingAreaModel>>(2);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //JSON
    }
}
