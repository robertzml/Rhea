using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.UI.Models;

namespace Rhea.UI.Controllers
{
    public class DepartmentController : Controller
    {
        #region Field
        /// <summary>
        /// 部门业务
        /// </summary>
        private IDepartmentBusiness departmentBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (departmentBusiness == null)
            {
                departmentBusiness = new MongoDepartmentBusiness();
            }

            base.Initialize(requestContext);
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
            return View(id);
        }

        /// <summary>
        /// 部门摘要
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            Department data = this.departmentBusiness.Get(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;
            return View(data);
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.departmentBusiness.GetList();
            return View(data);
        }

        /// <summary>
        /// 部门分楼宇信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public ActionResult BuildingSummary(int id)
        {
            Department department = this.departmentBusiness.Get(id);

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            List<Building> buildings = buildingBusiness.GetListByDepartment(id);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            List<DepartmentBuildingModel> data = new List<DepartmentBuildingModel>();

            foreach (var building in buildings)
            {
                DepartmentBuildingModel model = new DepartmentBuildingModel();
                var rooms = roomBusiness.GetListByDepartment(id, building.Id);

                model.DepartmentId = id;
                model.DepartmentName = department.Name;
                model.BuildingId = building.Id;
                model.BuildingName = building.Name;
                model.RoomCount = rooms.Count();
                model.TotalUsableArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));

                data.Add(model);
            }

            return View(data);
        }

        /// <summary>
        /// 部门相关楼宇
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public ActionResult Building(int id, int buildingId)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("DepartmentId", id);
            data.Add("BuildingId", buildingId);

            return View(data);
        }
        #endregion //Action

        #region Json
        #endregion //Json
    }
}
