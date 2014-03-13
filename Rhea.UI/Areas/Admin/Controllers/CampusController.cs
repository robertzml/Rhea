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

        public ActionResult List()
        {
            //CampusRepository campusRepository = new CampusRepository();

            //var data = campusRepository.Get();
            //return View(data);
            CampusBusiness campusBusiness = new CampusBusiness();
            var data = campusBusiness.Get();
            return View(data);
        }
	}
}