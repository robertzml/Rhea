using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    public class EstateController : Controller
    {
        #region Action
        /// <summary>
        /// 房产管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion //Action
    }
}
