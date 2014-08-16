using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Apartment;
using Rhea.UI.Areas.Apartment.Models;
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
        /// <param name="rooms">房间对象</param>
        /// <returns>房间居住对象</returns>
        private IEnumerable<RoomResideModel> BindRoom(IEnumerable<ApartmentRoom> rooms)
        {
            List<RoomResideModel> data = new List<RoomResideModel>();

            foreach (var room in rooms)
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
                }

                data.Add(model);
            }

            return data;
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
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            var rooms = this.roomBusiness.GetByBuilding(buildingId);

            var data = BindRoom(rooms);
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
            var rooms = this.roomBusiness.GetByBuilding(buildingId).Where(r => r.Floor == floor);

            var data = BindRoom(rooms);
            return View("ListByBuilding", data);
        }

        /// <summary>
        /// 房间详细
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            ApartmentRoomModel data = new ApartmentRoomModel();

            data.Room = this.roomBusiness.Get(id);

            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            data.Records = recordBusiness.GetByRoom(id).ToList();

            return View(data);
        }

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ApartmentRoom model)
        {
            if (ModelState.IsValid)
            {
                ApartmentRoom data = this.roomBusiness.Get(model.RoomId);

                data.HouseType = model.HouseType;
                data.Orientation = model.Orientation;
                data.HasAirCondition = model.HasAirCondition;
                data.HasWaterHeater = model.HasWaterHeater;
                data.ResideType = model.ResideType;
                data.Remark = model.Remark;

                ErrorCode result = this.roomBusiness.Update(data);
                if (result == ErrorCode.Success)
                {
                    return RedirectToAction("Details", new { id = model.RoomId });
                }
                else
                {
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
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
        #endregion //Action
    }
}