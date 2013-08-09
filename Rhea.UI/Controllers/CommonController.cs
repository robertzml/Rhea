using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Controllers
{
    public class CommonController : Controller
    {
        #region Action
        /// <summary>
        /// 搜索框
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult SearchInline()
        {
            return View();
        }

        /// <summary>
        /// 信息条
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Heading(string title, string active)
        {
            ViewBag.Title = title;
            ViewBag.Active = active;
            return View();
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>       
        public ActionResult ShowMessage(string msg, string title)
        {
            ViewBag.Title = title;
            ViewBag.Message = msg;
            return View();
        }
        #endregion //Action
    }
}
