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
    public class BuildingGroupController : ApiController
    {
        #region Action
        /// <summary>
        /// 显示所有楼群
        /// </summary>
        /// <returns></returns>        
        public HttpResponseMessage GetAll()
        {
            IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
            var data = buildingGroupBusiness.GetList();
            
            HttpResponseMessage response = Request.CreateResponse<List<BuildingGroup>>(HttpStatusCode.OK, data);
            return response;
            
        }
        #endregion //Action
    }
}
