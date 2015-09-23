using Rhea.Business.Apartment;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Apartment;
using Rhea.UI.Areas.Admin.Models;
using Rhea.UI.Filters;
using Rhea.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 青教管理控制器
    /// </summary>
    [Privilege(Require = "ApartmentAdmin")]
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

            switch ((LogType)data.Type)
            {
                case LogType.ApartmentCheckIn:
                    var checkIn = business.GetCheckInTransaction(id);
                    return View("CheckInDetails", checkIn);

                case LogType.ApartmentCheckIn2:
                    var checkIn2 = business.GetCheckIn2Transaction(id);
                    return View("CheckIn2Details", checkIn2);

                case LogType.ApartmentCheckOut:
                    var checkOut = business.GetCheckOutTransaction(id);
                    return View("CheckOutDetails", checkOut);

                case LogType.ApartmentExtend:
                    var extend = business.GetExtendTransaction(id);
                    return View("ExtendDetails", extend);

                case LogType.ApartmentExchange:
                    var exchange = business.GetExchangeTransaction(id);
                    return View("ExchangeDetails", exchange);

                case LogType.ApartmentSpecialExchange:
                    var special = business.GetSpecialExchangeTransaction(id);
                    return View("SpecialExchangeDetails", special);

                case LogType.ApartmentRegister:
                    var register = business.GetRegisterTransaction(id);
                    return View("RegisterDetails", register);
            }

            return View();
        }

        /// <summary>
        /// 特殊换房业务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SpecialExchange()
        {
            return View();
        }

        /// <summary>
        /// 特殊换房业务
        /// </summary>
        /// <param name="model">特殊换房对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SpecialExchange(SpecialExchangeModel model)
        {
            if (ModelState.IsValid)
            {
                User user = PageService.GetCurrentUser(User.Identity.Name);
                TransactionBusiness business = new TransactionBusiness();
                ErrorCode result = business.SpecialExchange(model.InhabitantId, model.OldRoomId, model.NewRoomId, model.Remark, user);

                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "特殊换房办理失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "业务办理成功";
                return RedirectToAction("TransactionList");
            }

            ModelState.AddModelError("", "输入有误，请重新输入。");
            return View(model);
        }
        #endregion //Action
    }
}