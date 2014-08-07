using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.UI.Filters;
using Rhea.UI.Models;
using Rhea.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Controllers
{
    [EnhancedAuthorize]
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
        private UserBusiness userBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (formsService == null)
            {
                formsService = new FormsAuthenticationService();
            }
            if (userBusiness == null)
            {
                userBusiness = new UserBusiness();
            }
            base.Initialize(requestContext);
        }

        /// <summary>
        /// 计算Unix格式的时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetUnixTimeStamp(DateTime dt)
        {
            DateTime unixStartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan timeSpan = dt.Subtract(unixStartTime);
            string timeStamp = timeSpan.Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 7);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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

                ErrorCode result = this.userBusiness.Login(model.UserName, model.Password);
                if (result == ErrorCode.Success)
                {
                    User user = this.userBusiness.GetByUserName(model.UserName);
                    HttpCookie cookie = formsService.SignIn(user.UserName, user.UserGroupName(), model.RememberMe);
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.DisplayName());
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
        /// 个人主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var user = this.userBusiness.GetByUserName(User.Identity.Name);
            return View(user);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword()
        {
            string oldPassword = Request.Form["oldPassword"];
            string newPassword = Request.Form["newPassword"];
            string confirmPassword = Request.Form["confirmPassword"];

            var user = this.userBusiness.GetByUserName(User.Identity.Name);

            if (newPassword != confirmPassword)
            {
                ViewBag.Message = "修改密码失败";
                return View();
            }

            ErrorCode result = this.userBusiness.ChangePassword(user, oldPassword, newPassword);
            if (result == ErrorCode.Success)
                ViewBag.Message = "修改密码成功";
            else
                ViewBag.Message = "修改密码失败，" + result.DisplayName() + ".";

            return View();
        }
        #endregion //Action
    }
}