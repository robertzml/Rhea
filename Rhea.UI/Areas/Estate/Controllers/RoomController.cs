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
        /// 房间信息
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            EstateService service = new EstateService();
            var data = service.GetRoom(id);            
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult List(int buildingId, int floor = 0)
        {
            EstateService service = new EstateService();
            List<Room> data;
            if (floor == 0)
            {
                data = service.GetRoomByBuilding(buildingId).OrderBy(r => r.Id).ToList();
            }
            else
            {
                data = service.GetRoomByBuilding(buildingId, floor).OrderBy(r => r.Id).ToList();
            }

            return View(data);
        }       
        #endregion //Action
    }
}
