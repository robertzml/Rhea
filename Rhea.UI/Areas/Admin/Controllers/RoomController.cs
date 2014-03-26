using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    public class RoomController : Controller
    {
        #region Field
        private RoomBusiness roomBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (roomBusiness == null)
            {
                roomBusiness = new RoomBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        //
        // GET: /Admin/Room/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            
            //IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();

            //ViewBag.BuildingName = buildingBusiness.GetName(buildingId);
            //ViewBag.BuildingId = buildingId;

            var data = this.roomBusiness.GetByBuilding(buildingId);
            return View(data);
        }
        #endregion //Action
    }
}