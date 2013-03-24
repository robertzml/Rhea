using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        #region Action
        /// <summary>
        /// 楼宇详细
        /// GET: /Estate/Building/Details/7
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            EstateService service = new EstateService();
            Building data = service.GetBuilding(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImageServer + data.ImageUrl;  

            return View(data);
        }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        public ActionResult List(int buildingGroupId)
        {
            EstateService service = new EstateService();
            var data = service.GetBuildingByGroup(buildingGroupId).OrderBy(r => r.Id);
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
                EstateService service = new EstateService();
                int result = service.CreateBuilding(model);

                if (result != 0)
                {
                    return RedirectToAction("Details", "Building", new { area = "Estate", id = result });
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
            EstateService service = new EstateService();
            Building data = service.GetBuilding(id);

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
                EstateService service = new EstateService();
                bool result = service.UpdateBuilding(model);

                if (result)
                {
                    return RedirectToAction("Details", "Building", new { area = "Estate", id = model.Id });
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
            EstateService service = new EstateService();
            var data = service.GetBuilding(id);
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
            EstateService service = new EstateService();
            bool result = service.DeleteBuilding(id);

            if (result)
            {
                return RedirectToAction("Index", "Home", new { area = "Estate" });
            }
            else
                return View("Delete", id);
        }

        /// <summary>
        /// 楼层信息
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Floor(int id, int floorId = 0)
        {
            EstateService service = new EstateService();
            var data = service.GetBuilding(id);
            ViewBag.FloorId = floorId;
            ViewBag.Floors = JsonConvert.SerializeObject(data.Floors); 
            return View(data);
        }
        #endregion //Action
    }
}
