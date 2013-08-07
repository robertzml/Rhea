using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Admin.Models;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 房间业务
        /// </summary>
        private IRoomBusiness roomBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (roomBusiness == null)
            {
                roomBusiness = new MongoRoomBusiness();
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
            room.ImageUrl = model.ImageUrl;
            room.BuildingId = model.BuildingId;
            room.DepartmentId = model.DepartmentId;
            room.StartDate = model.StartDate;
            room.FixedYear = model.FixedYear;
            //room.Manager = model.Manager;
            room.PersonNumber = model.PersonNumber;
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

            RoomFunctionCode code = this.roomBusiness.GetFunctionCodeList().First(r => r.CodeId == model.FunctionCodeId);
            room.Function = new Room.RoomFunction
            {
                FirstCode = code.FirstCode,
                SecondCode = code.SecondCode,
                ClassifyName = code.ClassifyName,
                Property = code.FunctionProperty
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
            room.ImageUrl = model.ImageUrl;
            room.FunctionCodeId = model.Function.FirstCode.ToString() + "." + model.Function.SecondCode.ToString();
            room.BuildingId = model.BuildingId;
            room.DepartmentId = model.DepartmentId;
            room.StartDate = model.StartDate;
            room.FixedYear = model.FixedYear;
            //room.Manager = model.Manager;
            room.PersonNumber = model.PersonNumber;
            room.RoomStatus = model.RoomStatus;
            room.Remark = model.Remark;

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
        /// 房间管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房间信息
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.roomBusiness.GetList().OrderBy(r => r.Id);
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();

            ViewBag.BuildingName = buildingBusiness.GetName(buildingId);
            ViewBag.BuildingId = buildingId;

            var data = this.roomBusiness.GetListByBuilding(buildingId).OrderBy(r => r.Id);
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(RoomEditModel model)
        {
            if (ModelState.IsValid)
            {
                Room room = ModelTranslate(model);
                var user = GetUser();
                int result = this.roomBusiness.Create(room, user);

                if (result != 0)
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("Details", "Room", new { area = "Admin", id = result });
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
            Room room = this.roomBusiness.Get(id);

            RoomEditModel model = ModelTranslate(room);
            return View(model);
        }

        /// <summary>
        /// 房间编辑
        /// </summary>
        /// <param name="model">房间模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(RoomEditModel model)
        {
            if (ModelState.IsValid)
            {
                Room room = ModelTranslate(model);
                room.Id = model.Id;
                var user = GetUser();

                bool backok = this.roomBusiness.Backup(room.Id);
                if (!backok)
                {
                    ModelState.AddModelError("", "备份失败");
                    return View(model);
                }

                bool result = this.roomBusiness.Edit(room, user);
                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "Room", new { area = "Admin", id = room.Id });
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
            var data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var user = GetUser();

            bool backok = this.roomBusiness.Backup(id);
            if (!backok)
            {
                ModelState.AddModelError("", "备份失败");
                return View("Delete", id);
            }

            bool result = this.roomBusiness.Delete(id, user);
            if (result)
            {
                TempData["Message"] = "删除成功";
                return RedirectToAction("Index", "Room", new { area = "Admin" });
            }
            else
                return View("Delete", id);
        }
        #endregion //Action
    }
}
