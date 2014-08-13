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
        private IEnumerable<RoomResideModel> BindRoom(IEnumerable<ApartmentRoom> rooms, IEnumerable<ResideRecord> records)
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
                model.BuildingName = room.BuildingName();

                //ResideRecord record = records.Where(r => r.RoomId == room.RoomId)

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
            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var records = recordBusiness.GetByBuilding(buildingId);

            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            var rooms = roomBusiness.GetByBuilding(buildingId);

            var data = BindRoom(rooms, records);

            return View(data);
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
        #endregion //Action
    }
}