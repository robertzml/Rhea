using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Personnel;
using Rhea.Model.Personnel;

namespace Rhea.UI.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页地图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }       

        /// <summary>
        /// 导航栏
        /// </summary>
        /// <returns></returns>
        public ActionResult Nav()
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            var data = departmentBusiness.GetList();

            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }   

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
    }
}
