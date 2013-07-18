using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Model.Account;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class ManagerGroupController : Controller
    {
        #region Field
        /// <summary>
        /// 管理组业务
        /// </summary>
        private IManagerGroupBusiness managerGroupBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (managerGroupBusiness == null)
            {
                managerGroupBusiness = new MongoManagerGroupBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 管理组列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.managerGroupBusiness.GetList();
            return View(data);
        }

        /// <summary>
        /// 管理组信息
        /// </summary>
        /// <param name="id">管理组ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.managerGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 管理组编辑
        /// </summary>
        /// <param name="id">管理组ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = this.managerGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 管理组编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ManagerGroup model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.managerGroupBusiness.Edit(model);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "ManagerGroup", new { area = "Admin", id = model.Id });
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
