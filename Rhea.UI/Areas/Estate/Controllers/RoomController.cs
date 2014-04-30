using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Estate.Models;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Energy;

namespace Rhea.UI.Areas.Estate.Controllers
{
    [EnhancedAuthorize]
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 房间业务
        /// </summary>
        private IRoomBusiness roomBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (roomBusiness == null)
            {
                roomBusiness = new MongoRoomBusiness();
            }

            base.Initialize(requestContext);
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        private UserProfile GetUser()
        {
            IAccountBusiness accountBusiness = new MongoAccountBusiness();
            UserProfile user = accountBusiness.GetByUserName(User.Identity.Name);
            return user;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 房间信息
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }              

        /// <summary>
        /// 房间基本信息
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Intro(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 房间摘要
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Summary(int id)
        {
            Room data = this.roomBusiness.Get(id);

            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            ViewBag.DepartmentName = departmentBusiness.GetName(data.DepartmentId);

            return View(data);
        }        

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public ActionResult ListByBuildingGroup(int buildingGroupId)
        {
            var data = this.roomBusiness.GetListByBuildingGroup(buildingGroupId);
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public ActionResult ListByBuilding(int buildingId)
        {
            var data = this.roomBusiness.GetListByBuilding(buildingId);
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult ListByFloor(int buildingId, int floor)
        {
            var data = this.roomBusiness.GetListByBuilding(buildingId, floor);
            return View(data);
        }

        /// <summary>
        /// 房间列表
        /// </summary>
        /// <param name="departmentId">所属部门ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public ActionResult ListByDepartment(int departmentId, int buildingId = 0, int? floor = null)
        {
            var data = this.roomBusiness.GetListByDepartment(departmentId);

            if (buildingId != 0)
                data = data.Where(r => r.BuildingId == buildingId).ToList();

            if (floor != null)
                data = data.Where(r => r.Floor == floor).ToList();

            return View(data);
        }

        /// <summary>
        /// 房间功能下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomFunction()
        {
            EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
            var data = dictionaryBusiness.GetRoomFunctionCode();
            return View(data);
        }

        /// <summary>
        /// 房间分配
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        [EnhancedAuthorize(Rank=650)]
        [HttpGet]
        public ActionResult Assign(int id)
        {
            var data = this.roomBusiness.Get(id);

            var drooms = this.roomBusiness.GetListByDepartment(data.DepartmentId);
            ViewBag.DepartmentArea = Math.Round(Convert.ToDouble(drooms.Sum(r => r.UsableArea)), RheaConstant.AreaDecimalDigits);

            return View(data);
        }

        /// <summary>
        /// 房间分配
        /// </summary>
        /// <returns></returns>
        [EnhancedAuthorize(Rank = 650)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Assign()
        {
            string msg, title = "房间管理";

            int roomId = Convert.ToInt32(Request.Form["RoomId"]);

            if (Request.Form["DepartmentId"] == "")
            {
                msg = "未选择部门";
                return RedirectToAction("ShowMessage", new { controller = "Common", area = "", msg = msg, title = title });
            }

            int departmentId = Convert.ToInt32(Request.Form["DepartmentId"]);   //新部门
            int currentDepartmentId = Convert.ToInt32(Request.Form["CurrentDepartmentId"]); //原部门

            if (departmentId == currentDepartmentId)
            {
                msg = "当前部门与新部门相同";
                return RedirectToAction("ShowMessage", new { controller = "Common", area = "", msg = msg, title = title });
            }

            //backup
            bool backok = this.roomBusiness.Backup(roomId);
            if (!backok)
            {
                msg = "备份失败";
                return RedirectToAction("ShowMessage", new { controller = "Common", area = "", msg = msg, title = title });
            }

            //assign
            bool result = this.roomBusiness.Assign(roomId, departmentId);
            if (!result)
            {
                msg = "分配房间失败";
                return RedirectToAction("ShowMessage", new { controller = "Common", area = "", msg = msg, title = title });
            }

            //log
            var user = GetUser();
            var room = this.roomBusiness.Get(roomId);
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            string oldName = departmentBusiness.GetName(currentDepartmentId);
            string newName = departmentBusiness.GetName(departmentId);
            Log log = new Log
            {
                Title = "分配房间",
                Content = string.Format("分配房间, 房间ID:{0}, 房间名称:{1}, 房间编号:{2}, 原部门ID:{3}, 原部门名称:{4}, 新部门ID:{5}, 新部门名称:{6}.",
                    roomId, room.Name, room.Number, currentDepartmentId, oldName, departmentId, newName),
                Time = DateTime.Now,
                UserId = user._id,
                UserName = user.Name,
                Type = (int)LogType.RoomAssign
            };

            bool logok = this.roomBusiness.Log(roomId, log);
            if (!logok)
            {
                msg = "记录日志失败";
                return RedirectToAction("ShowMessage", new { controller = "Common", area = "", msg = msg, title = title });
            }

            msg = "分配房间成功";
            return RedirectToAction("ShowMessage", new { controller = "Common", area = "", msg = msg, title = title });
        }

        /// <summary>
        /// 房间分配历史
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult AssignHistory(int id)
        {
            var data = this.roomBusiness.GetAssignHistory(id);
            return View(data);
        }

        /// <summary>
        /// 用电情况
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Electric(int id)
        {
            ElectricModel model = new ElectricModel();
            model.RoomId = id;
            return View(model);
        }
        #endregion //Action

        #region Json
        /// <summary>
        /// 得到二级分类
        /// </summary>
        /// <param name="firstCode">一级编码</param>
        /// <returns></returns>
        public JsonResult GetSecondClassify(int firstCode)
        {
            EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
            var data = dictionaryBusiness.GetRoomFunctionCode();
            data = data.Where(r => r.FirstCode == firstCode).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取房间使用面积
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public JsonResult GetUsableArea(int id)
        {
            double area = this.roomBusiness.GetUsableArea(id);
            return Json(area, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用电数据
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="date">查询日期</param>
        /// <returns></returns>
        public JsonResult GetElectric(int id)
        {
            MongoElectricBusiness electricBusiness = new MongoElectricBusiness();
            var data = electricBusiness.GetHourValueByDay(id, DateTime.Now);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion //Json
    }
}
