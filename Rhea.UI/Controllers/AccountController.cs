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
using System.Drawing;
using System.IO;
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
                LogBusiness logBusiness = new LogBusiness();

                ErrorCode result = this.userBusiness.Login(model.UserName, model.Password);
                if (result == ErrorCode.Success)
                {
                    User user = this.userBusiness.GetByUserName(model.UserName);
                    HttpCookie cookie = formsService.SignIn(user.UserName, user.UserGroupName(), model.RememberMe);
                    Response.Cookies.Add(cookie);

                    Log log = new Log
                    {
                        Title = "用户登录成功",
                        Time = DateTime.Now,
                        Type = (int)LogType.UserLoginSuccess,
                        Content = string.Format("用户登录成功, 用户名：{0}。", user.Name),
                        UserId = user._id,
                        UserName = user.Name
                    };
                    result = logBusiness.Create(log);
                    if (result != ErrorCode.Success)
                    {
                        TempData["Message"] = "记录日志失败";
                        ModelState.AddModelError("", result.DisplayName());
                        return View(model);
                    }

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
            if (!string.IsNullOrEmpty(user.AvatarUrl))
                user.AvatarUrl = RheaConstant.AvatarRoot + user.AvatarUrl;
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

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UploadAvatar()
        {
            HttpPostedFileBase hp = Request.Files["avatarfile"];
            if (hp == null)
            {
                ViewBag.Message = "请选择文件";
                return View("UploadAvatarResult");
            }

            var user = this.userBusiness.GetByUserName(User.Identity.Name);

            string uploadPath = Server.MapPath(RheaConstant.AvatarRoot);
            //文件名为用户ID
            string fileName = user._id + Path.GetExtension(hp.FileName);

            long contentLength = hp.ContentLength;
            //文件不能大于1M
            if (contentLength > 1024 * 1024)
            {
                ViewBag.Message = "文件大小超过限制要求.";
                return View("UploadAvatarResult");
            }

            if (!Util.CheckImageExtension(fileName))
            {
                ViewBag.Message = "请上传图片文件.";
                return View("UploadAvatarResult");
            }

            try
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);

                Bitmap image = Util.ResizeImage(hp.InputStream, 256, 256);
                string filePath = uploadPath + fileName;
                image.Save(filePath);

                Bitmap large = Util.ResizeImage(filePath, 128, 128);
                string largePath = uploadPath + name + "_128" + ext;
                large.Save(largePath);

                Bitmap medium = Util.ResizeImage(filePath, 64, 64);
                string mediumPath = uploadPath + name + "_64" + ext;
                medium.Save(mediumPath);

                Bitmap small = Util.ResizeImage(filePath, 32, 32);
                string smallPath = uploadPath + name + "_32" + ext;
                small.Save(smallPath);

                ErrorCode result = this.userBusiness.ChangeAvatar(user, fileName, name + "_128" + ext, name + "_64" + ext, name + "_32" + ext);
                if (result == ErrorCode.Success)
                {
                    ViewBag.Message = "保存头像成功";
                }
                else
                {
                    ViewBag.Message = result.DisplayName();
                }
            }
            catch
            {
                ViewBag.Message = "上传失败.";
            }

            return View("UploadAvatarResult");
        }
        #endregion //Action
    }
}