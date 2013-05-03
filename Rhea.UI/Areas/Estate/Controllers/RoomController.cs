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
        /// <summary>
        /// 模型转换
        /// </summary>
        /// <param name="model">房间编辑模型</param>
        /// <returns>房间模型</returns>
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

            IBuildingService buildingService = new MongoBuildingService();
            Building building = buildingService.Get(model.BuildingId);
            room.Building = new Room.RoomBuilding
            {
                Id = building.Id,
                Name = building.Name
            };

            EstateService service = new EstateService();
            RoomFunctionCode code = service.GetFunctionCodeList().First(r => r.CodeId == model.FunctionCodeId);
            room.Function = new Room.RoomFunction
            {
                FirstCode = code.FirstCode,
                SecondCode = code.SecondCode,
                Property = code.FunctionProperty
            };

            DepartmentService dservice = new DepartmentService();
            Department department = dservice.GetDepartment(model.DepartmentId);
            room.Department = new Room.RoomDepartment
            {
                Id = department.Id,
                Name = department.Name
            };

            return room;
        }

        /// <summary>
        /// 模型转换
        /// </summary>
        /// <param name="model">房间模型</param>
        /// <returns>房间编辑模型</returns>
        private RoomEditModel ModelTranslate(Room model)
        {
            RoomEditModel room = new RoomEditModel();
            room.Id = model.Id;
            room.Name = model.Name;
            room.Number = model.Number;
            room.Floor = model.Floor;
            room.Span = model.Span;
            room.Orientation = model.Orientation;
            room.BuildArea = model.BuildArea;
            room.UsableArea = model.UsableArea;
            room.FunctionCodeId = model.Function.FirstCode.ToString() + "." + model.Function.SecondCode.ToString();
            room.BuildingId = model.Building.Id;
            room.DepartmentId = model.Department.Id;
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
        /// 房间摘要
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
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
        /// 房间列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public ActionResult ListByDepartment(int departmentId)
        {
            EstateService service = new EstateService();
            List<Room> data = service.GetRoomByDepartment(departmentId).ToList();

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

        /// <summary>
        /// 房间添加
        /// </summary>
        /// <param name="model">房间模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(RoomEditModel model)
        {           
            if (ModelState.IsValid)
            {
                Room room = ModelTranslate(model);               
                EstateService service = new EstateService();
                int result = service.CreateRoom(room);

                if (result != 0)
                {
                    return RedirectToAction("Index", "Room", new { area = "Estate", id = result });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
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
            EstateService service = new EstateService();
            Room room = service.GetRoom(id);
            
            RoomEditModel model = ModelTranslate(room);
            return View(model);
        }

        /// <summary>
        /// 房间编辑
        /// </summary>
        /// <param name="model">房间模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(RoomEditModel model)
        {
            if (ModelState.IsValid)
            {
                Room room = ModelTranslate(model);
                room.Id = model.Id;
                EstateService service = new EstateService();
                bool result = service.UpdateRoom(room);
                if (result)
                {
                    return RedirectToAction("Index", "Room", new { area = "Estate", id = room.Id });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
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

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            EstateService service = new EstateService();
            bool result = service.DeleteRoom(id);

            if (result)
            {
                return RedirectToAction("List", "BuildingGroup", new { area = "Estate" });
            }
            else
                return View("Delete", id);
        }
        #endregion //Action
    }
}
