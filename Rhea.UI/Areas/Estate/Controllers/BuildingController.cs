using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Estate.Models;
using Rhea.Business;
using Rhea.Business.Personnel;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 建筑控制器
    /// </summary>
    public class BuildingController : Controller
    {
        #region Function
        /// <summary>
        /// 初始化楼群显示模型
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        private BuildingGroupIndexModel InitBuildingGroup(int id)
        {
            BuildingBusiness business = new BuildingBusiness();

            BuildingGroupIndexModel data = new BuildingGroupIndexModel();
            data.BuildingGroup = business.GetBuildingGroup(id);
            data.Subregions = business.GetChildSubregions(id).OrderBy(r => r.Sort).ToList();

            if (!string.IsNullOrEmpty(data.BuildingGroup.ImageUrl))
                data.BuildingGroup.ImageUrl = RheaConstant.ImagesRoot + data.BuildingGroup.ImageUrl;

            data.EnterDepartment = GetEnterDepartment(id);

            return data;
        }

        /// <summary>
        /// 初始化分区显示模型
        /// </summary>
        /// <param name="id">分区ID</param>
        /// <returns></returns>
        private SubregionIndexModel InitSubregion(int id)
        {
            BuildingBusiness business = new BuildingBusiness();

            SubregionIndexModel data = new SubregionIndexModel();
            data.Subregion = business.GetSubregion(id);
            data.Parent = business.GetBuildingGroup(data.Subregion.ParentId);

            if (!string.IsNullOrEmpty(data.Subregion.ImageUrl))
                data.Subregion.ImageUrl = RheaConstant.ImagesRoot + data.Subregion.ImageUrl;

            data.EnterDepartment = GetEnterDepartment(id);

            return data;
        }

        /// <summary>
        /// 获取建筑入驻部门
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <returns></returns>
        private List<BuildingDepartmentModel> GetEnterDepartment(int buildingId)
        {
            RoomBusiness roomBusiness = new RoomBusiness();
            DepartmentBusiness departmentBusiness = new DepartmentBusiness();

            List<BuildingDepartmentModel> data = new List<BuildingDepartmentModel>();
            var rooms = roomBusiness.GetByBuilding(buildingId);
            var dList = rooms.GroupBy(r => r.DepartmentId).Select(s => new { s.Key, Count = s.Count(), Area = s.Sum(r => r.UsableArea) }).OrderBy(g => g.Key);

            foreach (var d in dList)
            {
                BuildingDepartmentModel model = new BuildingDepartmentModel
                {
                    BuildingId = buildingId,
                    DepartmentId = d.Key,
                    RoomCount = d.Count,
                    TotalUsableArea = Math.Round(d.Area, RheaConstant.AreaDecimalDigits)
                };
                model.DepartmentName = departmentBusiness.Get(model.DepartmentId).Name;

                data.Add(model);
            }

            return data;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 建筑主页
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            BuildingBusiness business = new BuildingBusiness();
            var building = business.Get(id);
            switch ((BuildingOrganizeType)building.OrganizeType)
            {
                case BuildingOrganizeType.BuildingGroup:
                    var data1 = InitBuildingGroup(id);
                    return View("BuildingGroupIndex", data1);

                case BuildingOrganizeType.Subregion:
                    var data2 = InitSubregion(id);
                    return View("SubregionIndex", data2);
            }
            return View(building);
        }
        #endregion //Action
    }
}