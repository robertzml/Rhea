using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Controllers
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
