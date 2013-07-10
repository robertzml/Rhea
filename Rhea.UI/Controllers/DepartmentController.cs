using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Personnel;
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
        public ActionResult Index(int id)
        {
            return View(id);
        }
        #endregion //Action
    }
}
