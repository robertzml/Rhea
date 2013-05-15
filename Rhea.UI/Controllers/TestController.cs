using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business;
using Rhea.Data.Entities;
using Rhea.Data.Estate;
using Rhea.Business.Estate;

namespace Rhea.UI.Controllers
{
    [AllowAnonymous]
    public class TestController : Controller
    {
        #region Action
        public ActionResult Index()
        {
            IBuildingGroupService buildingGroupService = new MongoBuildingGroupService();
            var data = buildingGroupService.Get(100023);
            return View(data);
        }
        #endregion //Action

        #region Json
        public JsonResult CollegeClassifyData()
        {
            //get codes
            IRoomService roomService = new MongoRoomService();
            var functionCodes = roomService.GetFunctionCodeList();

            List<CollegeClassifyAreaModel> data = new List<CollegeClassifyAreaModel>();
            //get area by department
            int departmentId = 100400;

            CollegeClassifyAreaModel c = new CollegeClassifyAreaModel
            {
                Id = 100400,
                CollegeName = "物联网工程学院"
            };

            IStatisticService statisticService = new MongoStatisticService();

            c.OfficeDetailArea = statisticService.GetClassifyArea(departmentId, 1, functionCodes);
            c.OfficeArea = Math.Round(c.OfficeDetailArea.Sum(r => r.Area), 3);

            c.EducationDetailArea = statisticService.GetClassifyArea(departmentId, 2, functionCodes);
            c.EducationArea = Math.Round(c.EducationDetailArea.Sum(r => r.Area), 3);

            c.ExperimentDetailArea = statisticService.GetClassifyArea(departmentId, 3, functionCodes);
            c.ExperimentArea = Math.Round(c.ExperimentDetailArea.Sum(r => r.Area), 3);

            c.ResearchDetailArea = statisticService.GetClassifyArea(departmentId, 4, functionCodes);
            c.ResearchArea = Math.Round(c.ResearchDetailArea.Sum(r => r.Area), 3);

            data.Add(c);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
