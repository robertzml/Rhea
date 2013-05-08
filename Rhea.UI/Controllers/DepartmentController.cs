using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Data.Entities;
using Rhea.Data.Estate;

namespace Rhea.UI.Controllers
{
    /// <summary>
    /// 部门控制器
    /// </summary>
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
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 部门详细
        /// /Department/Details/7
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Department data = this.departmentService.Get(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;
            return View(data);
        }

        /// <summary>
        /// 部门树形列表
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Tree()
        {
            List<Department> departments = departmentService.GetList();
            return View(departments);
        }

        /// <summary>
        /// 分楼宇用房面积
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult BuildingArea(int id)
        {
            IStatisticService statisticService = new MongoStatisticService();
            IBuildingService buildingService = new MongoBuildingService();
            List<Building> buildings = buildingService.GetList();

            List<BuildingAreaModel> data = statisticService.GetBuildingArea(id, buildings);
            return View(data);
        }

        /// <summary>
        /// 部门编辑
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Department data = this.departmentService.Get(id);
            return View(data);
        }

        /// <summary>
        /// 部门编辑
        /// </summary>
        /// <param name="model">部门数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.departmentService.Edit(model);

                if (result)
                {
                    return RedirectToAction("Index", "Department", new { id = model.Id });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }
            return View(model);
        }
        #endregion //Action
    }
}
