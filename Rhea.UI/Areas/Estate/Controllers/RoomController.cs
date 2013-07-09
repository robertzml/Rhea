using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 房间业务
        /// </summary>
        private IRoomService roomService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (roomService == null)
            {
                roomService = new MongoRoomService();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        public ActionResult Summary(int id)
        {
            Room data = this.roomService.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}
