using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Model.Apartment;
using Rhea.UI.Areas.Apartment.Models;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    public class RoomController : Controller
    {
        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="rooms">房间对象</param>
        /// <returns>房间居住对象</returns>
        private IEnumerable<RoomResideModel> BindRoom(IEnumerable<ApartmentRoom> rooms)
        {
            List<RoomResideModel> data = new List<RoomResideModel>();
            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();

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

                ResideRecord record = roomBusiness.GetCurrentRecord(room.RoomId);
                if (record != null)
                {
                    model.InhabitantId = record.InhabitantId;
                    model.InhabitantName = record.InhabitantName;
                    model.InhabitantDepartment = record.InhabitantDepartment;
                    model.ResideType = (ResideType)record.ResideType;
                }

                data.Add(model);
            }

            return data;
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
            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            var rooms = roomBusiness.GetByBuilding(buildingId);

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
            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            var rooms = roomBusiness.GetByBuilding(buildingId).Where(r => r.Floor == floor);

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

            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            data.Room = roomBusiness.Get(id);

            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            data.Records = recordBusiness.GetByRoom(id).ToList();

            return View(data);
        }

        /// <summary>
        /// 房间当前住户
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult CurrentInhabitant(int id)
        {
            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            var data = roomBusiness.GetCurrentInhabitant(id);

            return View(data);
        }
        #endregion //Action
    }
}