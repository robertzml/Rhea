using Rhea.Business.Energy;
using Rhea.Model.Energy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 能源管理控制器
    /// </summary>
    public class EnergyController : Controller
    {
        #region Field
        /// <summary>
        /// 电系统业务
        /// </summary>
        private MongoElectricBusiness mongoElectricBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (mongoElectricBusiness == null)
            {
                mongoElectricBusiness = new MongoElectricBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 能源管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 绑定房间与电系统
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BindRoom()
        {
            return View();
        }

        /// <summary>
        /// 绑定房间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult BindRoom(RoomMap model)
        {
            if (ModelState.IsValid)
            {
                //create
                bool result = this.mongoElectricBusiness.BindRoom(model);
                if (!result)
                {
                    ModelState.AddModelError("", "绑定失败");
                    return View(model);
                }


                TempData["Message"] = "绑定成功";
                return RedirectToAction("BindDetails", "Energy", new { area = "Admin", roomId = model.RoomId });
            }

            return View(model);
        }

        public ActionResult BindDetails(int roomId)
        {
            return View();
        }
        #endregion //Action
    }
}
