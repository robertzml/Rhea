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

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 入住记录控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
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
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(ResideRecord model)
        {
            if (ModelState.IsValid)
            {
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
        #endregion //Action
    }
}