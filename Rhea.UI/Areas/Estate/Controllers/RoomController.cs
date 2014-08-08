using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    public class RoomController : Controller
    {
        #region Action
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            RoomBusiness business = new RoomBusiness();
            var data = business.GetByBuilding(buildingId);

            return View(data);
        }
        #endregion //Action
    }
}