using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 房产管理
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class EstateController : Controller
    {
        #region Action
        // GET: Admin/Estate
        public ActionResult Index()
        {
            return View();
        }
        #endregion //Action
    }
}