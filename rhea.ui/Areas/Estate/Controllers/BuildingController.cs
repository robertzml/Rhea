using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 楼宇控制器
    /// </summary>
    public class BuildingController : Controller
    {
        //
        // GET: /Estate/Building/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 楼宇详细
        /// GET: /Estate/Building/Details/7
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            EstateService service = new EstateService();
            Building data = service.GetBuilding(id);

            return View(data);
        }
    }
}
