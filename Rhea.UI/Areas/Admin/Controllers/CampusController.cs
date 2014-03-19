using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Model.Estate;
using Rhea.Business.Estate;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 校区控制器
    /// </summary>
    public class CampusController : Controller
    {

        //
        // GET: /Admin/Campus/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 校区列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {           
            CampusBusiness campusBusiness = new CampusBusiness();
            var data = campusBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 校区信息
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            CampusBusiness campusBusiness = new CampusBusiness();
            var data = campusBusiness.Get(id);
            return View(data);
        }
	}
}