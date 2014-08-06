using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
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
        /// <summary>
        /// 房间列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.roomBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 根据建筑获取房间
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            BuildingBusiness buildingBusiness = new BuildingBusiness();
            var building = buildingBusiness.Get(buildingId);
            ViewBag.BuildingName = building.Name;
            ViewBag.BuildingId = buildingId;

            var data = this.roomBusiness.GetByBuilding(buildingId);
            return View(data);
        }

        /// <summary>
        /// 房间详细
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}