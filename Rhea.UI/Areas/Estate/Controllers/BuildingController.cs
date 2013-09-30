using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data.Estate;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Areas.Estate.Models;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 楼宇控制器
    /// </summary>
    [EnhancedAuthorize]
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
        #endregion //Function

        #region Action
        /// <summary>
        /// 楼宇主页
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            BuildingSectionModel data = new BuildingSectionModel();

            Building building = this.buildingBusiness.Get(id);
            data.BuildingId = building.Id;
            data.BuildingName = building.Name;
            data.BuildArea = Convert.ToInt32(building.BuildArea);
            data.UsableArea = Convert.ToInt32(this.buildingBusiness.GetUsableArea(id));
            data.Floors = building.Floors;
            data.BuildingGroupId = building.BuildingGroupId;

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            data.RoomCount = roomBusiness.CountByBuilding(id);

            return View(data);
        }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        public ActionResult ListByBuildingGroup(int buildingGroupId)
        {
            var data = this.buildingBusiness.GetListByBuildingGroup(buildingGroupId, true);
            data.ForEach(r => r.UsableArea = this.buildingBusiness.GetUsableArea(r.Id));
            return View(data);
        }

        /// <summary>
        /// 楼宇信息
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            ViewBag.RoomCount = roomBusiness.CountByBuilding(id);

            Building data = this.buildingBusiness.Get(id);
            data.UsableArea = Math.Round(this.buildingBusiness.GetUsableArea(id), RheaConstant.AreaDecimalDigits);
            data.Floors.ForEach(
                r => r.UsableArea = Math.Round(this.buildingBusiness.GetFloorUsableArea(id, r.Number), RheaConstant.AreaDecimalDigits)
            );

            return View(data);
        }

        /// <summary>
        /// 楼宇摘要
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Intro(int id)
        {
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            ViewBag.RoomCount = roomBusiness.CountByBuilding(id);

            Building data = this.buildingBusiness.Get(id);

            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;
            if (!string.IsNullOrEmpty(data.PartMapUrl))
                data.PartMapUrl = RheaConstant.ImagesRoot + data.PartMapUrl;
            data.UsableArea = Math.Round(this.buildingBusiness.GetUsableArea(id), RheaConstant.AreaDecimalDigits);

            return View(data);
        }

        /// <summary>
        /// 楼宇入住部门
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Department(int id)
        {
            List<BuildingDepartmentModel> data = new List<BuildingDepartmentModel>();

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            IRoomBusiness roomBusiness = new MongoRoomBusiness();

            var building = this.buildingBusiness.Get(id);
            var roomList = roomBusiness.GetListByBuilding(building.Id);

            // get departments in one building
            var dList = roomList.GroupBy(r => r.DepartmentId).Select(s => new { s.Key, Count = s.Count(), Area = s.Sum(r => r.UsableArea) });
            foreach (var d in dList)
            {
                BuildingDepartmentModel model = new BuildingDepartmentModel
                {
                    BuildingId = id,
                    DepartmentId = d.Key,
                    RoomCount = d.Count,
                    TotalUsableArea = Math.Round(Convert.ToDouble(d.Area), RheaConstant.AreaDecimalDigits)
                };
                model.DepartmentName = departmentBusiness.GetName(model.DepartmentId);

                data.Add(model);
            }

            return View(data);
        }

        /// <summary>
        /// 楼层信息
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult FloorInfo(int id, int floor)
        {
            var building = this.buildingBusiness.Get(id);
            Floor data = building.Floors.Single(r => r.Number == floor);

            FloorViewModel model = new FloorViewModel
            {
                Id = data.Id,
                BuildingId = id,
                Number = data.Number,
                Name = data.Name,
                BuildArea = Convert.ToDouble(data.BuildArea),
                UsableArea = this.buildingBusiness.GetFloorUsableArea(id, floor),
                AboveGroundFloor = Convert.ToInt32(building.AboveGroundFloor),
                Remark = data.Remark
            };

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            model.RoomCount = roomBusiness.CountByFloor(id, model.Number);

            return View(model);
        }

        /// <summary>
        /// 楼层视图
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult FloorView(int id, int floor)
        {
            var building = this.buildingBusiness.Get(id);

            Floor data = building.Floors.Single(r => r.Number == floor);

            FloorViewModel model = new FloorViewModel
            {
                Id = data.Id,
                BuildingId = id,
                Number = data.Number,
                Name = data.Name,
                BuildArea = Convert.ToDouble(data.BuildArea),
                UsableArea = this.buildingBusiness.GetFloorUsableArea(id, floor)
            };

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            model.RoomCount = roomBusiness.CountByFloor(id, model.Number);

            if (!string.IsNullOrEmpty(data.ImageUrl))
                model.SvgPath = RheaConstant.SvgRoot + data.ImageUrl;
            else
                model.SvgPath = string.Empty;

            return View(model);
        }

        /// <summary>
        /// 楼层入住部门
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult FloorDepartment(int id, int floor)
        {
            List<FloorDepartmentModel> data = new List<FloorDepartmentModel>();

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            IRoomBusiness roomBusiness = new MongoRoomBusiness();

            var building = this.buildingBusiness.Get(id);
            var roomList = roomBusiness.GetListByBuilding(building.Id, floor);

            // get departments in one building
            var dList = roomList.GroupBy(r => r.DepartmentId).Select(s => new { s.Key, Count = s.Count(), Area = s.Sum(r => r.UsableArea) });
            foreach (var d in dList)
            {
                FloorDepartmentModel model = new FloorDepartmentModel
                {
                    BuildingId = id,
                    DepartmentId = d.Key,
                    Floor = floor,
                    RoomCount = d.Count,
                    TotalUsableArea = Convert.ToDouble(d.Area)
                };

                model.DepartmentName = departmentBusiness.GetName(model.DepartmentId);

                var result = data.Find(r => r.DepartmentId == model.DepartmentId);
                if (result != null)
                {
                    result.RoomCount += model.RoomCount;
                    result.TotalUsableArea = model.TotalUsableArea;
                }
                else
                    data.Add(model);
            }

            return View(data);
        }

        /// <summary>
        /// 分类统计
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Classify(int id)
        {
            IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
            BuildingClassifyAreaModel data = statisticBusiness.GetBuildingClassifyArea(id, false);

            return View(data);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 按楼群得到楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        public JsonResult GetListByBuildingGroup(int buildingGroupId)
        {
            var data = buildingBusiness.GetListByBuildingGroup(buildingGroupId, true);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 得到部门占用楼宇
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public JsonResult GetListByDepartment(int departmentId)
        {
            var data = this.buildingBusiness.GetListByDepartment(departmentId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 得到楼宇内房间数量
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public JsonResult GetRoomCount(int buildingId)
        {
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            int count = roomBusiness.CountByBuilding(buildingId);
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 得到楼层信息
        /// </summary>
        /// <param name="floorId">楼层ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public JsonResult GetFloorInfo(int floorId, int buildingId)
        {
            var floor = this.buildingBusiness.GetFloor(floorId);

            FloorViewModel model = new FloorViewModel
            {
                Id = floor.Id,
                Number = floor.Number,
                BuildArea = Convert.ToDouble(floor.BuildArea),
                UsableArea = Convert.ToDouble(floor.UsableArea)
            };

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            model.RoomCount = roomBusiness.CountByFloor(buildingId, model.Number);

            if (!string.IsNullOrEmpty(floor.ImageUrl))
                model.SvgPath = RheaConstant.SvgRoot + floor.ImageUrl;
            else
                model.SvgPath = string.Empty;

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
