﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Business.Personnel;
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
    /// 业务办理控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
    public class TransactionController : Controller
    {
        #region Action
        // GET: Apartment/Transaction
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 入住办理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckIn()
        {
            return View();
        }

        /// <summary>
        /// 入住办理
        /// </summary>
        /// <param name="model">入住模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CheckIn(CheckInModel model)
        {
            if (ModelState.IsValid)
            {
                Inhabitant inhabitant = new Inhabitant();
                inhabitant._id = model.OldInhabitant;
                inhabitant.JobNumber = model.JobNumber;
                inhabitant.Name = model.Name;
                inhabitant.Gender = model.Gender;
                inhabitant.Type = model.Type;
                inhabitant.DepartmentName = model.DepartmentName;
                inhabitant.Duty = model.Duty;
                inhabitant.Telephone = model.Telephone;
                inhabitant.IdentityCard = model.IdentityCard;
                inhabitant.Education = model.Education;
                inhabitant.AccumulatedFundsDate = model.AccumulatedFundsDate;
                inhabitant.IsCouple = model.IsCouple;
                inhabitant.Marriage = model.Marriage;
                inhabitant.LiHuEnterDate = model.LiHuEnterDate;
                inhabitant.Remark = model.InhabitantRemark;

                ResideRecord record = new ResideRecord();
                record.RoomId = model.RoomId;
                record.ResideType = (int)ResideType.Normal;
                record.Rent = model.Rent;
                record.EnterDate = model.EnterDate;
                record.ExpireDate = model.ExpireDate;
                record.TermLimit = model.TermLimit;
                record.MonthCount = model.MonthCount;
                record.ReceiptNumber = model.ReceiptNumber;
                record.Remark = model.RecordRemark;
                record.Status = 0;

                User user = PageService.GetCurrentUser(User.Identity.Name);
                TransactionBusiness business = new TransactionBusiness();
                ErrorCode result = business.CheckIn(inhabitant, record, model.RoomId, user);

                if (result == ErrorCode.Success)
                {
                    ViewBag.Message = "入住办理成功。";
                    return View("TransactionResult");
                }
                else
                {
                    ViewBag.Message = "入住办理失败。" + business.ErrorMessage;
                    return View("TransactionResult");
                }
            }

            ViewBag.Message = "输入有误，请重新输入。";
            return View(model);
        }

        /// <summary>
        /// 退房办理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckOut()
        {
            return View();
        }

        /// <summary>
        /// 退房办理
        /// </summary>
        /// <param name="model">退房模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CheckOut(CheckOutModel model)
        {
            if (ModelState.IsValid)
            {
                User user = PageService.GetCurrentUser(User.Identity.Name);
                TransactionBusiness business = new TransactionBusiness();
                ErrorCode result = business.CheckOut(model.InhabitantId, model.RoomId, model.LeaveDate, model.Remark, user);

                if (result == ErrorCode.Success)
                {
                    ViewBag.Message = "退房办理成功。";
                    return View("TransactionResult");
                }
                else
                {
                    ViewBag.Message = "退房办理失败。" + business.ErrorMessage;
                    return View("TransactionResult");
                }
            }

            ViewBag.Message = "输入有误，请重新输入。";
            return View(model);
        }

        /// <summary>
        /// 延期申请
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Extend()
        {
            return View();
        }

        /// <summary>
        /// 延期申请
        /// </summary>
        /// <param name="model">延期模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Extend(ExtendModel model)
        {
            if (ModelState.IsValid)
            {
                ResideRecord record = new ResideRecord();
                record.RoomId = model.RoomId;
                record.ResideType = (int)ResideType.Normal;
                record.Rent = model.Rent;
                record.EnterDate = model.EnterDate;
                record.ExpireDate = model.ExpireDate;
                record.MonthCount = model.MonthCount;
                record.Remark = model.Remark;
                record.Files = model.RecordFile.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                User user = PageService.GetCurrentUser(User.Identity.Name);
                TransactionBusiness business = new TransactionBusiness();
                ErrorCode result = business.Extend(model.InhabitantId, record, model.RoomId, user);

                if (result == ErrorCode.Success)
                {
                    ViewBag.Message = "延期办理成功。";
                    return View("TransactionResult");
                }
                else
                {
                    ViewBag.Message = "延期办理失败。" + business.ErrorMessage;
                    return View("TransactionResult");
                }
            }

            ViewBag.Message = "输入有误，请重新输入。";
            return View(model);
        }

        /// <summary>
        /// 换房办理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Exchange()
        {
            return View();
        }

        /// <summary>
        /// 换房办理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Exchange(ExchangeModel model)
        {
            if (ModelState.IsValid)
            {
                ResideRecord record = new ResideRecord();
                record.ResideType = (int)ResideType.Normal;
                record.Rent = model.Rent;
                record.EnterDate = model.EnterDate;
                record.ExpireDate = model.ExpireDate;
                record.MonthCount = model.MonthCount;
                record.Remark = model.Remark;
                if (!string.IsNullOrEmpty(model.RecordFile))
                    record.Files = model.RecordFile.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                User user = PageService.GetCurrentUser(User.Identity.Name);
                TransactionBusiness business = new TransactionBusiness();
                ErrorCode result = business.Exchange(model.InhabitantId, model.RoomId, model.NewRoomId, record, user);

                if (result == ErrorCode.Success)
                {
                    ViewBag.Message = "换房办理成功。";
                    return View("TransactionResult");
                }
                else
                {
                    ViewBag.Message = "换房办理失败。" + business.ErrorMessage;
                    return View("TransactionResult");
                }
            }

            ViewBag.Message = "输入有误，请重新输入。";
            return View(model);
        }

        /// <summary>
        /// 住户登记
        /// </summary>
        /// <remarks>
        /// 人事处登记新入职教职工
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 住户登记
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Inhabitant inhabitant = new Inhabitant();
                inhabitant.JobNumber = model.JobNumber;
                inhabitant.Name = model.Name;
                inhabitant.Gender = model.Gender;
                inhabitant.DepartmentId = model.DepartmentId;
                inhabitant.DepartmentName = DepartmentBusiness.GetName(model.DepartmentId);
                inhabitant.Duty = model.Duty;
                inhabitant.Telephone = model.Telephone;
                inhabitant.IdentityCard = model.IdentityCard;
                inhabitant.Education = model.Education;
                inhabitant.IsCouple = model.IsCouple;
                inhabitant.Marriage = model.Marriage;
                inhabitant.AccumulatedFundsDate = model.AccumulatedFundsDate;
                inhabitant.LiHuEnterDate = model.LiHuEnterDate;
                inhabitant.Remark = model.Remark;
                inhabitant.Type = 1;    //教职工

                User user = PageService.GetCurrentUser(User.Identity.Name);
                TransactionBusiness business = new TransactionBusiness();
                ErrorCode result = business.Register(inhabitant, user);

                if (result == ErrorCode.Success)
                {
                    ViewBag.Message = "新教职工登记成功。";
                    return View("TransactionResult");
                }
                else
                {
                    ViewBag.Message = "新教职工登记失败。" + business.ErrorMessage;
                    return View("TransactionResult");
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}