using Rhea.Business.Account;
using Rhea.Model.Account;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Controllers
{
    [EnhancedAuthorize]
    public class HomeController : Controller
    {
        #region Action
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 菜单导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            UserBusiness userBusiness = new UserBusiness();
            var user = userBusiness.GetByUserName(User.Identity.Name);

            return View(user);
        }

        /// <summary>
        /// 手机导航菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult MobileMenu()
        {
            UserBusiness userBusiness = new UserBusiness();
            var user = userBusiness.GetByUserName(User.Identity.Name);
            return View(user);
        }
        #endregion //Action
    }
}