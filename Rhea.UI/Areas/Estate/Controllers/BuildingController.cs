using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 建筑控制器
    /// </summary>
    public class BuildingController : Controller
    {
        #region Action
        /// <summary>
        /// 建筑主页
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            BuildingBusiness business = new BuildingBusiness();
            var data = business.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}