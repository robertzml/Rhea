using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.UI.Areas.Estate.Models;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Estate.Controllers
{
    [EnhancedAuthorize]
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

            ICampusBusiness campusBusiness = new MongoCampusBusiness();
            data.CampusCount = campusBusiness.Count();
            
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

        /// <summary>
        /// 展示
        /// </summary>
        /// <returns></returns>
        public ActionResult Gallery()
        {
            EstateMiscBusiness business = new EstateMiscBusiness();
            var data = business.GetGallery();
            return View(data);
        }
        #endregion //Action
    }
}
