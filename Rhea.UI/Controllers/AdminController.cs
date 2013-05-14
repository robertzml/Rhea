using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;

namespace Rhea.UI.Controllers
{
    /// <summary>
    /// 系统管理控制器
    /// </summary>
    public class AdminController : Controller
    {
        #region Field
        private IAdminService adminService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (adminService == null)
            {
                adminService = new MongoAdminService();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 系统管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 管理组列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagerGroupList()
        {           
            var data = this.adminService.GetManagerGroupList();
            return View(data);
        }

        /// <summary>
        /// 用户组列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserGroupList()
        {
            var data = this.adminService.GetUserGroupList();
            return View(data);
        }

        /// <summary>
        /// 添加管理组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateManagerGroup()
        {
            return View();
        }
        #endregion //Action
    }
}
