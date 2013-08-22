using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 600)]
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
        /// 导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Nav()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        #endregion //Action
    }
}
