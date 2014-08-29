using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 青教管理控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class ApartmentController : Controller
    {
        #region Action
        // GET: Admin/Apartment
        public ActionResult Index()
        {
            return View();
        }
        #endregion //Action
    }
}