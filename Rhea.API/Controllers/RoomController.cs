using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rhea.Business.Estate;
using Rhea.Model.Estate;

namespace Rhea.API.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    public class RoomController : ApiController
    {
        #region Field
        /// <summary>
        /// 房间业务对象
        /// </summary>
        private RoomBusiness roomBusiness;
        #endregion //Field

        #region Constructor
        public RoomController()
        {
            this.roomBusiness = new RoomBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 所有房间数据
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var data = this.roomBusiness.Get().ToList();
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
        #endregion //Action
    }
}
