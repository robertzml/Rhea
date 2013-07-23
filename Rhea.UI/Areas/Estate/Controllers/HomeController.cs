using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.UI.Areas.Estate.Models;

namespace Rhea.UI.Areas.Estate.Controllers
{
    public class HomeController : Controller
    {
        #region Action
        /// <summary>
        /// 房产概况
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 摘要
        /// </summary>
        /// <returns></returns>
        public ActionResult Summary()
        {
            EstateSummaryModel data = new EstateSummaryModel();

            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
            data.BuildingGroupCount = buildingGroupBusiness.Count();

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            data.BuildingCount = buildingBusiness.Count();
            data.FloorCount = buildingBusiness.FloorCount();

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            data.RoomCount = roomBusiness.Count();

            return View(data);
        }

        /// <summary>
        /// 楼群列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildingGroupList()
        {
            return View();
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentList()
        {
            return View();
        }
        #endregion //Action
    }
}
