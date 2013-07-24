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
using Rhea.UI.Areas.Estate.Models;

namespace Rhea.UI.Areas.Estate.Controllers
{
    public class BuildingGroupController : Controller
    {
        #region Field
        /// <summary>
        /// 楼群业务
        /// </summary>
        private IBuildingGroupBusiness buildingGroupBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (buildingGroupBusiness == null)
            {
                buildingGroupBusiness = new MongoBuildingGroupBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 楼群主页
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            ViewBag.Title = this.buildingGroupBusiness.GetName(id);
            return View(id);
        }

        /// <summary>
        /// 楼群概况
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Intro(int id)
        {
            BuildingGroup data = this.buildingGroupBusiness.Get(id);
            if (!string.IsNullOrEmpty(data.ImageUrl))
                data.ImageUrl = RheaConstant.ImagesRoot + data.ImageUrl;
            if (!string.IsNullOrEmpty(data.PartMapUrl))
                data.PartMapUrl = RheaConstant.ImagesRoot + data.PartMapUrl;

            return View(data);
        }

        /// <summary>
        /// 楼群详细
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            BuildingGroup data = this.buildingGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 楼群列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            List<BuildingGroup> data = this.buildingGroupBusiness.GetList().OrderBy(r => r.Id).ToList();
            return View(data);
        }

        /// <summary>
        /// 楼群入住部门
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Department(int id)
        {
            List<BuildingGroupDepartmentModel> data = new List<BuildingGroupDepartmentModel>();

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetListByBuildingGroup(id);

            foreach (var building in buildings)
            {
                IRoomBusiness roomBusiness = new MongoRoomBusiness();
                var roomList = roomBusiness.GetListByBuilding(building.Id);

                // get departments in one building
                var dList = roomList.GroupBy(r => r.DepartmentId).Select(s => new { s.Key, Count = s.Count(), Area = s.Sum(r => r.UsableArea) });

                foreach (var d in dList)
                {
                    BuildingGroupDepartmentModel model = new BuildingGroupDepartmentModel
                    {
                        BuildingGroupId = id,
                        DepartmentId = d.Key,
                        RoomCount = d.Count,
                        TotalUsableArea = Convert.ToDouble(d.Area)
                    };

                    model.DepartmentName = departmentBusiness.GetName(model.DepartmentId);

                    var result = data.Find(r => r.DepartmentId == model.DepartmentId);
                    if (result != null)
                    {
                        result.RoomCount += model.RoomCount;
                        result.TotalUsableArea = model.TotalUsableArea;
                    }
                    else
                        data.Add(model);
                }
            }

            return View(data);
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Statistic(int id)
        {
            BuildingGroup data = this.buildingGroupBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 分楼宇面积比较
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult BuildingTotalAreaCompare(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 房间汇总
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult RoomSummary(int id)
        {
            return View();
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 分楼宇面积比较
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public JsonResult BuildingTotalAreaCompareData(int id)
        {
            List<BuildingTotalAreaModel> data = new List<BuildingTotalAreaModel>();

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetListByBuildingGroup(id).OrderBy(r => r.Id);
            foreach (var building in buildings)
            {
                BuildingTotalAreaModel m = new BuildingTotalAreaModel
                {
                    BuildingId = building.Id,
                    BuildingName = building.Name,
                    BuildArea = Convert.ToDouble(building.BuildArea),
                    UsableArea = Convert.ToDouble(building.UsableArea)
                };

                m.RoomCount = roomBusiness.CountByBuilding(building.Id);

                data.Add(m);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
