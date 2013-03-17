using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

            return View(data);
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
        #endregion //Action
    }
}
