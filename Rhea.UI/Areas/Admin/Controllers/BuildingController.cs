using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Data;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Admin.Controllers
{
    public class BuildingController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private IBuildingBusiness buildingBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingBusiness == null)
            {
                buildingBusiness = new MongoBuildingBusiness();
            }

            base.Initialize(requestContext);
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        private UserProfile GetUser()
        {
            IAccountBusiness accountBusiness = new MongoAccountBusiness();
            UserProfile user = accountBusiness.GetByUserName(User.Identity.Name);
            return user;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public ActionResult List(int? buildingGroupId)
        {
            if (buildingGroupId == null)
            {
                var data = this.buildingBusiness.GetList().OrderBy(r => r.Id);
                return View(data);
            }
            else
            {
                var data = this.buildingBusiness.GetListByBuildingGroup((int)buildingGroupId, true);
                return View(data);
            }
        }

        /// <summary>
        /// 楼宇信息
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 楼宇添加
        /// </summary>
        /// <param name="model">楼宇模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Building model)
        {
            if (ModelState.IsValid)
            {
                //create
                int bid = this.buildingBusiness.Create(model);
                if (bid == 0)
                {
                    ModelState.AddModelError("", "添加失败");
                    return View(model);
                }

                //log
                var user = GetUser();
                Log log = new Log
                {
                    Title = "添加楼宇",
                    Content = string.Format("添加楼宇, ID:{0}, 名称:{1}.", bid, model.Name),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = (int)LogType.BuildingCreate
                };

                bool logok = this.buildingBusiness.Log(bid, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    return View(model);
                }

                TempData["Message"] = "添加成功";
                return RedirectToAction("Details", "Building", new { area = "Admin", id = bid });
            }

            return View(model);
        }

        /// <summary>
        /// 楼宇编辑
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Building model)
        {
            if (ModelState.IsValid)
            {
                //backup
                bool backok = this.buildingBusiness.Backup(model.Id);
                if (!backok)
                {
                    ModelState.AddModelError("", "备份失败");
                    return View(model);
                }

                //edit
                bool result = this.buildingBusiness.Edit(model);
                if (!result)
                {
                    ModelState.AddModelError("", "保存失败");
                    return View(model);
                }

                //log
                var user = GetUser();
                Log log = new Log
                {
                    Title = "编辑楼宇",
                    Content = string.Format("编辑楼宇, ID:{0}, 名称:{1}.", model.Id, model.Name),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = (int)LogType.BuildingEdit
                };

                bool logok = this.buildingBusiness.Log(model.Id, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    return View(model);
                }

                TempData["Message"] = "编辑成功";
                return RedirectToAction("Details", "Building", new { area = "Admin", id = model.Id });
            }

            return View(model);
        }

        /// <summary>
        /// 楼宇删除
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇删除
        /// POST: /Estate/Builidng/Delete/7
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            //backup
            bool backok = this.buildingBusiness.Backup(id);
            if (!backok)
            {
                TempData["Message"] = "备份失败";
                return View("Delete", id);
            }

            //delete
            bool result = this.buildingBusiness.Delete(id);
            if (!result)
            {
                TempData["Message"] = "删除失败";
                return View("Delete", id);
            }

            //log
            var user = GetUser();
            string bdName = this.buildingBusiness.GetName(id);
            Log log = new Log
            {
                Title = "删除楼宇",
                Content = string.Format("删除楼宇, ID:{0}, 名称:{1}.", id, bdName),
                Time = DateTime.Now,
                UserId = user._id,
                UserName = user.Name,
                Type = (int)LogType.BuildingDelete
            };

            bool logok = this.buildingBusiness.Log(id, log);
            if (!logok)
            {
                TempData["Message"] = "记录日志失败";
                return View("Delete", id);
            }

            TempData["Message"] = "删除成功";
            return RedirectToAction("List", "Building", new { area = "Admin" });
        }

        /// <summary>
        /// 楼层添加
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateFloor(int buildingId)
        {
            ViewBag.BuildingId = buildingId;
            return View();
        }

        /// <summary>
        /// 楼层添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateFloor(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            if (ModelState.IsValid)
            {
                //backup
                bool backok = this.buildingBusiness.Backup(buildingId);
                if (!backok)
                {
                    ModelState.AddModelError("", "备份失败");
                    ViewBag.BuildingId = buildingId;
                    return View(model);
                }

                //create
                int fid = this.buildingBusiness.CreateFloor(buildingId, model);
                if (fid == 0)
                {
                    ModelState.AddModelError("", "添加失败");
                    ViewBag.BuildingId = buildingId;
                    return View(model);
                }

                //log
                var user = GetUser();
                string bdName = this.buildingBusiness.GetName(buildingId);
                Log log = new Log
                {
                    Title = "添加楼层",
                    Content = string.Format("添加楼层, ID:{0}, 名称:{1}, 楼宇ID:{2}, 楼宇名称:{3}.", fid, model.Name, buildingId, bdName),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = (int)LogType.FloorCreate
                };

                bool logok = this.buildingBusiness.Log(buildingId, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ViewBag.BuildingId = buildingId;
                    return View(model);
                }

                TempData["Message"] = "添加成功";
                return RedirectToAction("Details", "Building", new { area = "Admin", id = buildingId });
            }

            ViewBag.BuildingId = buildingId;
            return View(model);
        }

        /// <summary>
        /// 楼层编辑
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>        
        public ActionResult EditFloor(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(floorId);

            return View(data);
        }

        /// <summary>
        /// 楼层编辑
        /// </summary>
        /// <param name="model">楼层数据</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditFloor(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            if (ModelState.IsValid)
            {
                //backup
                bool backok = this.buildingBusiness.Backup(buildingId);
                if (!backok)
                {
                    ModelState.AddModelError("", "备份失败");
                    ViewBag.BuildingId = buildingId;
                    return View(model);
                }

                //edit
                bool result = this.buildingBusiness.EditFloor(buildingId, model);
                if (!result)
                {
                    ModelState.AddModelError("", "保存失败");
                    ViewBag.BuildingId = buildingId;
                    return View(model);
                }

                //log
                var user = GetUser();
                string bdName = this.buildingBusiness.GetName(buildingId);
                Log log = new Log
                {
                    Title = "编辑楼层",
                    Content = string.Format("编辑楼层, ID:{0}, 名称:{1}, 楼宇ID:{2}, 楼宇名称:{3}.", model.Id, model.Name, buildingId, bdName),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = (int)LogType.FloorEdit
                };

                bool logok = this.buildingBusiness.Log(buildingId, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ViewBag.BuildingId = buildingId;
                    return View(model);
                }

                TempData["Message"] = "编辑成功";
                return RedirectToAction("Details", "Building", new { area = "Admin", id = buildingId });
            }

            ViewBag.BuildingId = buildingId;
            return View(model);
        }

        /// <summary>
        /// 楼层删除
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteFloor(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(floorId);

            return View(data);
        }

        /// <summary>
        /// 楼层删除 
        /// </summary>
        /// <param name="id">楼层ID</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteFloor")]
        public ActionResult DeleteFloorConfirm(int id)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);

            //backup
            bool backok = this.buildingBusiness.Backup(buildingId);
            if (!backok)
            {
                TempData["Message"] = "备份失败";
                return View("DeleteFloor", new { buildingId = buildingId, floorId = id });
            }

            //delete
            bool result = this.buildingBusiness.DeleteFloor(buildingId, id);
            if (!result)
            {
                TempData["Message"] = "删除失败";
                return View("DeleteFloor", new { buildingId = buildingId, floorId = id });
            }

            //log
            var user = GetUser();
            string bdName = this.buildingBusiness.GetName(buildingId);
            Log log = new Log
            {
                Title = "删除楼层",
                Content = string.Format("删除楼层, ID:{0}, 楼宇ID:{1}, 楼宇名称:{2}.", id, buildingId, bdName),
                Time = DateTime.Now,
                UserId = user._id,
                UserName = user.Name,
                Type = (int)LogType.FloorDelete
            };

            bool logok = this.buildingBusiness.Log(buildingId, log);
            if (!logok)
            {
                TempData["Message"] = "记录日志失败";
                return View("DeleteFloor", new { buildingId = buildingId, floorId = id });
            }

            TempData["Message"] = "删除成功";
            return RedirectToAction("Details", "Building", new { area = "Admin", id = buildingId });
        }

        /// <summary>
        /// 上传平面图
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UploadSvg(int buildingId, int floorId)
        {
            ViewBag.BuildingId = buildingId;
            var data = this.buildingBusiness.GetFloor(floorId);

            return View(data);
        }

        /// <summary>
        /// 上传平面图
        /// </summary>
        /// <param name="model">楼层模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadSvg(Floor model)
        {
            int buildingId = Convert.ToInt32(Request.Form["BuildingId"]);
            string oldSvg = Request.Form["OldSvg"];
            ViewBag.BuildingId = buildingId;

            //backup
            string backsvg = this.buildingBusiness.BackupFloorSvg(Request.MapPath("~"), oldSvg);
            if (string.IsNullOrEmpty(backsvg))
            {
                ModelState.AddModelError("", "备份SVG失败");
                return View(model);
            }

            bool backok = this.buildingBusiness.Backup(buildingId);
            if (!backok)
            {
                ModelState.AddModelError("", "备份失败");
                return View(model);
            }

            //edit
            bool editok = this.buildingBusiness.EditFloorSvg(buildingId, model.Id, model.ImageUrl);
            if (!editok)
            {
                ModelState.AddModelError("", "修改失败");
                return View(model);
            }

            //log
            var user = GetUser();
            string bdName = this.buildingBusiness.GetName(buildingId);
            Log log = new Log
            {
                Title = "上传楼层平面图",
                Content = string.Format("上传楼层平面图, ID:{0}, 名称:{1}, 楼宇ID:{2}, 楼宇名称:{3}，备份平面图:{4}, 新平面图:{5}.",
                    model.Id, model.Name, buildingId, bdName, backsvg, model.ImageUrl),
                Time = DateTime.Now,
                UserId = user._id,
                UserName = user.Name,
                Type = (int)LogType.FloorSvgUpload,
                Tag = backsvg
            };

            bool logok = this.buildingBusiness.Log(buildingId, log);
            if (!logok)
            {
                ModelState.AddModelError("", "记录日志失败");
                ViewBag.BuildingId = buildingId;
                return View(model);
            }

            TempData["Message"] = "修改平面图成功";
            return RedirectToAction("Details", "Building", new { area = "Admin", id = buildingId });
        }

        /// <summary>
        /// 导出楼宇
        /// </summary>
        /// <returns></returns>
        public FileResult Export()
        {
            byte[] fileContents = this.buildingBusiness.Export();
            return File(fileContents, "application/ms-excel", "building.csv");
        }
        #endregion //Action
    }
}
