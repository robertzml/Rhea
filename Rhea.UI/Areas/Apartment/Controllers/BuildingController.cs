﻿using Rhea.Business;
using Rhea.Business.Apartment;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Apartment.Models;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Apartment.Controllers
{
    /// <summary>
    /// 青教建筑控制器
    /// </summary>
    [Privilege(Require = "ApartmentBuilding")]
    public class BuildingController : Controller
    {
        #region Action
        /// <summary>
        /// 青教楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Block(int id)
        {
            BlockModel data = new BlockModel();

            BuildingBusiness buildingBusiness = new BuildingBusiness();
            data.Block = buildingBusiness.GetBlock(id);

            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            data.Rooms = roomBusiness.GetByBuilding(id).OrderBy(r => r.Number).ToList();

            data.TotalArea = Math.Round(data.Rooms.Sum(r => r.UsableArea), RheaConstant.AreaDecimalDigits);

            return View(data);
        }

        /// <summary>
        /// 青教楼宇详细
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public ActionResult BlockDetails(int id)
        {
            BuildingBusiness buildingBusiness = new BuildingBusiness();
            Block data = buildingBusiness.GetBlock(id);
            return View(data);
        }

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
            data.Parent = block;
            data.Floor = f;

            if (!string.IsNullOrEmpty(f.ImageUrl))
                data.SvgPath = RheaConstant.SvgRoot + f.ImageUrl;
            else
                data.SvgPath = string.Empty;

            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            data.Rooms = roomBusiness.GetByBuilding(buildingId).Where(r => r.Floor == floor).ToList();

            return View(data);
        }
        #endregion //Action
    }
}