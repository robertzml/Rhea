using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 青教公寓主页控制器
    /// </summary>
    public class HomeController : Controller
    {
        #region Field
        /// <summary>
        /// 青教公寓ID
        /// </summary>
        private int apartmentId = 200037;
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
            var data = business.GetChildBlocks(this.apartmentId);
            return View(data);
        }
        #endregion //Action
    }
}