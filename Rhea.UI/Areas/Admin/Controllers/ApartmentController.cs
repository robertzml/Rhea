using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.UI.Filters;
using Rhea.Business.Apartment;
using Rhea.Model.Apartment;
using Rhea.Model;
using Rhea.Common;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 青教管理控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class ApartmentController : Controller
    {
        #region Action
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 业务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TransactionList()
        {
            TransactionBusiness business = new TransactionBusiness();
            var data = business.GetTransaction();

            return View(data);
        }

        /// <summary>
        /// 业务详细
        /// </summary>
        /// <param name="id">业务ID</param>
        /// <returns></returns>
        public ActionResult TransactionDetails(string id)
        {
            TransactionBusiness business = new TransactionBusiness();
            var data = business.GetTransaction(id);

            switch((LogType)data.Type)
            {
                case LogType.ApartmentCheckIn:
                    var checkIn = business.GetCheckInTransaction(id);
                    return View("CheckInDetails", checkIn);
                    
                case LogType.ApartmentCheckOut:
                    var checkOut = business.GetCheckOutTransaction(id);
                    return View("CheckOutDetails", checkOut);

                case LogType.ApartmentExtend:
                    var extend = business.GetExtendTransaction(id);
                    return View("ExtendDetails", extend);
            }

            return View();
        }
        #endregion //Action
    }
}