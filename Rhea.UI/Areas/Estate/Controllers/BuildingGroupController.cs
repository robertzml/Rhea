using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
    public class BuildingGroupController : Controller
    {
        #region Field
        /// <summary>
        /// 楼群业务
        /// </summary>
        private IBuildingGroupService buildingGroupService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingGroupService == null)
            {
                buildingGroupService = new MongoBuildingGroupService();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        //
        // GET: /Estate/BuildingGroup/

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
            List<BuildingGroup> data = this.buildingGroupService.GetList().OrderBy(r => r.Id).ToList();
            return View(data);
        }
    }
}
