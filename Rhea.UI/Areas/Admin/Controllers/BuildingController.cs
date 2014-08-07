using Rhea.Business;
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

            ViewBag.Children = this.buildingBusiness.GetChildBuildings(id);

            switch ((BuildingOrganizeType)data.OrganizeType)
            {
                case BuildingOrganizeType.BuildingGroup:
                    BuildingGroup bg = this.buildingBusiness.GetBuildingGroup(id);
                    return View("BuildingGroupDetails", bg);
                case BuildingOrganizeType.Cluster:
                    Cluster cluster = this.buildingBusiness.GetCluster(id);
                    return View("ClusterDetails", cluster);
                case BuildingOrganizeType.Cottage:
                    Cottage cottage = this.buildingBusiness.GetCottage(id);
                    return View("CottageDetails", cottage);
                case BuildingOrganizeType.Subregion:
                    Subregion subregion = this.buildingBusiness.GetSubregion(id);
                    return View("SubregionDetails", subregion);
                case BuildingOrganizeType.Block:
                    Block block = this.buildingBusiness.GetBlock(id);
                    return View("BlockDetails", block);
                case BuildingOrganizeType.Playground:
                    Playground playground = this.buildingBusiness.GetPlayground(id);
                    return View("PlaygroundDetails", playground);
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
                    Cluster cluster = this.buildingBusiness.GetCluster(id);
                    return View("ClusterEdit", cluster);
                case BuildingOrganizeType.Cottage:
                    Cottage cottage = this.buildingBusiness.GetCottage(id);
                    return View("CottageEdit", cottage);
                case BuildingOrganizeType.Subregion:
                    Subregion subregion = this.buildingBusiness.GetSubregion(id);
                    return View("SubregionEdit", subregion);
                case BuildingOrganizeType.Block:
                    Block block = this.buildingBusiness.GetBlock(id);
                    return View("BlockEdit", block);
                case BuildingOrganizeType.Playground:
                    Playground playground = this.buildingBusiness.GetPlayground(id);
                    return View("PlaygroundEdit", playground);
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
                    return RedirectToAction("Details", "Building", new { id = model.BuildingId });
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
        /// 编辑组团
        /// </summary>
        /// <param name="model">组团对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ClusterEdit(Cluster model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.UpdateCluster(model);
                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑组团成功";
                    return RedirectToAction("Details", "Building", new { id = model.BuildingId });
                }
                else
                {
                    ModelState.AddModelError("", "编辑组团失败");
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
                    return RedirectToAction("Details", "Building", new { id = model.BuildingId });
                }
                else
                {
                    ModelState.AddModelError("", "编辑独栋失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 编辑分区
        /// </summary>
        /// <param name="model">分区对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubregionEdit(Subregion model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.UpdateSubregion(model);
                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑分区成功";
                    return RedirectToAction("Details", "Building", new { id = model.BuildingId });
                }
                else
                {
                    ModelState.AddModelError("", "编辑分区失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 编辑楼宇
        /// </summary>
        /// <param name="model">楼宇对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult BlockEdit(Block model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.UpdateBlock(model);
                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑分区成功";
                    return RedirectToAction("Details", "Building", new { id = model.BuildingId });
                }
                else
                {
                    ModelState.AddModelError("", "编辑分区失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 编辑操场
        /// </summary>
        /// <param name="model">操场对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PlaygroundEdit(Playground model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.UpdatePlayground(model);
                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑操场成功";
                    return RedirectToAction("Details", "Building", new { id = model.BuildingId });
                }
                else
                {
                    ModelState.AddModelError("", "编辑操场失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 编辑楼层
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditFloor(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(buildingId, floorId);
            return View(data);
        }

        /// <summary>
        /// 编辑楼层
        /// </summary>
        /// <param name="model">楼层对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditFloor(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);

            if (ModelState.IsValid)
            {
                ErrorCode result = this.buildingBusiness.UpdateFloor(buildingId, model);
                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑楼层成功";
                    return RedirectToAction("Details", "Building", new { id = buildingId });
                }
                else
                {
                    ModelState.AddModelError("", "编辑楼层失败");
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            ViewBag.BuildingId = buildingId;
            return View(model);
        }
        #endregion //Action
    }
}