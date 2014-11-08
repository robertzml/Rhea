using Rhea.Business.Personnel;
using Rhea.Model.Personnel;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Institute.Controllers
{
    /// <summary>
    /// 部门管理主控制器
    /// </summary>
    [Privilege(Require = "InstituteManage")]
    public class HomeController : Controller
    {
        #region Action
        /// <summary>
        /// 部门管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            DepartmentBusiness departmentBusiness = new DepartmentBusiness();
            var data = departmentBusiness.Get().OrderBy(r => r.DepartmentId);
            ViewBag.Departments = data;

            return View();
        }

        /// <summary>
        /// 部门菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentMenu()
        {
            DepartmentBusiness departmentBusiness = new DepartmentBusiness();
            var data = departmentBusiness.Get().OrderBy(r => r.DepartmentId);
            
            return View(data);
        }
        #endregion //Action
    }
}