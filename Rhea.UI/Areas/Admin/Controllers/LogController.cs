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
            var data = this.logBusiness.Get().OrderByDescending(r => r.Time);
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
        #endregion //Action

        #region Json
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