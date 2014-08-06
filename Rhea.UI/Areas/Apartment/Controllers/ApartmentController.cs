﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Apartment.Models;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 青教公寓控制器
    /// </summary>
    public class ApartmentController : Controller
    {
        #region Action
        /// <summary>
        /// 青教楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult Floor(int buildingId, int floor)
        {
            BuildingBusiness buildingBusiness = new BuildingBusiness();
            var block = buildingBusiness.GetBlock(buildingId);

            var f = block.Floors.Single(r => r.Number == floor);

            FloorModel data = new FloorModel();
            data.BuildingId = buildingId;
            data.BuildingName = block.Name;
            data.Floor = f;
            
            RoomBusiness roomBusiness = new RoomBusiness();
            data.Rooms = roomBusiness.GetByBuilding(buildingId).Where(r => r.Floor == floor).ToList();

            return View(data);
        }
        #endregion //Action
    }
}