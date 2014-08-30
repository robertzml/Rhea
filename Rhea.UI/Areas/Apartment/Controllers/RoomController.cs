using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Apartment;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Apartment.Models;
using Rhea.UI.Filters;
using Rhea.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Apartment,Leader")]
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 青教房间业务对象
        /// </summary>
        private ApartmentRoomBusiness roomBusiness;
        #endregion //Field

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="room">房间对象</param>
        /// <returns>房间居住对象</returns>
        private RoomResideModel BindRoom(ApartmentRoom room)
        {
            RoomResideModel model = new RoomResideModel();

            model.RoomId = room.RoomId;
            model.Name = room.Name;
            model.Number = room.Number;
            model.Floor = room.Floor;
            model.UsableArea = room.UsableArea;
            model.HouseType = room.HouseType;
            model.BuildingName = room.BuildingName();
            model.RoomResideType = (ResideType)room.ResideType;

            ResideRecord record = this.roomBusiness.GetCurrentRecord(room.RoomId);
            if (record != null)
            {
                model.InhabitantId = record.InhabitantId;
                model.InhabitantName = record.InhabitantName;
                model.InhabitantDepartment = record.InhabitantDepartment;
                model.RecordStatus = record.Status;
            }
            else
                model.RecordStatus = -1;

            return model;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (roomBusiness == null)
            {
                roomBusiness = new ApartmentRoomBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房间列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            List<RoomResideModel> data = new List<RoomResideModel>();
            var rooms = this.roomBusiness.Get();

            foreach (var room in rooms)
            {
                var item = BindRoom(room);
                data.Add(item);
            }

            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            List<RoomResideModel> data = new List<RoomResideModel>();
            var rooms = this.roomBusiness.GetByBuilding(buildingId);

            foreach (var room in rooms)
            {
                var item = BindRoom(room);
                data.Add(item);
            }

            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult ListByFloor(int buildingId, int floor)
        {
            List<RoomResideModel> data = new List<RoomResideModel>();
            var rooms = this.roomBusiness.GetByBuilding(buildingId).Where(r => r.Floor == floor);

            foreach (var room in rooms)
            {
                var item = BindRoom(room);
                data.Add(item);
            }

            return View("ListByBuilding", data);
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
        /// 房间摘要
        /// </summary>
        /// <remarks>
        /// SVG选择后显示信息
        /// </remarks>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            var room = this.roomBusiness.Get(id);
            RoomResideModel data = BindRoom(room);

            return View(data);
        }

        /// <summary>
        /// 房间当前住户
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult CurrentInhabitant(int id)
        {
            var data = this.roomBusiness.GetCurrentInhabitant(id);

            return View(data);
        }

        /// <summary>
        /// 房间树列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Tree()
        {
            RoomTreeModel data = new RoomTreeModel();

            BuildingBusiness business = new BuildingBusiness();
            data.Blocks = business.GetChildBlocks(RheaConstant.ApartmentBuildingId).ToList();

            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            data.Rooms = roomBusiness.GetByBuilding(RheaConstant.ApartmentBuildingId).OrderBy(r => r.Number).ToList();

            return View(data);
        }

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ApartmentRoom data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="model">房间对象</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ApartmentRoom model)
        {
            if (ModelState.IsValid)
            {
                //backup
                ErrorCode result = this.roomBusiness.Backup(model._id);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "备份房间失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //edit
                ApartmentRoom data = this.roomBusiness.Get(model.RoomId);
                data.HouseType = model.HouseType;
                data.Orientation = model.Orientation;
                data.HasAirCondition = model.HasAirCondition;
                data.HasWaterHeater = model.HasWaterHeater;
                data.ResideType = model.ResideType;
                data.Remark = model.Remark;

                result = this.roomBusiness.Update(data);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑房间失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "编辑青教房间",
                    Time = DateTime.Now,
                    Type = (int)LogType.RoomEdit,
                    Content = string.Format("编辑青教房间， ID:{0}, 名称:{1}。", model.RoomId, model.Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.roomBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                return RedirectToAction("Details", new { id = model.RoomId });
            }

            return View(model);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 获取可分配房间
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public JsonResult GetAvailableRooms(int buildingId)
        {
            var data = this.roomBusiness.GetByBuilding(buildingId).Where(r => r.ResideType == (int)ResideType.Available).OrderBy(r => r.Number);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取房间数据
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public JsonResult GetRoom(int id)
        {
            var data = this.roomBusiness.Get(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}