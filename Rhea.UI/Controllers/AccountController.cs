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
using Rhea.UI.Filters;

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

        /// <summary>
        /// 统一身份认证登录
        /// </summary>
        /// <param name="userId">学号或工号</param>
        public void LoginUnity(string userId)
        {
            bool result = this.accountBusiness.CreateUnity(userId);

            UserProfile user = this.accountBusiness.Login(userId);
            if (user != null)
            {
                HttpCookie cookie = formsService.SignIn(user.UserName, user.UserGroupName, false);
                Response.Cookies.Add(cookie);

                RedirectToAction("Index", "Home");
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

                UserProfile user = accountBusiness.Login(model.UserName, model.Password);

                if (user != null)
                {
                    HttpCookie cookie = formsService.SignIn(user.UserName, user.UserGroupName, model.RememberMe);
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
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Info()
        {
            return View();
        }

        /// <summary>
        /// 用户摘要
        /// </summary>
        /// <returns></returns>
        public ActionResult Summary()
        {
            UserProfile user = this.accountBusiness.GetByUserName(User.Identity.Name);
            return View(user);
        }

        /// <summary>
        /// 用户设置
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

                return RedirectToAction("ShowMessage", "Common", new { area = "", msg = "修改密码成功", title = "用户设置" });
            }

            return View(model);
        }

        /// <summary>
        /// 统一身份认证
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public bool Check()
        {
            //工号或者学号 
            string id_tag = Request.QueryString["id_tag"];

            //客户端md5散列串 
            string secret = Request.QueryString["secret"];

            //客户端时间 
            string time = Request.QueryString["timestamp"];

            //双方约定的密码  
            string pass = "该内容会另行告知";

            //获取服务器时间戳 
            string mytime = GetUnixTimeStamp(DateTime.Now);

            //如果客户端时间早于服务器时间1个小时，则加密串过期 
            if (Convert.ToInt64(mytime) - Convert.ToInt64(time) > 60 * 60)
            {
                Response.Redirect(Rhea.Business.RheaConstant.AuthUrl);
                return false;
            }

            //服务器端md5散列串="工号+双方约定的密码+客户端时间"的md5 
            string secret1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(id_tag + pass + time, "md5").ToLower();

            //如果客户端md5散列串==服务器端md5散列串，则认证通过。 
            if (secret == secret1)
                return true;
            else
            {
                Response.Redirect(Rhea.Business.RheaConstant.AuthUrl);
                return false;
            }
        }
        #endregion //Action
    }
}
