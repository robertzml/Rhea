using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rhea.Business;
using Rhea.Data.Entities;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 楼宇控制器
    /// </summary>
    public class BuildingController : Controller
    {
        #region Field
        /// <summary>
        /// 楼宇业务
        /// </summary>
        private IBuildingService buildingService;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingService == null)
            {
                buildingService = new MongoBuildingService();
            }

            base.Initialize(requestContext);
        } 
        #endregion //Function

        #region Action
        /// <summary>
        /// 楼宇主页
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 楼宇详细
        /// GET: /Estate/Building/Details/7
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {           
            Building data = this.buildingService.Get(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;  

            return View(data);
        }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        public ActionResult List(int buildingGroupId)
        {            
            var data = this.buildingService.GetListByBuildingGroup(buildingGroupId).OrderBy(r => r.Id);
            return View(data);
        }

        /// <summary>
        /// 楼宇添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 楼宇添加
        /// </summary>
        /// <param name="model">楼宇模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Building model)
        {
            if (ModelState.IsValid)
            {               
                int result = this.buildingService.Create(model);

                if (result != 0)
                {
                    return RedirectToAction("Index", "Building", new { area = "Estate", id = result });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 楼宇编辑
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {           
            Building data = this.buildingService.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Building model)
        {
            if (ModelState.IsValid)
            {                
                bool result = this.buildingService.Edit(model);

                if (result)
                {
                    return RedirectToAction("Index", "Building", new { area = "Estate", id = model.Id });
                }
                else
                {
                    ModelState.AddModelError("", "保存失败");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 楼宇删除
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Building data = this.buildingService.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼宇删除
        /// POST: /Estate/Builidng/Delete/7
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {            
            bool result = this.buildingService.Delete(id);

            if (result)
            {
                return RedirectToAction("List", "BuildingGroup", new { area = "Estate" });
            }
            else
                return View("Delete", id);
        }
        #endregion //Action
    }
}
