using Rhea.API.Models;
using Rhea.Business.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Rhea.API.Controllers
{
    /// <summary>
    /// 校区控制器
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

        #region Function
        /// <summary>
        /// 模型映射
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Campus BindCampus(Rhea.Model.Estate.Campus model)
        {
            if (model == null)
                return null;

            Campus campus = new Campus();
            campus.Id = model.CampusId;
            campus.Name = model.Name;
            campus.Remark = model.Remark;
            campus.LastUpdateTime = model.Log.Time;

            return campus;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 获取所有校区数据
        /// </summary>
        public IEnumerable<Campus> Get()
        {
            var model = this.campusBusiness.Get().ToList();

            List<Campus> data = new List<Campus>();
            foreach (var item in model)
            {
                data.Add(BindCampus(item));
            }

            return data;
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
            Campus campus = BindCampus(data);

            return campus;
        }
        #endregion //Action
    }
}
