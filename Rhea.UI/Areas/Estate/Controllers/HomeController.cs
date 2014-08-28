using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 房产管理主控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Estate,Leader")]
    public class HomeController : Controller
    {
        #region Action
        /// <summary>
        /// 房产管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 建筑菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildingMenu()
        {
            BuildingBusiness business = new BuildingBusiness();
            var data = business.Get().OrderBy(r => r.Sort);
            return View(data);
        }
        #endregion //Action
    }
}