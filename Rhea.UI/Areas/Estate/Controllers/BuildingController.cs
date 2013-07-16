using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Estate.Models;

namespace Rhea.UI.Areas.Estate.Controllers
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
        #endregion //Function

        #region Action
        /// <summary>
        /// 楼宇主页
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        public ActionResult ListByBuildingGroup(int buildingGroupId)
        {
            var data = this.buildingBusiness.GetListByBuildingGroup(buildingGroupId);
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
            return View(data);
        }

        /// <summary>
        /// 楼宇摘要
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            ViewBag.RoomCount = roomBusiness.CountByBuilding(id);

            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼层视图
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Floor(int id)
        {
            Building data = this.buildingBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼层列表
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public ActionResult FloorByDepartment(int id, int departmentId)
        {
            Building data = this.buildingBusiness.Get(id);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(departmentId, id);
            var floorNumbers = rooms.Select(r => r.Floor).Distinct();

            data.Floors = data.Floors.Where(r => floorNumbers.Contains(r.Number)).ToList();

            return View(data);
        }

        /// <summary>
        /// 楼层列表
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult FloorList(int id)
        {
            Building data = this.buildingBusiness.Get(id);
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
            var data = buildingBusiness.GetListByBuildingGroup(buildingGroupId).OrderBy(r => r.Name);
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
