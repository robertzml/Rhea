using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Data.Entities;
using Rhea.UI.Models;
using Rhea.UI.Services;

namespace Rhea.UI.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
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
        private IAccountService accountService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (formsService == null) 
            { 
                formsService = new FormsAuthenticationService(); 
            }
            if (accountService == null)
            {
                accountService = new MongoAccountService();
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
        /// POST: /Account/Login
        /// </summary>
        /// <param name="model"></param>
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

                UserProfile user = accountService.Login(model.UserName, model.Password);

                if (user != null)
                {
                    HttpCookie cookie = formsService.SignIn(user.UserName, user.ManagerType + "," + user.UserType, true);
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
        /// 账户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            string userName = User.Identity.Name;         
            UserProfile user = accountService.Get(userName);

            return View(user);
        }
        #endregion //Action
    }
}
