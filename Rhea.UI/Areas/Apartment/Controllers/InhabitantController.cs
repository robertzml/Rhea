using Rhea.Business.Apartment;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Apartment;
using Rhea.UI.Filters;
using Rhea.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 住户控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Apartment,Leader")]
    public class InhabitantController : Controller
    {
        #region Field
        /// <summary>
        /// 住户业务对象
        /// </summary>
        private InhabitantBusiness inhabitantBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (inhabitantBusiness == null)
            {
                inhabitantBusiness = new InhabitantBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 住户主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 住户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.inhabitantBusiness.GetByMoveOut(false);
            return View(data);
        }

        /// <summary>
        /// 已搬出住户
        /// </summary>
        /// <returns></returns>
        public ActionResult ListMoveOut()
        {
            var data = this.inhabitantBusiness.GetByMoveOut(true);
            return View(data);
        }


        /// <summary>
        /// 住户信息
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.inhabitantBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 住户编辑
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var data = this.inhabitantBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 住户编辑
        /// </summary>
        /// <param name="model">住户对象</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Inhabitant model)
        {
            if (ModelState.IsValid)
            {
                //edit
                ErrorCode result = this.inhabitantBusiness.Update(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑住户失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "编辑青教住户",
                    Time = DateTime.Now,
                    Type = (int)LogType.InhabitantEdit,
                    Content = string.Format("编辑青教住户， ID:{0}, 姓名:{1}, 部门:{2}。", model._id, model.Name, model.DepartmentName),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.inhabitantBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                return RedirectToAction("Details", new { id = model._id });
            }

            return View(model);
        }

        /// <summary>
        /// 住户摘要
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public ActionResult Summary(string id)
        {
            var data = this.inhabitantBusiness.Get(id);
            return View(data);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 获取所有住户列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <remarks>
        /// 根据姓名搜索
        /// </remarks>
        /// <returns></returns>
        public JsonResult GetList(string name)
        {
            var data = this.inhabitantBusiness.Get();
            data = data.Where(r => r.Name.Contains(name));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有在住住户列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <remarks>
        /// 根据姓名搜索
        /// </remarks>
        /// <returns></returns>
        public JsonResult GetCurrentList(string name)
        {
            var data = this.inhabitantBusiness.GetByMoveOut(false);
            data = data.Where(r => r.Name.Contains(name));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取住户
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public JsonResult Get(string id)
        {
            var data = this.inhabitantBusiness.Get(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取当前居住房间
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public JsonResult GetCurrentRooms(string id)
        {
            var data = this.inhabitantBusiness.GetCurrentRooms(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}