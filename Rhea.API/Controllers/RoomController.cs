using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.API.Controllers
{
    public class RoomController : ApiController
    {
        #region Field
        /// <summary>
        /// 房间业务
        /// </summary>
        private IRoomBusiness roomBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            if (roomBusiness == null)
            {
                roomBusiness = new MongoRoomBusiness();
            }

            base.Initialize(controllerContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房间数据
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var data = this.roomBusiness.GetList();
            HttpResponseMessage message = Request.CreateResponse<List<Room>>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 房间数据
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            var data = this.roomBusiness.Get(id);
            HttpResponseMessage message = Request.CreateResponse<Room>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 房间数据
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        public HttpResponseMessage GetByBuildingGroup(int buildingGroupId)
        {
            var data = this.roomBusiness.GetListByBuildingGroup(buildingGroupId);
            HttpResponseMessage message = Request.CreateResponse<List<Room>>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 房间数据
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public HttpResponseMessage GetByBuilding(int buildingId)
        {
            var data = this.roomBusiness.GetListByBuilding(buildingId);
            HttpResponseMessage message = Request.CreateResponse<List<Room>>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 房间数据
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public HttpResponseMessage GetByDepartment(int departmentId)
        {
            var data = this.roomBusiness.GetListByDepartment(departmentId);
            HttpResponseMessage message = Request.CreateResponse<List<Room>>(HttpStatusCode.OK, data);
            return message;
        }
        #endregion //Action
    }
}
