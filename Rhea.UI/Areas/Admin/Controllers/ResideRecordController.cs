using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Apartment;
using Rhea.UI.Filters;
using Rhea.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 居住记录控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class ResideRecordController : Controller
    {
        #region Field
        /// <summary>
        /// 居住记录业务
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
        /// 居住记录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.recordBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 居住记录详细
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
        /// 居住记录编辑
        /// </summary>
        /// <param name="id">居住记录ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var data = this.recordBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 居住记录编辑
        /// </summary>
        /// <param name="model">居住记录对象</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ResideRecord model)
        {
            if (ModelState.IsValid)
            {
                ResideRecord old = this.recordBusiness.Get(model._id);
                model.Files = old.Files;

                ErrorCode result = this.recordBusiness.Update(model);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "编辑居住记录失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑居住记录",
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
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑居住记录成功";
                return RedirectToAction("Details", new { id = model._id });
            }

            return View(model);
        }
        #endregion //Action
    }
}