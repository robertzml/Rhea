using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
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
        /// 土地证
        /// </summary>
        /// <returns></returns>
        public ActionResult LandCertificate()
        {
            EstateMiscBusiness business = new EstateMiscBusiness();
            var data = business.GetLandCertificate();
            ViewBag.LandCertificate = data;
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

        #region Json
        /// <summary>
        /// 土地类型数据
        /// </summary>
        /// <returns></returns>
        public JsonResult LandTypeData()
        {
            EstateMiscBusiness business = new EstateMiscBusiness();
            var data = business.GetLandType();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
