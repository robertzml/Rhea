using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 业务办理控制器
    /// </summary>
    public class TransactionController : Controller
    {
        #region Action
        // GET: Apartment/Transaction
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 入住办理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckIn()
        {
            return View();
        }
        #endregion //Action
    }
}