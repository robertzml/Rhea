using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Controllers
{
    public class SummaryController : Controller
    {
        #region Action
        /// <summary>
        /// 纪要主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房屋变动记录
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomAlteration()
        {
            return View();
        }
        #endregion //Action
    }
}
