using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Admin.Controllers
{
    public class DictionaryController : Controller
    {
        #region Action
        /// <summary>
        /// 字典列表
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public ActionResult List(string db)
        {
            return View();
        }
        #endregion //Action
    }
}
