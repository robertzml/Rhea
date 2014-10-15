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
    /// 校区控制器
    /// </summary>
    public class CampusController : ApiController
    {
        #region Field
        /// <summary>
        /// 校区业务
        /// </summary>
        private CampusBusiness campusBusiness;
        #endregion //Field

        #region Constructor
        public CampusController()
        {
            this.campusBusiness = new CampusBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 获取所有校区数据
        /// </summary>
        public HttpResponseMessage Get()
        {
            var data = this.campusBusiness.Get().ToList();
            HttpResponseMessage message = Request.CreateResponse<List<Campus>>(HttpStatusCode.OK, data);
            return message;
        }

        /// <summary>
        /// 获取校区数据
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns>
        /// 校区数据
        /// </returns>
        public Campus Get(int id)
        {
            var data = this.campusBusiness.Get(id);

            return data;
        }
        #endregion //Action
    }
}
