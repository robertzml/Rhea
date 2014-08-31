using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Common;
using Rhea.Model;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 日志控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root")]
    public class LogController : Controller
    {
        #region Field
        /// <summary>
        /// 日志业务
        /// </summary>
        private LogBusiness logBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (logBusiness == null)
            {
                logBusiness = new LogBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.logBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 日志详细
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.logBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns></returns>
        [EnhancedAuthorize(Roles = "Root")]
        public ActionResult Delete(string id)
        {
            ErrorCode result = this.logBusiness.Delete(id);
            if (result != ErrorCode.Success)
            {
                TempData["Message"] = "删除日志失败";
                return RedirectToAction("Details", new { id = id });
            }

            TempData["Message"] = "删除日志成功";
            return RedirectToAction("List");
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 获取日志数据
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public JsonResult GetData(int draw, int start, int length)
        {
            var data = this.logBusiness.Get(start, length);
            long count = this.logBusiness.Count();

            var model = new
            {
                draw = draw,
                recordsTotal = count,
                recordsFiltered = count,
                data = data.Select(g => new { g._id, g.Title, g.Time, g.UserName, TypeName = ((LogType)g.Type).DisplayName(), g.Type })
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}