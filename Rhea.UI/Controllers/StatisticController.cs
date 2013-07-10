using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Common;
using Rhea.UI.Areas.Estate.Models;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
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
        public ActionResult Index()
        {
            return View();
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
        #endregion //Action

        #region Json
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
                data.BuildArea = this.statisticService.GetBuildingAreaByType(i);
                data.TypeName = ((BuildingUseType)i).DisplayName();

                model.Add(data);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
