using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
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
    /// 建筑控制器
    /// </summary>
    public class BuildingController : Controller
    {
        #region Field
        /// <summary>
        /// 建筑业务
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
        // GET: Admin/Building
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 建筑列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.buildingBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 建筑详细
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.buildingBusiness.Get(id);

            if (data.OrganizeType == (int)BuildingOrganizeType.BuildingGroup)
            {
                return View("BuildingGroupDetails", data);
            }
            return View(data);
        }

        /// <summary>
        /// 添加建筑
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加建筑
        /// </summary>
        /// <param name="model">建筑对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Building model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.Create(model);

                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "添加建筑成功";
                    return RedirectToAction("List", "Building");
                }
                else
                {
                    ModelState.AddModelError("", "添加建筑失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}