using Rhea.Business;
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
    [EnhancedAuthorize(Rank = 900)]
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 房间业务对象
        /// </summary>
        private RoomBusiness roomBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (roomBusiness == null)
            {
                roomBusiness = new RoomBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房间管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.roomBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 根据建筑获取房间
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            BuildingBusiness buildingBusiness = new BuildingBusiness();
            var building = buildingBusiness.Get(buildingId);
            ViewBag.BuildingName = building.Name;
            ViewBag.BuildingId = buildingId;

            var data = this.roomBusiness.GetByBuilding(buildingId);
            return View(data);
        }

        /// <summary>
        /// 房间详细
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 添加公用房
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            int lastId = this.roomBusiness.GetLastRoomId(1);
            ViewData["RoomId"] = lastId + 1;
            return View();
        }

        /// <summary>
        /// 添加公用房
        /// </summary>
        /// <param name="model">房间模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Room model)
        {
            if (ModelState.IsValid)
            {
                //create
                string codeId = Request.Form["FunctionCodeId"];
                DictionaryBusiness dictionaryBusiness = new DictionaryBusiness();
                RoomFunctionCode codes = dictionaryBusiness.GetRoomFunctionCodes().Single(r => r.CodeId == codeId);

                model.Function = codes;
                model.Status = 0;
                ErrorCode result = this.roomBusiness.Create(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "添加房间失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "添加房间",
                    Time = DateTime.Now,
                    Type = (int)LogType.RoomCreate,
                    Content = string.Format("添加房间， ID:{0}, 名称:{1}。", model.RoomId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.roomBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "添加房间成功";
                return RedirectToAction("Details", new { controller = "Room", id = model.RoomId });
            }

            return View(model);
        }

        /// <summary>
        /// 房间编辑
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 房间编辑
        /// </summary>
        /// <param name="model">房间模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Room model)
        {
            if (ModelState.IsValid)
            {
                //backup
                ErrorCode result = this.roomBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "备份房间失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                string codeId = Request.Form["FunctionCodeId"];
                DictionaryBusiness dictionaryBusiness = new DictionaryBusiness();
                RoomFunctionCode codes = dictionaryBusiness.GetRoomFunctionCodes().Single(r => r.CodeId == codeId);

                model.Function = codes;
                model.Status = 0;
                result = this.roomBusiness.Update(model);

                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "编辑房间失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "编辑房间",
                    Time = DateTime.Now,
                    Type = (int)LogType.RoomEdit,
                    Content = string.Format("编辑房间， ID:{0}, 名称:{1}。", model.RoomId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.roomBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑房间成功";
                return RedirectToAction("Details", new { controller = "Room", id = model.RoomId });
            }

            return View(model);
        }
        #endregion //Action
    }
}