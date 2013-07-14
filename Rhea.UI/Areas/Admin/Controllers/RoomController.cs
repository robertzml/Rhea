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
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 房间业务
        /// </summary>
        private IRoomBusiness roomBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (roomBusiness == null)
            {
                roomBusiness = new MongoRoomBusiness();
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
            var data = this.roomBusiness.GetList().OrderBy(r => r.Id);
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();

            ViewBag.BuildingName = buildingBusiness.GetName(buildingId);
            ViewBag.BuildingId = buildingId;            

            var data = this.roomBusiness.GetListByBuilding(buildingId).OrderBy(r => r.Id);
            return View(data);
        }
        #endregion //Action
    }
}
