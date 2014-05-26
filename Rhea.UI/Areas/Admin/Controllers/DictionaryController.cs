using Rhea.Business;
using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// 字典集信息
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        public ActionResult Details(string name)
        {
            var data = this.dictionaryBusiness.Get(name);
            return View(data);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="model">字典模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Dictionary model)
        {
            if (ModelState.IsValid)
            {
                model.Property = Regex.Split(Request.Form["Property"].TrimEnd(), "\r\n");
                model.IsCombined = false;

                bool result = this.dictionaryBusiness.Create(model);
                if (result)
                {
                    TempData["Message"] = "添加字典成功";
                    return RedirectToAction("List", "Dictionary");
                }
                else
                    TempData["Message"] = "添加字典失败";
            }
            return View();
        }
        #endregion //Action
    }
}