using Rhea.Business;
using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 字典控制器
    /// </summary>
    public class DictionaryController : Controller
    {
        #region Field
        /// <summary>
        /// 字典业务
        /// </summary>
        private DictionaryBusiness dictionaryBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (dictionaryBusiness == null)
            {
                dictionaryBusiness = new DictionaryBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        // GET: Admin/Dictionary
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 字典集列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.dictionaryBusiness.Get();
            return View(data);
        }
        #endregion //Action
    }
}