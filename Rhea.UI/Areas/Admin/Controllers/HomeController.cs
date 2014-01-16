using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 后台管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();            
        }

        /// <summary>
        /// 导航菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Nav()
        {
            return View();
        }
	}
}