﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 房产管理主控制器
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        #region Action
        /// <summary>
        /// 房产管理首页
        /// GET: /Estate/Home/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 部门导航列表
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult DepartmentNav()
        {
            IDepartmentService departmentService = new MongoDepartmentService();
            List<Department> departments = departmentService.GetList();

            return View(departments);
        }

        /// <summary>
        /// 地图导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Go(int bg)
        {
            return View();
        }
        #endregion //Action
    }
}
