﻿using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using Rhea.UI.Filters;
using Rhea.UI.Services;
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
    [EnhancedAuthorize(Rank = 900)]
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

            ViewBag.Children = this.buildingBusiness.GetChildBuildings(id).OrderBy(r => r.Sort);

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
                //create
                model.Status = 0;
                ErrorCode result = this.buildingBusiness.Create(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "添加建筑失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台添加建筑",
                    Time = DateTime.Now,
                    Type = (int)LogType.BuildingCreate,
                    Content = string.Format("添加建筑， ID:{0}，名称:{1}。", model.BuildingId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "添加建筑成功";
                return RedirectToAction("List", "Building");
            }

            return View(model);
        }

        /// <summary>
        /// 编辑建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        [HttpGet]
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
                //backup
                ErrorCode result = this.buildingBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份楼群失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.buildingBusiness.UpdateBuildingGroup(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑楼群失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑楼群",
                    Time = DateTime.Now,
                    Type = (int)LogType.BuildingEdit,
                    Content = string.Format("编辑楼群， ID:{0}，名称:{1}。", model.BuildingId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑楼群成功";
                return RedirectToAction("Details", "Building", new { id = model.BuildingId });

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
                //backup
                ErrorCode result = this.buildingBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份组团失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.buildingBusiness.UpdateCluster(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑组团失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑组团",
                    Time = DateTime.Now,
                    Type = (int)LogType.BuildingEdit,
                    Content = string.Format("编辑组团， ID:{0}，名称:{1}。", model.BuildingId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑组团成功";
                return RedirectToAction("Details", "Building", new { id = model.BuildingId });
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
                //backup
                ErrorCode result = this.buildingBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份独栋失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.buildingBusiness.UpdateCottage(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑独栋失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑独栋",
                    Time = DateTime.Now,
                    Type = (int)LogType.BuildingEdit,
                    Content = string.Format("编辑独栋， ID:{0}，名称:{1}。", model.BuildingId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑独栋成功";
                return RedirectToAction("Details", "Building", new { id = model.BuildingId });
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
                //backup
                ErrorCode result = this.buildingBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份分区失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.buildingBusiness.UpdateSubregion(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑分区失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑分区",
                    Time = DateTime.Now,
                    Type = (int)LogType.BuildingEdit,
                    Content = string.Format("编辑分区， ID:{0}，名称:{1}。", model.BuildingId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑分区成功";
                return RedirectToAction("Details", "Building", new { id = model.BuildingId });
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
                //backup
                ErrorCode result = this.buildingBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份楼宇失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.buildingBusiness.UpdateBlock(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑楼宇失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑楼宇",
                    Time = DateTime.Now,
                    Type = (int)LogType.BuildingEdit,
                    Content = string.Format("编辑楼宇， ID:{0}，名称:{1}。", model.BuildingId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑楼宇成功";
                return RedirectToAction("Details", "Building", new { id = model.BuildingId });
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
                //backup
                ErrorCode result = this.buildingBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份操场失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                model.Status = 0;
                result = this.buildingBusiness.UpdatePlayground(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑操场失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑操场",
                    Time = DateTime.Now,
                    Type = (int)LogType.BuildingEdit,
                    Content = string.Format("编辑操场， ID:{0}，名称:{1}。", model.BuildingId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑操场成功";
                return RedirectToAction("Details", "Building", new { id = model.BuildingId });
            }

            return View(model);
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FloorCreate(int buildingId)
        {
            ViewBag.BuildingId = buildingId;
            int lastId = this.buildingBusiness.GetLastFloorId();
            ViewData["FloorId"] = lastId + 1;
            return View();
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="model">楼层对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FloorCreate(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            ViewBag.BuildingId = buildingId;

            if (ModelState.IsValid)
            {
                Building building = this.buildingBusiness.Get(buildingId);

                //create
                model.Status = 0;
                ErrorCode result = this.buildingBusiness.CreateFloor(buildingId, model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "添加楼层失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台添加楼层",
                    Time = DateTime.Now,
                    Type = (int)LogType.FloorCreate,
                    Content = string.Format("添加楼层，楼层ID:{0}，名称:{1}，建筑ID:{2}，建筑名称:{3}。", model.FloorId, model.Name, building.BuildingId, building.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(building._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "添加楼层成功";
                return RedirectToAction("Details", "Building", new { id = buildingId });
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
        public ActionResult FloorEdit(int buildingId, int floorId)
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
        public ActionResult FloorEdit(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            ViewBag.BuildingId = buildingId;

            if (ModelState.IsValid)
            {
                Building building = this.buildingBusiness.Get(buildingId);

                //backup
                ErrorCode result = this.buildingBusiness.Backup(building._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份建筑失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                result = this.buildingBusiness.UpdateFloor(buildingId, model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑楼层失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑楼层",
                    Time = DateTime.Now,
                    Type = (int)LogType.FloorEdit,
                    Content = string.Format("编辑楼层， 楼层ID:{0}，名称:{1}，建筑ID:{2}，建筑名称{3}。", model.FloorId, model.Name, building.BuildingId, building.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.buildingBusiness.Log(building._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑楼层成功";
                return RedirectToAction("Details", "Building", new { id = buildingId });
            }

            return View(model);
        }

        /// <summary>
        /// 上传平面图
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UploadSvg(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(buildingId, floorId);
            return View(data);
        }

        /// <summary>
        /// 上传平面图
        /// </summary>
        /// <param name="model">楼层对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UploadSvg(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            string oldSvg = Request.Form["OldSvg"];
            ViewBag.BuildingId = buildingId;

            if (ModelState.IsValid)
            {
                Building building = this.buildingBusiness.Get(buildingId);

                //backup
                string backsvg = this.buildingBusiness.BackupFloorSvg(Request.MapPath("~"), oldSvg);

                ErrorCode result = this.buildingBusiness.Backup(building._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份建筑失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                result = this.buildingBusiness.UpdateSvg(buildingId, model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "修改平面图失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台修改平面图",
                    Time = DateTime.Now,
                    Type = (int)LogType.FloorSvgUpload,
                    Content = string.Format("修改平面图, 楼层ID:{0}, 名称:{1}, 建筑ID:{2}, 建筑名称:{3}, 备份平面图:{4}, 新平面图:{5}.",
                        model.FloorId, model.Name, building.BuildingId, building.Name, backsvg, model.ImageUrl),
                    UserId = user._id,
                    UserName = user.Name,
                    Tag = backsvg
                };

                result = this.buildingBusiness.Log(building._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑平面图成功";
                return RedirectToAction("Details", "Building", new { id = buildingId });
            }

            return View(model);
        }

        /// <summary>
        /// 楼层删除
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FloorDelete(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(buildingId, floorId);

            return View(data);
        }

        /// <summary>
        /// 楼层删除
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("FloorDelete")]
        public ActionResult DeleteFloorConfirm(int buildingId, int floorId)
        {
            Building building = this.buildingBusiness.Get(buildingId);

            //backup
            ErrorCode result = this.buildingBusiness.Backup(building._id);
            if (result != ErrorCode.Success)
            {
                TempData["Message"] = "备份失败";
                return RedirectToAction("FloorDelete", new { buildingId = buildingId, floorId = floorId });
            }

            //delete
            result = this.buildingBusiness.DeleteFloor(buildingId, floorId);
            if (result != ErrorCode.Success)
            {
                TempData["Message"] = "删除失败";
                return View("FloorDelete", new { buildingId = buildingId, floorId = floorId });
            }

            //log
            var user = PageService.GetCurrentUser(User.Identity.Name);
            string fname = Request.Form["Name"];
            Log log = new Log
            {
                Title = "后台删除楼层",
                Time = DateTime.Now,
                Type = (int)LogType.FloorDelete,
                Content = string.Format("删除楼层, 建筑ID:{0}, 建筑名称:{1}, 楼层ID:{2}, 楼层名称:{3}。", building.BuildingId, building.Name, floorId, fname),
                UserId = user._id,
                UserName = user.Name
            };

            result = this.buildingBusiness.Log(building._id, log);
            if (result != ErrorCode.Success)
            {
                TempData["Message"] = "记录日志失败";
                return RedirectToAction("FloorDelete", new { buildingId = buildingId, floorId = floorId });
            }

            TempData["Message"] = "删除成功";
            return RedirectToAction("Details", "Building", new { id = buildingId });
        }
        #endregion //Action
    }
}