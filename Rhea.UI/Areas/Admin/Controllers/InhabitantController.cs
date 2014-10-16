using Rhea.Business;
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

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 住户管理控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class InhabitantController : Controller
    {
        #region Field
        /// <summary>
        /// 住户业务
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
        /// 住户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.inhabitantBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 住户详细
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.inhabitantBusiness.Get(id);

            DictionaryBusiness business = new DictionaryBusiness();
            var types = business.GetPairProperty("InhabitantType");
            ViewBag.Type = types[data.Type];
            return View(data);
        }

        /// <summary>
        /// 住户编辑
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
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
                    TempData["Message"] = "编辑住户失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑青教住户",
                    Time = DateTime.Now,
                    Type = (int)LogType.InhabitantEdit,
                    Content = string.Format("编辑青教住户， ID:{0}, 姓名:{1}, 部门:{2}。", model._id, model.Name, model.DepartmentName),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.inhabitantBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑住户成功";
                return RedirectToAction("Details", new { id = model._id });
            }

            return View(model);
        }
        #endregion //Action
    }
}