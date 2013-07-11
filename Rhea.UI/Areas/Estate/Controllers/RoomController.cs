using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Controllers
{
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
        #endregion //Function

        #region Action
        /// <summary>
        /// 房间信息
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.roomBusiness.Get(id);

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            ViewBag.DepartmentName = departmentBusiness.GetName(data.DepartmentId);

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            ViewBag.BuildingName = buildingBusiness.GetName(data.BuildingId);

            return View(data);
        }

        /// <summary>
        /// 房间摘要
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            Room data = this.roomBusiness.Get(id);

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            ViewBag.DepartmentName = departmentBusiness.GetName(data.DepartmentId);

            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public ActionResult ListByBuildingGroup(int buildingGroupId)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetListByBuildingGroup(buildingGroupId);

            List<Room> room = new List<Room>();

            foreach (var building in buildings)
            {
                var r = this.roomBusiness.GetListByBuilding(building.Id);
                room.AddRange(r);
            }

            return View(room);
        }
        #endregion //Action
    }
}
