using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;

namespace Rhea.UI.Controllers
{
    public class EstateController : Controller
    {
        //
        // GET: /Estate/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBuildingGroupList()
        {
            EstateService estateService = new EstateService();
            var data = estateService.GetBuildingGroupList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
