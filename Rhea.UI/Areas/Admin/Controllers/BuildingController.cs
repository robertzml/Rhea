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

            switch ((BuildingOrganizeType)data.OrganizeType)
            {
                case BuildingOrganizeType.BuildingGroup:
                    BuildingGroup bg = this.buildingBusiness.GetBuildingGroup(id);
                    return View("BuildingGroupDetails", bg);
                case BuildingOrganizeType.Cluster:
                    break;
                case BuildingOrganizeType.Cottage:
                    Cottage cottage = this.buildingBusiness.GetCottage(id);
                    return View("CottageDetails", cottage);
                case BuildingOrganizeType.Block:
                    break;
                case BuildingOrganizeType.Subregion:
                    break;
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

        /// <summary>
        /// 编辑建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var data = this.buildingBusiness.Get(id);

            switch ((BuildingOrganizeType)data.OrganizeType)
            {
                case BuildingOrganizeType.BuildingGroup:
                    BuildingGroup bg = this.buildingBusiness.GetBuildingGroup(id);
                    return View("BuildingGroupEdit", bg);
                case BuildingOrganizeType.Cluster:
                    break;
                case BuildingOrganizeType.Cottage:
                    Cottage cottage = this.buildingBusiness.GetCottage(id);
                    return View("CottageEdit", cottage);
            }

            return View();
        }

        /// <summary>
        /// 编辑楼群
        /// </summary>
        /// <param name="model">楼群对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult BuildingGroupEdit(BuildingGroup model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.UpdateBuildingGroup(model);
                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑楼群成功";
                    return RedirectToAction("List", "Building");
                }
                else
                {
                    ModelState.AddModelError("", "编辑楼群失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 编辑独栋
        /// </summary>
        /// <param name="model">独栋对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CottageEdit(Cottage model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.UpdateCottage(model);
                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑独栋成功";
                    return RedirectToAction("List", "Building");
                }
                else
                {
                    ModelState.AddModelError("", "编辑独栋失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}