using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 高级管理控制器
    /// </summary>
    public class AdvanceController : Controller
    {
        #region Action
        // GET: Admin/Advance
        public ActionResult Index()
        {
            return View();
        }
        #endregion //Action
    }
}