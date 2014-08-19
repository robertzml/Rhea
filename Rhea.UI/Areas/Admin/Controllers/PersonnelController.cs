using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 人事管理控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class PersonnelController : Controller
    {
        #region Action
        /// <summary>
        /// 人事管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion //Action
    }
}