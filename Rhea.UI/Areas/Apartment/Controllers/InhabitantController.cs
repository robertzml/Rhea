using Rhea.Business.Apartment;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Apartment;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 住户控制器
    /// </summary>
    [EnhancedAuthorize(Roles = "Root,Administrator,Apartment")]
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

        /// <summary>
        /// 住户编辑
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var data = this.inhabitantBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 住户编辑
        /// </summary>
        /// <param name="model">住户对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Inhabitant model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.inhabitantBusiness.Update(model);
                if (result == ErrorCode.Success)
                {
                    return RedirectToAction("Details", new { id = model._id });
                }
                else
                {
                    ModelState.AddModelError("", result.DisplayName());
                }
            }

            return View(model);
        }

        /// <summary>
        /// 居住记录
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public ActionResult Record(string id)
        {
            ResideRecordBusiness business = new ResideRecordBusiness();
            var data = business.GetByInhabitant(id);
            return View(data);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 获取所有住户列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <remarks>
        /// 根据姓名搜索
        /// </remarks>
        /// <returns></returns>
        public JsonResult GetList(string name)
        {
            var data = this.inhabitantBusiness.Get();
            data = data.Where(r => r.Name.Contains(name));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取住户
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns></returns>
        public JsonResult Get(string id)
        {
            var data = this.inhabitantBusiness.Get(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}