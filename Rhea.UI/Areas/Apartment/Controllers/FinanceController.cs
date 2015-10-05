using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
        #region Field
        /// <summary>
        /// 入住记录业务对象
        /// </summary>
        private ResideRecordBusiness recordBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (recordBusiness == null)
            {
                recordBusiness = new ResideRecordBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房租计算
        /// </summary>
        /// <returns></returns>
        public ActionResult Rent()
        {
            return View();
        }

        /// <summary>
        /// 房租计算
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RentProcess(RentRequestModel model)
        {
            if (ModelState.IsValid)
            {
                List<RentProcessModel> list = new List<RentProcessModel>();
                DictionaryBusiness business = new DictionaryBusiness();
                var types = business.GetPairProperty("InhabitantType");

                var data = this.recordBusiness.GetByResideType(ResideType.Normal).Where(r => r.LeaveDate == null || r.LeaveDate >= model.Start);

                foreach (var item in data)
                {
                    RentProcessModel m = new RentProcessModel();

                    var inhabitant = item.GetInhabitant();
                    if (inhabitant.Type != 1 && inhabitant.Type != 7)
                        continue;

                    m.ResideRecordId = item._id;
                    m.InhabitantId = item.InhabitantId;
                    m.InhabitantName = item.InhabitantName;
                    m.InhabitantNumber = inhabitant.JobNumber;
                    m.InhabitantDepartment = item.InhabitantDepartment;
                    m.InhabitantType = types[inhabitant.Type];

                    m.RoomNumber = item.GetApartmentRoom().Number;
                    m.EnterDate = item.EnterDate;
                    m.LeaveDate = item.LeaveDate;
                    m.CurrentRent = item.Rent;
                    m.Status = item.Status;

                    list.Add(m);
                }

                return View(list);
            }

            ViewBag.Error = "请选择日期";
            return View();
        }
        #endregion //Action
    }
}