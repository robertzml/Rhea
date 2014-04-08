using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Estate.Models;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Estate.Controllers
{
    /// <summary>
    /// 楼群控制器
    /// </summary>
    [EnhancedAuthorize]
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
        /// 楼群摘要页
        /// </summary>
        /// <returns></returns>
        public ActionResult Summary()
        {
            return View();
        }

        /// <summary>
        /// 楼群主页
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            BuildingGroupSectionModel data = new BuildingGroupSectionModel();

            var buildingGroup = this.buildingGroupBusiness.Get(id);
            data.BuildingGroupId = id;
            data.BuildingGroupName = buildingGroup.Name;
            data.BuildArea = Convert.ToInt32(buildingGroup.BuildArea);
            data.UsableArea = Convert.ToInt32(this.buildingGroupBusiness.GetUsableArea(id));

            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            var buildings = buildingBusiness.GetListByBuildingGroup(id, true);
            data.Buildings = buildings;

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByBuildingGroup(id);

            double totArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));

            double offArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 1).Sum(r => r.UsableArea));
            data.OfficeAreaRatio = Math.Round(offArea / totArea * 100, 2);

            double eduArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 2).Sum(r => r.UsableArea));
            data.EducationAreaRatio = Math.Round(eduArea / totArea * 100, 2);

            double expArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 3).Sum(r => r.UsableArea));
            data.ExperimentAreaRatio = Math.Round(expArea / totArea * 100, 2);

            double resArea = Convert.ToDouble(rooms.Where(r => r.Function.FirstCode == 4).Sum(r => r.UsableArea));
            data.ResearchAreaRatio = Math.Round(resArea / totArea * 100, 2);

            data.OtherAreaRatio = 100 - data.OfficeAreaRatio - data.EducationAreaRatio - data.ExperimentAreaRatio - data.ResearchAreaRatio;
            if (data.OtherAreaRatio < 0)
                data.OtherAreaRatio = 0;

            data.RoomCount = rooms.Count;

            return View(data);
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
            data.UsableArea = this.buildingGroupBusiness.GetUsableArea(id);

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
            data.UsableArea = Math.Round(this.buildingGroupBusiness.GetUsableArea(id), RheaConstant.AreaDecimalDigits);
            return View(data);
        }

        /// <summary>
        /// 楼群列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            List<BuildingGroup> data = this.buildingGroupBusiness.GetList(true);
            data.ForEach(r => r.UsableArea = this.buildingGroupBusiness.GetUsableArea(r.Id));
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
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var roomList = roomBusiness.GetListByBuildingGroup(id);

            // get departments in one building
            var dList = roomList.GroupBy(r => r.DepartmentId).Select(s => new { s.Key, Count = s.Count(), Area = s.Sum(r => r.UsableArea) });

            foreach (var d in dList)
            {
                BuildingGroupDepartmentModel model = new BuildingGroupDepartmentModel
                {
                    BuildingGroupId = id,
                    DepartmentId = d.Key,
                    RoomCount = d.Count,
                    TotalUsableArea = Math.Round(Convert.ToDouble(d.Area), RheaConstant.AreaDecimalDigits)
                };
                model.DepartmentName = departmentBusiness.GetName(model.DepartmentId);

                data.Add(model);
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
        /// 分类统计
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public ActionResult Classify(int id)
        {
            IStatisticBusiness statisticBusiness = new MongoStatisticBusiness();
            var data = statisticBusiness.GetBuildingGroupTotalArea(id);

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
                    UsableArea = Math.Round(buildingBusiness.GetUsableArea(building.Id), 2)
                };

                m.RoomCount = roomBusiness.CountByBuilding(building.Id);

                data.Add(m);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
