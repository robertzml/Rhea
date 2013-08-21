using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Model;

namespace Rhea.UI.Areas.Admin.Controllers
{
    public class EstateController : Controller
    {
        #region Action
        /// <summary>
        /// 房产管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导出任务
        /// </summary>
        /// <returns></returns>
        public ActionResult Export()
        {
            return View();
        }

        /// <summary>
        /// 归档任务
        /// </summary>
        /// <returns></returns>
        public ActionResult Archive()
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            var data = logBusiness.GetList(20);
            return View(data);
        }

        /// <summary>
        /// 日志列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LogList()
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            var data = logBusiness.GetList().OrderBy(r => r.Time);

            return View(data);
        }
        #endregion //Action
    }
}
