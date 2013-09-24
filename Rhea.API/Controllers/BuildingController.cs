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
    public class BuildingController : ApiController
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private IBuildingBusiness buildingBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            if (buildingBusiness == null)
            {
                buildingBusiness = new MongoBuildingBusiness();
            }

            base.Initialize(controllerContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 所有楼宇数据
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var data = this.buildingBusiness.GetList();
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

        /// <summary>
        /// 楼宇数据
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public HttpResponseMessage GetByBuildingGroup(int buildingGroupId)
        {
            var data = this.buildingBusiness.GetListByBuildingGroup(buildingGroupId);
            HttpResponseMessage message = Request.CreateResponse<List<Building>>(HttpStatusCode.OK, data);
            return message;
        }
        #endregion //Action
    }
}
