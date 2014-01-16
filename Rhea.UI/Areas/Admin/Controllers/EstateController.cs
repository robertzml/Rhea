using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 房产管理控制器
    /// </summary>
    public class EstateController : Controller
    {
        /// <summary>
        /// 房产管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房产管理菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return View();
        }
	}
}