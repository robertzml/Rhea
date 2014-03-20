using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 楼宇控制器
    /// </summary>
    public class BuildingController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private BuildingBusiness buildingBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingBusiness == null)
            {
                buildingBusiness = new BuildingBusiness();
            }

            base.Initialize(requestContext);
        } 
        #endregion //Function

        #region Action
        //
        // GET: /Admin/Building/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.buildingBusiness.Get();
            return View(data);
        }        
        #endregion //Action
    }
}