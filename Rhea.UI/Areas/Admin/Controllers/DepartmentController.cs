using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Personnel;
using Rhea.Data.Personnel;
using Rhea.Model.Personnel;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Field
        /// <summary>
        /// 部门业务
        /// </summary>
        private IDepartmentBusiness departmentBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (departmentBusiness == null)
            {
                departmentBusiness = new MongoDepartmentBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.departmentBusiness.GetList();
            return View(data);
        }

        /// <summary>
        /// 部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
            return View(data);
        }

        /// <summary>
        /// 部门添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 部门添加
        /// </summary>
        /// <param name="model">部门模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                int result = this.departmentBusiness.Create(model);

                if (result != 0)
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("Details", "Department", new { area = "Admin", id = result });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 部门编辑
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var data = this.departmentBusiness.Get(id, DepartmentAdditionType.ScaleData | DepartmentAdditionType.ResearchData | DepartmentAdditionType.SpecialAreaData);
            return View(data);
        }

        /// <summary>
        /// 部门编辑
        /// </summary>
        /// <param name="model">部门模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.departmentBusiness.Edit(model);

                if (!result)
                {
                    ModelState.AddModelError("", "保存基础信息失败");
                }
                else
                {
                    result = this.departmentBusiness.EditScale(model);
                    if (!result)
                    {
                        ModelState.AddModelError("", "保存规模数据失败");
                        return View(model);
                    }

                    result = this.departmentBusiness.EditResearch(model);
                    if (!result)
                    {
                        ModelState.AddModelError("", "保存科研数据失败");
                        return View(model);
                    }

                    result = this.departmentBusiness.EditSpecialArea(model);
                    if (!result)
                    {
                        ModelState.AddModelError("", "保存特殊面积数据失败");
                        return View(model);
                    }

                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "Department", new { area = "Admin", id = model.Id });
                }
            }

            return View(model);
        }

        /// <summary>
        /// 部门删除
        /// </summary>
        /// <param name="id">cID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = this.departmentBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 部门删除    
        /// </summary>
        /// <param name="id">部门删除ID</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            bool result = this.departmentBusiness.Delete(id);

            if (result)
            {
                TempData["Message"] = "删除成功";
                return RedirectToAction("List", "Department", new { area = "Admin" });
            }
            else
                return View("Delete", id);
        }
        #endregion //Action
    }
}
