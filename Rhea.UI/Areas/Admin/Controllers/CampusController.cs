using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Data.Estate;
using Rhea.Model.Estate;

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
            CampusRepository campusRepository = new CampusRepository();

            var data = campusRepository.Get();
            return View(data);
        }
	}
}