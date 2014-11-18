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
using Rhea.UI.Filters;
using Rhea.UI.Services;
using Rhea.UI.Areas.Apartment.Models;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 入住记录控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Apartment,Leader")]
    public class ResideRecordController : Controller
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
        /// 居住记录信息
        /// </summary>
        /// <param name="id">居住记录ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.recordBusiness.Get(id);
            if (data.Files != null && data.Files.Length != 0)
            {
                for (int i = 0; i < data.Files.Length; i++)
                {
                    data.Files[i] = RheaConstant.ApartmentRecord + data.Files[i];
                }
            }
            return View(data);
        }

        /// <summary>
        /// 居住记录摘要
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public ActionResult Summary(string inhabitantId, int roomId)
        {
            var records = this.recordBusiness.GetByInhabitant(inhabitantId);

            var data = records.Where(r => r.RoomId == roomId).OrderByDescending(r => r.RegisterTime).FirstOrDefault();
            return View(data);
        }

        /// <summary>
        /// 按房间显示
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public ActionResult ListByRoom(int roomId)
        {
            var data = this.recordBusiness.GetByRoom(roomId).OrderByDescending(r => r.RegisterTime).ToList();
            return View(data);
        }

        /// <summary>
        /// 按住户显示
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <returns></returns>
        public ActionResult ListByInhabitant(string inhabitantId)
        {
            var data = this.recordBusiness.GetByInhabitant(inhabitantId).OrderByDescending(r => r.RegisterTime).ToList();
            return View(data);
        }

        /// <summary>
        /// 编辑居住记录
        /// </summary>
        /// <param name="id">居住记录ID</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var data = this.recordBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 编辑居住记录
        /// </summary>
        /// <param name="model">居住记录对象</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ResideRecord model)
        {
            if (ModelState.IsValid)
            {
                ResideRecord old = this.recordBusiness.Get(model._id);
                model.Files = old.Files;
                model.RentHistory = old.RentHistory;
                model.InhabitantDepartmentId = Convert.ToInt32(Request.Form["DepartmentId"]);

                ErrorCode result = this.recordBusiness.Update(model);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "编辑居住记录失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "编辑居住记录",
                    Time = DateTime.Now,
                    Type = (int)LogType.ResideRecordEdit,
                    Content = string.Format("编辑居住记录， ID:{0}, 住户姓名:{1}, 部门:{2}, 房间ID:{3}, 房间名称:{4}。",
                        model._id, model.InhabitantName, model.InhabitantDepartment, model.RoomId, model.GetApartmentRoom().Name),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.recordBusiness.Log(model._id, log);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                return RedirectToAction("Details", new { id = model._id });
            }

            return View(model);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="id">居住记录ID</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [HttpGet]
        public ActionResult UploadFiles(string id)
        {
            var data = this.recordBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="model">居住记录模型</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UploadFiles(ResideRecord model)
        {
            if (ModelState.IsValid)
            {
                string files = Request.Form["recordFile"];

                ResideRecord record = this.recordBusiness.Get(model._id);
                record.Files = files.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                ErrorCode result = this.recordBusiness.Edit(record);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "保存居住记录失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "上传居住记录附件",
                    Time = DateTime.Now,
                    Type = (int)LogType.ResideRecordUploadFile,
                    Content = string.Format("上传居住记录附件， ID:{0}, 住户姓名:{1}, 部门:{2}, 房间ID:{3}, 房间名称:{4}, 附件:{5}。",
                        record._id, record.InhabitantName, record.InhabitantDepartment, record.RoomId, record.GetApartmentRoom().Name, string.Join(",", record.Files)),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.recordBusiness.Log(record._id, log);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                return RedirectToAction("Details", new { id = record._id });
            }

            return View(model);
        }

        /// <summary>
        /// 变更房租
        /// </summary>
        /// <param name="id">居住记录ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangeRent(string id)
        {
            ChangeRentModel data = new ChangeRentModel();
            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();            

            var record = this.recordBusiness.Get(id);

            data.RecordId = record._id;
            data.RoomId = record.RoomId;
            data.RoomName = roomBusiness.Get(record.RoomId).Name;
            data.InhabitantId = record.InhabitantId;
            data.InhabitantName = record.InhabitantName;
            data.LastRent = record.Rent;
            data.StartDate = DateTime.Now;            

            return View(data);
        }

        /// <summary>
        /// 变更房租
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangeRent(ChangeRentModel model)
        {
            if (ModelState.IsValid)
            {
                var record = this.recordBusiness.Get(model.RecordId);

                DateTime lastStart;
                if (record.RentHistory.Count == 0)
                    lastStart = Convert.ToDateTime(record.EnterDate);
                else
                    lastStart = record.RentHistory.Last().EndDate;

                RentHistory rentHistory = new RentHistory
                {
                    Rent = model.LastRent,
                    StartDate = lastStart,
                    EndDate = model.StartDate,
                    Remark = model.Remark
                };

                record.RentHistory.Add(rentHistory);
                record.Rent = model.CurrentRent;

                ErrorCode result = this.recordBusiness.Edit(record);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "变更房租失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "变更房租",
                    Time = DateTime.Now,
                    Type = (int)LogType.ChangeRent,
                    Content = string.Format("变更房租， 居住记录ID:{0}, 住户ID:{1}, 住户姓名:{1}, 房间ID:{2}, 房间名称:{3}, 原房租:{4}, 新房租:{5}，开始日期:{6}。",
                        record._id, model.InhabitantId, model.InhabitantName, model.RoomId, model.RoomName, model.LastRent, model.CurrentRent, model.StartDate),
                    UserId = user._id,
                    UserName = user.Name
                };

                result = this.recordBusiness.Log(record._id, log);
                if (result != ErrorCode.Success)
                {
                    ModelState.AddModelError("", "记录日志失败");
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                return RedirectToAction("Details", new { id = record._id });
            }

            return View(model);
        }
        #endregion //Action
    }
}