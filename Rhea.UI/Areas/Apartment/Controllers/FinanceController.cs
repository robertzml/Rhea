using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Apartment;
using Rhea.UI.Areas.Apartment.Models;
using Rhea.UI.Filters;
using Rhea.UI.Services;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 收费管理控制器
    /// </summary>
    [Privilege(Require = "ApartmentFinance")]
    public class FinanceController : Controller
    {
        #region Action
        /// <summary>
        /// 房租计算
        /// </summary>
        /// <returns></returns>
        public ActionResult Rent()
        {
            return View();
        }
        #endregion //Action
    }
}