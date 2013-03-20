using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 楼层控制器
    /// </summary>
    public class FloorController : Controller
    {
        #region Action
        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">所属楼宇</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(int buildingId)
        {
            return View();
        }
        #endregion //Action
    }
}
