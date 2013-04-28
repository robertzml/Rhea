using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
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
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
        #endregion //Action
    }
}
