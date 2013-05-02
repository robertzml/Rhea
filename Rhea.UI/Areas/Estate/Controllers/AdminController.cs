using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Data.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 系统管理控制器
    /// </summary>
    public class AdminController : Controller
    {
        #region Action
        /// <summary>
        /// 系统管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 管理组列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagerGroupList()
        {
            IAdminService service = new AdminService();
            var data = service.GetManagerGroupList();
            return View(data);
        }

        [HttpGet]
        public ActionResult CreateManagerGroup()
        {
            return View();
        }
        #endregion //Action
    }
}
