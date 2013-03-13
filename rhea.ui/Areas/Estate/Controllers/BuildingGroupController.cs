﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 楼群控制器
    /// </summary>
    public class BuildingGroupController : Controller
    {
        #region Action
        /// <summary>
        /// GET: /Estate/BuildingGroup/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 楼群详细
        /// GET: /Estate/BuildingGroup/Details/7
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            EstateService service = new EstateService();
            BuildingGroup data = service.GetBuildingGroup(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImageServer + data.ImageUrl;

            data.Buildings = service.GetBuildingByGroup(id).OrderBy(r => r.Id).ToList();

            return View(data);
        }

        /// <summary>
        /// 编辑楼群
        /// GET: /Estate/BuildingGroup/Edit/7
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            EstateService service = new EstateService();
            BuildingGroup data = service.GetBuildingGroup(id);
            return View(data);
        }

        /// <summary>
        /// 编辑楼群
        /// POST: /Estate/BuildingGroup/Edit/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(BuildingGroup model)
        {
            if (ModelState.IsValid)
            {
                EstateService service = new EstateService();
                bool result = service.UpdateBuildingGroup(model);

                if (result)
                {
                    return RedirectToAction("Details", "BuildingGroup", new { area = "Estate", id = model.Id });
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
