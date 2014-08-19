using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 校区控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class CampusController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private CampusBusiness campusBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (campusBusiness == null)
            {
                campusBusiness = new CampusBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        // GET: Admin/Campus
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 校区列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.campusBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 校区信息
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.campusBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 校区编辑
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var data = this.campusBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 校区编辑
        /// </summary>
        /// <param name="model">校区对象</param>
        /// <returns></returns>
        public ActionResult Edit(Campus model)
        {
            if (ModelState.IsValid)
            {
                model.Status = 0;
                ErrorCode result = this.campusBusiness.Update(model);

                if (result == ErrorCode.Success)
                {
                    TempData["Message"] = "编辑校区成功";
                    return RedirectToAction("Details", new { controller = "Campus", id = model.CampusId });
                }
                else
                {
                    TempData["Message"] = "编辑校区失败";
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }
        #endregion //Action
    }
}