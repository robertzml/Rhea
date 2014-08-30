using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Controllers
{
    /// <summary>
    /// 错误控制器
    /// </summary>
    public class ErrorController : Controller
    {
        #region Action
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 404错误
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            return View();
        }

        /// <summary>
        /// 500错误
        /// </summary>
        /// <returns></returns>
        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult E500()
        {
            return View();
        }
        #endregion //Action
    }
}