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
using Rhea.Model.Personnel;

namespace Rhea.UI.Controllers
{
    public class DepartmentController : Controller
    {
        #region Field
        /// <summary>
        /// 部门业务
        /// </summary>
        private IDepartmentService departmentService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (departmentService == null)
            {
                departmentService = new MongoDepartmentService();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 部门主页
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 部门摘要
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            Department data = this.departmentService.Get(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;
            return View(data);
        }

        /// <summary>
        /// 楼宇信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Building(int id, int buildingId)
        {

            return View();
        }
        #endregion //Action

        #region Json        
        #endregion //Json
    }
}
