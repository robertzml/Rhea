using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Rhea.Business.Personnel;
using Rhea.Model.Personnel;

namespace Rhea.API.Controllers
{
    public class DepartmentController : ApiController
    {
        #region Field
        /// <summary>
        /// 房间业务
        /// </summary>
        private IDepartmentBusiness departmentBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            if (departmentBusiness == null)
            {
                departmentBusiness = new MongoDepartmentBusiness();
            }

            base.Initialize(controllerContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 部门数据
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var data = this.departmentBusiness.GetList();
            HttpResponseMessage message = Request.CreateResponse<List<Department>>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 部门数据
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            var data = this.departmentBusiness.Get(id);
            HttpResponseMessage message = Request.CreateResponse<Department>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 部门数据
        /// </summary>
        /// <param name="type">部门类型</param>
        /// <returns></returns>
        public HttpResponseMessage GetByType(int type)
        {
            var data = this.departmentBusiness.GetList();
            data = data.Where(r => r.Type == type).ToList();
            HttpResponseMessage message = Request.CreateResponse<List<Department>>(HttpStatusCode.OK, data);
            return message;
        }
        #endregion //Action
    }
}
