using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 校区控制器
    /// </summary>
    public class CampusController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private CampusBusiness campusBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (campusBusiness == null)
            {
                campusBusiness = new CampusBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        // GET: Admin/Campus
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 校区列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.campusBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 校区信息
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.campusBusiness.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}