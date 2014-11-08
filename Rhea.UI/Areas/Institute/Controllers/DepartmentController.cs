using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Personnel;
using Rhea.Model.Personnel;

namespace Rhea.UI.Areas.Institute.Controllers
{
    /// <summary>
    /// 部门控制器
    /// </summary>
    public class DepartmentController : Controller
    {
        #region Field
        /// <summary>
        /// 部门业务对象
        /// </summary>
        private DepartmentBusiness departmentBusiness;
        #endregion //Field

        #region Constructor
        public DepartmentController()
        {
            this.departmentBusiness = new DepartmentBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 部门主页
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var data = this.departmentBusiness.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}