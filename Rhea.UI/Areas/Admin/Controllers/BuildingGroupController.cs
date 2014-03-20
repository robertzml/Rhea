using Rhea.Business.Estate;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 楼群控制器
    /// </summary>
    public class BuildingGroupController : Controller
    {
        #region Field
        private BuildingGroupBusiness buildingGroupBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingGroupBusiness == null)
            {
                buildingGroupBusiness = new BuildingGroupBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        //
        // GET: /Admin/BuildingGroup/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 楼群列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.buildingGroupBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 楼群信息
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.buildingGroupBusiness.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}