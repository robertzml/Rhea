using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Data;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [EnhancedAuthorize(Rank = 600)]
    public class EstateController : Controller
    {
        #region Function
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        private UserProfile GetUser()
        {
            IAccountBusiness accountBusiness = new MongoAccountBusiness();
            UserProfile user = accountBusiness.GetByUserName(User.Identity.Name);
            return user;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房产管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导出任务
        /// </summary>
        /// <returns></returns>
        public ActionResult Export()
        {
            return View();
        }
        #endregion //Action
    }
}
