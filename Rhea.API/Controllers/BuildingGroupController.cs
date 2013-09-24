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
    public class BuildingGroupController : ApiController
    {
        #region Field
        /// <summary>
        /// 楼群业务
        /// </summary>
        private IBuildingGroupBusiness buildingGroupBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            if (buildingGroupBusiness == null)
            {
                buildingGroupBusiness = new MongoBuildingGroupBusiness();
            }

            base.Initialize(controllerContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 所有楼群数据
        /// </summary>
        /// <returns></returns>        
        public HttpResponseMessage Get()
        {
            var data = this.buildingGroupBusiness.GetList();
            HttpResponseMessage response = Request.CreateResponse<List<BuildingGroup>>(HttpStatusCode.OK, data);
            return response;
        }

        /// <summary>
        /// 楼群数据
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            var data = this.buildingGroupBusiness.Get(id);
            HttpResponseMessage response = Request.CreateResponse<BuildingGroup>(HttpStatusCode.OK, data);
            return response;
        }
        #endregion //Action
    }
}
