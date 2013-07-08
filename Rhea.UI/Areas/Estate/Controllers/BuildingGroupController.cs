using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
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

        #region Action
        /// <summary>
        /// 楼群主页
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 楼群概况
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            BuildingGroup data = this.buildingGroupService.Get(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;
            if (!string.IsNullOrEmpty(data.PartMapUrl))
                data.PartMapUrl = RheaConstant.ImagesRoot + data.PartMapUrl;  

            return View(data);
        }

        /// <summary>
        /// 楼群详细
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            BuildingGroup data = this.buildingGroupService.Get(id);
            return View(data);
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
        #endregion //Action
    }
}
