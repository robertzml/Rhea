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

        /// <summary>
        /// 会议纪要
        /// </summary>
        /// <returns></returns>
        public ActionResult MeetingSummary()
        {
            return View();
        }

        /// <summary>
        /// 房产文件
        /// </summary>
        /// <returns></returns>
        public ActionResult EstateFile()
        {
            return View();
        }
        #endregion //Action
    }
}
