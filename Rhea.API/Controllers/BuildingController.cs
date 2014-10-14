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
    /// 建筑控制器
    /// </summary>
    public class BuildingController : ApiController
    {
        #region Field
        /// <summary>
        /// 建筑业务对象
        /// </summary>
        private BuildingBusiness buildingBusiness;
        #endregion //Field

        #region Constructor
        public BuildingController()
        {
            this.buildingBusiness = new BuildingBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 所有建筑数据
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var data = this.buildingBusiness.Get().ToList();
            HttpResponseMessage message = Request.CreateResponse<List<Building>>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 楼宇数据
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            var data = this.buildingBusiness.Get(id);
            HttpResponseMessage message = Request.CreateResponse<Building>(HttpStatusCode.OK, data);
            return message;
        }
        #endregion //Action
    }
}
