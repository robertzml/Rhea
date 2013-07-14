using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;
using Rhea.Model.Account;
using Rhea.UI.Models;
using Rhea.UI.Services;

namespace Rhea.UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Field
        /// <summary>
        /// 认证服务
        /// </summary>
        private IFormsAuthenticationService formsService;

        /// <summary>
        /// 账户服务
        /// </summary>
        private IAccountBusiness accountBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (formsService == null)
            {
                formsService = new FormsAuthenticationService();
            }
            if (accountBusiness == null)
            {
                accountBusiness = new MongoAccountBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// POST: 用户登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                formsService.SignOut();
                HttpContext.Session.Clear();

                UserProfile user = accountBusiness.Login(model.UserName, model.Password);

                if (user != null)
                {
                    HttpCookie cookie = formsService.SignIn(user.UserName, user.ManagerGroupName + "," + user.UserGroupName, true);
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "用户名密码错误");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            formsService.SignOut();
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Setting()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "两次输入密码不一致");
                    return View(model);
                }

                string userName = User.Identity.Name;

                bool result = this.accountBusiness.ValidatePassword(userName, model.OldPassword);
                if (!result)
                {
                    ModelState.AddModelError("", "原密码错误");
                    return View(model);
                }

                result = this.accountBusiness.ChangePassword(userName, model.OldPassword, model.NewPassword);
                if (!result)
                {
                    ModelState.AddModelError("", "修改密码出错");
                    return View(model);
                }

                return RedirectToAction("ShowMessage", "Home", new { area = "", msg = "修改密码成功" });
            }

            return View(model);
        }
        #endregion //Action
    }
}
