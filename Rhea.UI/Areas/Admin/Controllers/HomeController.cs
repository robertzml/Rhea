using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Action
        /// <summary>
        /// 管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return View();
        }
        #endregion //Action
    }
}
