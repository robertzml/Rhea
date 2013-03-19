using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    public class RoomController : Controller
    {
        #region Action
        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public JsonResult List(int buildingId)
        {
            EstateService service = new EstateService();
            var data = service.GetRoomByBuilding(buildingId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Action
    }
}
