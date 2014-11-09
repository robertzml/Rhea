using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Areas.Institute.Models;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rhea.UI.Areas.Institute.Controllers
{
    /// <summary>
    /// 部门控制器
    /// </summary>
    [Privilege(Require = "InstituteManage")]
    public class DepartmentController : Controller
    {
        #region Field
        /// <summary>
        /// 部门业务对象
        /// </summary>
        private DepartmentBusiness departmentBusiness;
        #endregion //Field

        #region Constructor
        public DepartmentController()
        {
            this.departmentBusiness = new DepartmentBusiness();
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 获取部门所在建筑
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        private List<DepartmentBuildingModel> GetEnterBuilding(int id)
        {
            List<DepartmentBuildingModel> data = new List<DepartmentBuildingModel>();
            RoomBusiness roomBusiness = new RoomBusiness();
            BuildingBusiness buildingBusiness = new BuildingBusiness();

            var rooms = roomBusiness.GetByDepartment(id);
            var bList = rooms.GroupBy(r => r.BuildingId).Select(s => new { s.Key, Count = s.Count(), Area = s.Sum(t => t.UsableArea) }).OrderBy(g => g.Key);

            foreach (var b in bList)
            {
                DepartmentBuildingModel model = new DepartmentBuildingModel
                {
                    DepartmentId = id,
                    BuildingId = b.Key,
                    RoomCount = b.Count,
                    TotalUsableArea = Math.Round(b.Area, RheaConstant.AreaDecimalDigits)
                };

                model.BuildingName = buildingBusiness.Get(model.BuildingId).Name;

                data.Add(model);
            }

            return data;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 部门主页
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            DepartmentIndexModel data = new DepartmentIndexModel();

            data.Department = this.departmentBusiness.Get(id);

            if (!string.IsNullOrEmpty(data.Department.ImageUrl))
                data.Department.ImageUrl = RheaConstant.ImagesRoot + data.Department.ImageUrl;

            data.EnterBuilding = GetEnterBuilding(id);

            return View(data);
        }
        #endregion //Action
    }
}