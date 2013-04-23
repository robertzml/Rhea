using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.UI.Areas.Estate.Models;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 用房统计
    /// </summary>
    public class StatisticController : Controller
    {
        #region Action
        /// <summary>
        /// 统计首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
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
        #endregion //Action

        #region JSON
        /// <summary>
        /// 学院各类用房数据
        /// </summary>
        /// <returns></returns>
        public JsonResult CollegeClassifyData()
        {
            EstateService service = new EstateService();
            var data = service.GetStatisticArea<List<ClassifyAreaModel>>(1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //JSON
    }
}
