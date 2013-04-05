using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;
using Rhea.UI.Areas.Estate.Models;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    public class RoomController : Controller
    {
        #region Function
        private Room ModelTranslate(RoomEditModel model)
        {
            Room room = new Room();
            room.Name = model.Name;
            room.Number = model.Number;
            room.Floor = model.Floor;
            room.Span = model.Span;
            room.Orientation = model.Orientation;
            room.BuildArea = model.BuildArea;
            room.UsableArea = model.UsableArea;
            //room.Function 
            //room.Building
            //room.Department
            room.StartDate = model.StartDate;
            room.FixedYear = model.FixedYear;
            room.Manager = model.Manager;
            room.RoomStatus = model.RoomStatus;
            room.Remark = model.Remark;
            room.Status = 0;

            room.Heating = model.Heating;
            room.FireControl = model.FireControl;
            room.Height = model.Height;
            room.SNWidth = model.SNWidth;
            room.EWWidth = model.EWWidth;
            room.InternationalId = model.InternationalId;
            room.EducationId = model.EducationId;
            room.PowerSupply = model.PowerSupply;
            room.AirCondition = model.AirCondition;
            room.HasSecurity = model.HasSecurity;
            room.HasChemical = model.HasChemical;
            room.HasTrash = model.HasTrash;
            room.HasSecurityCheck = model.HasSecurityCheck;
            room.PressureContainer = model.PressureContainer;
            room.Cylinder = model.Cylinder;
            room.HeatingInAeration = model.HeatingInAeration;
            room.HasTestBed = model.HasTestBed;
            room.UsageCharge = model.UsageCharge;

            return room;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房间主页
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 房间信息
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            EstateService service = new EstateService();
            var data = service.GetRoom(id);            
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult List(int buildingId, int floor = 0)
        {
            EstateService service = new EstateService();
            List<Room> data;
            if (floor == 0)
            {
                data = service.GetRoomByBuilding(buildingId).OrderBy(r => r.Id).ToList();
            }
            else
            {
                data = service.GetRoomByBuilding(buildingId, floor).OrderBy(r => r.Id).ToList();
            }

            return View(data);
        }

        /// <summary>
        /// 房间添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Room model)
        {           
            if (ModelState.IsValid)
            {
                
                ModelState.AddModelError("", "valid");
            }
            else
            {
                ModelState.AddModelError("", "error2");
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
            return View();
        }

        /// <summary>
        /// 房间删除
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            EstateService service = new EstateService();
            var data = service.GetRoom(id);
            return View(data);
        }
        #endregion //Action
    }
}
