using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Model.Account;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{    
    public class AccountController : Controller
    {        
        #region Field       
        /// <summary>
        /// 管理业务
        /// </summary>
        private IAccountBusiness accountBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (accountBusiness == null)
            {
                accountBusiness = new MongoAccountBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root")]
        public ActionResult List()
        {           
            var data = this.accountBusiness.GetList();
            return View(data);
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="id">用户系统ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.accountBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                string result = this.accountBusiness.Create(model);

                if (!string.IsNullOrEmpty(result))
                {
                    TempData["Message"] = "添加成功";
                    return RedirectToAction("List", "Account", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="id">用户系统ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var data = this.accountBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                bool result = this.accountBusiness.Edit(model);

                if (result)
                {
                    TempData["Message"] = "编辑成功";
                    return RedirectToAction("Details", "Account", new { area = "Admin", id = model.Id });
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
