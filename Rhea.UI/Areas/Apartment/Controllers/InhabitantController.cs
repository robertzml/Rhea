using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Rhea.Business.Apartment;
using Rhea.Model.Apartment;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 住户控制器
    /// </summary>
    public class InhabitantController : Controller
    {
        #region Field
        /// <summary>
        /// 住户业务对象
        /// </summary>
        private InhabitantBusiness inhabitantBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (inhabitantBusiness == null)
            {
                inhabitantBusiness = new InhabitantBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 住户主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 住户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.inhabitantBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 住户信息
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            var data = this.inhabitantBusiness.Get(id);
            return View(data);
        }
        #endregion //Action
    }
}