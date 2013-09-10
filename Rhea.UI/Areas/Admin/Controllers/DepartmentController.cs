using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Business.Personnel;
using Rhea.Data;
using Rhea.Data.Personnel;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Personnel;
using Rhea.UI.Areas.Admin.Models;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [EnhancedAuthorize(Roles = "Root,Administrator")]
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

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        private UserProfile GetUser()
        {
            IAccountBusiness accountBusiness = new MongoAccountBusiness();
            UserProfile user = accountBusiness.GetByUserName(User.Identity.Name);
            return user;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 部门管理主页
        /// </summary>
        /// <returns></returns>
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
                //create
                int did = this.departmentBusiness.Create(model);
                if (did == 0)
                {
                    ModelState.AddModelError("", "添加失败");
                    return View(model);
                }

                //log
                var user = GetUser();
                Log log = new Log
                {
                    Title = "添加部门",
                    Content = string.Format("添加部门, ID:{0}, 名称:{1}.", did, model.Name),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = (int)LogType.DepartmentCreate
                };

                bool logok = this.departmentBusiness.Log(did, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    return View(model);
                }

                TempData["Message"] = "添加成功";
                return RedirectToAction("Details", "Department", new { area = "Admin", id = did });
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
                //backup
                bool backok = this.departmentBusiness.Backup(model.Id);
                if (!backok)
                {
                    ModelState.AddModelError("", "备份失败");
                    return View(model);
                }

                //edit
                bool result = this.departmentBusiness.Edit(model);

                if (!result)
                {
                    ModelState.AddModelError("", "保存基础信息失败");
                    return View(model);
                }

                if (model.Type == (int)DepartmentType.Type1)
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
                }
                else
                {
                    result = this.departmentBusiness.EditScale(model);
                    if (!result)
                    {
                        ModelState.AddModelError("", "保存规模数据失败");
                        return View(model);
                    }
                }

                //log
                var user = GetUser();
                Log log = new Log
                {
                    Title = "编辑部门",
                    Content = string.Format("编辑部门, ID:{0}, 名称:{1}.", model.Id, model.Name),
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    Type = (int)LogType.DepartmentEdit
                };

                bool logok = this.departmentBusiness.Log(model.Id, log);
                if (!logok)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    return View(model);
                }

                TempData["Message"] = "编辑成功";
                return RedirectToAction("Details", "Department", new { area = "Admin", id = model.Id });
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
            //backup
            bool backok = this.departmentBusiness.Backup(id);
            if (!backok)
            {
                TempData["Message"] = "备份失败";
                return View("Delete", id);
            }

            //delete
            bool result = this.departmentBusiness.Delete(id);
            if (!result)
            {
                TempData["Message"] = "删除失败";
                return View("Delete", id);
            }

            //log
            var user = GetUser();
            string dName = this.departmentBusiness.GetName(id);
            Log log = new Log
            {
                Title = "删除部门",
                Content = string.Format("删除部门, ID:{0}, 名称:{1}.", id, dName),
                Time = DateTime.Now,
                UserId = user._id,
                UserName = user.Name,
                Type = (int)LogType.DepartmentDelete
            };

            bool logok = this.departmentBusiness.Log(id, log);
            if (!logok)
            {
                TempData["Message"] = "记录日志失败";
                return View("Delete", id);
            }

            TempData["Message"] = "删除成功";
            return RedirectToAction("List", "Department", new { area = "Admin" });
        }

        /// <summary>
        /// 部门归档
        /// </summary>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root")]
        [HttpGet]
        public ActionResult Archive()
        {
            return View();
        }

        /// <summary>
        /// 归档任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root")]
        [HttpPost]
        public ActionResult Archive(ArchiveModel model)
        {
            if (ModelState.IsValid)
            {
                var user = GetUser();
                Log log = new Log
                {
                    Title = "归档部门",
                    Content = model.ArchiveContent,
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    RelateTime = model.ArchiveDate,
                    Type = (int)LogType.DepartmentArchive
                };

                bool result = this.departmentBusiness.Archive(log);

                if (!result)
                {
                    ModelState.AddModelError("", "归档失败");
                }
                else
                {
                    TempData["Message"] = "归档成功";
                    return RedirectToAction("ArchiveList");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 归档列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ArchiveList()
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            var data = logBusiness.GetList((int)LogType.DepartmentArchive);
            return View(data);
        }
        #endregion //Action
    }
}
