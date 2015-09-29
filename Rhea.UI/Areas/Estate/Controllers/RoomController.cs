using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    [Privilege(Require = "EstateManage")]
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 房间业务对象
        /// </summary>
        private RoomBusiness roomBusiness;
        #endregion //Field

        #region Constructor
        public RoomController()
        {
            this.roomBusiness = new RoomBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 房间信息页
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            var data = this.roomBusiness.GetByBuilding(buildingId);

            return View(data);
        }

        /// <summary>
        /// 房间摘要
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}