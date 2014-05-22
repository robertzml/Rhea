using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 房产管理
    /// </summary>
    public class EstateController : Controller
    {
        #region Action
        // GET: Admin/Estate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }
        #endregion //Action
    }
}