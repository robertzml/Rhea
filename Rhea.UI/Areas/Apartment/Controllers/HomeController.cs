using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 青教公寓主页控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
    public class HomeController : Controller
    {
        #region Field
        #endregion //Field

        #region Action
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 青教公寓建筑菜单
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult BuildingMenu()
        {
            BuildingBusiness business = new BuildingBusiness();
            var data = business.GetChildBlocks(RheaConstant.ApartmentBuildingId);
            return View(data);
        }
        #endregion //Action
    }
}