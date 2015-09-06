using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Apartment;
using Rhea.Model.Estate;
using Rhea.UI.Filters;
using Rhea.UI.Areas.Apartment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 青教公寓主页控制器
    /// </summary>    
    [Privilege(Require = "ApartmentManage")]
    public class HomeController : Controller
    {
        #region Field
        #endregion //Field

        #region Action
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 青教公寓建筑菜单
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult BuildingMenu()
        {
            BuildingBusiness business = new BuildingBusiness();
            var data = business.GetChildBlocks(RheaConstant.ApartmentBuildingId);
            return View(data);
        }

        /// <summary>
        /// 基本数据
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseInfo()
        {
            BaseInfoModel data = new BaseInfoModel();           

            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            var rooms = roomBusiness.Get();

            data.RoomCount = rooms.Count();
            data.TwoBedroomsCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[0]);
            data.BigOneBedroomsCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[1]);
            data.OneBedroomsCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[2]);
            data.BigSimpleRoomCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[3]);
            data.SimpleRoomCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[4]);

            data.EmptyRoomCount = rooms.Count(r => r.ResideType == 0);
            data.EmptyTwoBedroomsCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[0] && r.ResideType == 0);
            data.EmptyBigOneBedroomsCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[1] && r.ResideType == 0);
            data.EmptyOneBedroomsCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[2] && r.ResideType == 0);
            data.EmptyBigSimpleRoomCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[3] && r.ResideType == 0);
            data.EmptySimpleRoomCount = rooms.Count(r => r.HouseType == RheaConstant.ApartmentHouseTypes[4] && r.ResideType == 0);

            return View(data);
        }

        /// <summary>
        /// 获取快要到期的居住记录
        /// </summary>
        /// <param name="day">到期天数</param>
        /// <returns></returns>
        public ActionResult ExpireInDays(int day)
        {
            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var data = recordBusiness.GetExpireInDays(day);
            return View(data);
        }

        /// <summary>
        /// 获取超期居住记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpireRecords()
        {
            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var data = recordBusiness.GetExpired();
            return View(data);
        }

        /// <summary>
        /// 最新入住办理
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public ActionResult LastCheckIn(int count)
        {
            TransactionBusiness business = new TransactionBusiness();
            var data = business.GetCheckInTransaction().Take(count);
            return View(data);
        }

        /// <summary>
        /// 最新退房办理
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public ActionResult LastCheckOut(int count)
        {
            TransactionBusiness business = new TransactionBusiness();
            var data = business.GetCheckOutTransaction().Take(count);
            return View(data);
        }

        /// <summary>
        /// 最新换房办理
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public ActionResult LastExchange(int count)
        {
            TransactionBusiness business = new TransactionBusiness();
            var data = business.GetExchangeTransaction().Take(count);
            return View(data);
        }

        /// <summary>
        /// 检测更新居住记录和住户状态
        /// </summary>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [HttpPost]
        public string CheckStatus()
        {
            TransactionBusiness business = new TransactionBusiness();
            ErrorCode result = business.UpdateStatus();
            return result.DisplayName();
        }
        #endregion //Action
    }
}