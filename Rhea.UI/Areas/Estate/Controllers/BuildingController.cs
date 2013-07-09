using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
    public class BuildingController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private IBuildingService buildingService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingService == null)
            {
                buildingService = new MongoBuildingService();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 楼宇主页
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 楼宇信息
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Building data = this.buildingService.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼层导航
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Floors(int id)
        {
            Building data = this.buildingService.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼层列表
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult FloorList(int id)
        {
            Building data = this.buildingService.Get(id);
            return View(data);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 按楼群得到楼宇信息
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        public JsonResult GetListByBuildingGroup(int buildingGroupId)
        {
            var data = buildingService.GetListByBuildingGroup(buildingGroupId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
