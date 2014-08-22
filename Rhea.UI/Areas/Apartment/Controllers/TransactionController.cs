using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Apartment;
using Rhea.UI.Areas.Apartment.Models;
using Rhea.UI.Filters;

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
                Inhabitant inhabitant;
                ErrorCode result;
                InhabitantBusiness inhabitantBusiness = new InhabitantBusiness();
                if (string.IsNullOrEmpty(model.OldInhabitant))
                {
                    //添加住户记录
                    inhabitant = new Inhabitant();
                    inhabitant.JobNumber = model.JobNumber;
                    inhabitant.Name = model.Name;
                    inhabitant.Gender = model.Gender;
                    inhabitant.Type = model.Type;
                    inhabitant.DepartmentName = model.DepartmentName;
                    inhabitant.Duty = model.Duty;
                    inhabitant.Telephone = model.Telephone;
                    inhabitant.IdentityCard = model.IdentityCard;
                    inhabitant.AccumulatedFundsDate = model.AccumulatedFundsDate;
                    inhabitant.Marriage = model.Marriage;
                    inhabitant.LiHuEnterDate = model.LiHuEnterDate;
                    inhabitant.Remark = model.InhabitantRemark;
                    inhabitant.Status = 0;

                    result = inhabitantBusiness.Create(inhabitant);
                }
                else
                {
                    inhabitant = inhabitantBusiness.Get(model.OldInhabitant);
                    inhabitant.JobNumber = model.JobNumber;
                    inhabitant.Name = model.Name;
                    inhabitant.Gender = model.Gender;
                    inhabitant.Type = model.Type;
                    inhabitant.DepartmentName = model.DepartmentName;
                    inhabitant.Duty = model.Duty;
                    inhabitant.Telephone = model.Telephone;
                    inhabitant.IdentityCard = model.IdentityCard;
                    inhabitant.AccumulatedFundsDate = model.AccumulatedFundsDate;
                    inhabitant.Marriage = model.Marriage;
                    inhabitant.LiHuEnterDate = model.LiHuEnterDate;
                    inhabitant.Remark = model.InhabitantRemark;
                    inhabitant.Status = 0;

                    result = inhabitantBusiness.Update(inhabitant);
                }

                if (result != ErrorCode.Success)
                {
                    ViewBag.Message = "用户添加出错：" + result.DisplayName();
                    return View("CheckInResult");
                }

                //添加居住记录
                ResideRecord record = new ResideRecord();
                record.RoomId = model.RoomId;
                record.InhabitantId = inhabitant._id;
                record.InhabitantName = inhabitant.Name;
                record.InhabitantDepartment = inhabitant.DepartmentName;
                record.ResideType = (int)ResideType.Normal;
                record.Rent = model.Rent;
                record.EnterDate = model.EnterDate;
                record.ExpireDate = model.ExpireDate;
                record.TermLimit = model.TermLimit;
                record.Remark = model.RecordRemark;
                record.Status = 0;

                ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
                result = recordBusiness.Create(record);
                if (result != ErrorCode.Success)
                {
                    ViewBag.Message = "居住记录添加出错：" + result.DisplayName();
                    return View("CheckInResult");
                }

                //更新房间状态
                ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
                var room = roomBusiness.Get(model.RoomId);
                room.ResideType = (int)ResideType.Normal;

                result = roomBusiness.Update(room);
                if (result != ErrorCode.Success)
                {
                    ViewBag.Message = "房间状态更新出错：" + result.DisplayName();
                    return View("CheckInResult");
                }

                ViewBag.Message = "入住办理成功";
                return View("CheckInResult");
            }

            return View(model);
        }
        #endregion //Action
    }
}