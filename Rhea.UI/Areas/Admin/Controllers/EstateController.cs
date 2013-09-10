using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using Rhea.Business;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Data;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using Rhea.UI.Areas.Admin.Models;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [EnhancedAuthorize(Rank = 600)]
    public class EstateController : Controller
    {
        #region Function
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
        /// 房产管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房间功能
        /// </summary>
        /// <returns></returns>
        public ActionResult FunctionCode()
        {
            EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
            var codes = dictionaryBusiness.GetRoomFunctionCode();

            string data = "";
            foreach (var item in codes)
            {
                data += item.FirstCode + "|" + item.SecondCode + "|" + item.ClassifyName + "|" + item.FunctionProperty
                    + "|" + item.Remark + "\r\n";
            }
            ViewBag.Codes = data;

            return View();
        }

        /// <summary>
        /// 房间功能
        /// </summary>
        /// <param name="functionCodeText">功能内容</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult FunctionCode(string functionCodeText)
        {
            try
            {
                string[] codes = Regex.Split(functionCodeText, "\r\n");

                List<RoomFunctionCode> functionCodes = new List<RoomFunctionCode>();
                foreach (string code in codes)
                {
                    if (code.Trim().Length == 0)
                        continue;

                    string[] items = code.Split('|');
                    RoomFunctionCode fc = new RoomFunctionCode
                    {
                        CodeId = items[0] + "." + items[1],
                        FirstCode = Convert.ToInt32(items[0]),
                        SecondCode = Convert.ToInt32(items[1]),
                        ClassifyName = items[2],
                        FunctionProperty = items[3],
                        Remark = items[4]
                    };

                    functionCodes.Add(fc);
                }

                EstateDictionaryBusiness dictionaryBusiness = new EstateDictionaryBusiness();
                bool result = dictionaryBusiness.EditRoomFunctionCode(functionCodes);
                if (result)
                {
                    TempData["Message"] = "编辑房间功能属性成功。";
                    RedirectToAction("FunctionCode");
                }
                else
                {
                    TempData["Message"] = "编辑房间功能属性失败。";
                    RedirectToAction("FunctionCode");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "输入格式有误。");
                return View();
            }

            return View();
        }

        /// <summary>
        /// 导出任务
        /// </summary>
        /// <returns></returns>
        public ActionResult Export()
        {
            return View();
        }        

        /// <summary>
        /// 归档任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Archive()
        {
            return View();
        }

        /// <summary>
        /// 归档任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Archive(ArchiveModel model)
        {
            if (ModelState.IsValid)
            {
                var user = GetUser();
                Log log = new Log
                {
                    Content = model.ArchiveContent,
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    RelateTime = model.ArchiveDate
                };

                bool result = false;

                log.Title = "归档校区";
                log.Type = (int)LogType.CampusArchive;
                ICampusBusiness campusBusiness = new MongoCampusBusiness();
                result = campusBusiness.Archive(log);

                log.Title = "归档楼群";
                log.Type = (int)LogType.BuildingGroupArchive;
                IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
                result = buildingGroupBusiness.Archive(log);

                log.Title = "归档楼宇";
                log.Type = (int)LogType.BuildingArchive;
                IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
                result = buildingBusiness.Archive(log);

                log.Title = "归档房间";
                log.Type = (int)LogType.RoomArchive;
                IRoomBusiness roomBusiness = new MongoRoomBusiness();
                result = roomBusiness.Archive(log);

                log.Title = "归档部门";
                log.Type = (int)LogType.DepartmentArchive;
                IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
                result = departmentBusiness.Archive(log);

                if (!result)
                {
                    ModelState.AddModelError("", "归档失败");
                }
                else
                {
                    TempData["Message"] = "归档成功";
                    return RedirectToAction("ArchiveList");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 归档数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ArchiveSingle()
        {
            return View();
        }

        /// <summary>
        /// 单独归档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ArchiveSingle(ArchiveModel model)
        {
            if (ModelState.IsValid)
            {
                var user = GetUser();
                Log log = new Log
                {
                    Content = model.ArchiveContent,
                    Time = DateTime.Now,
                    UserId = user._id,
                    UserName = user.Name,
                    RelateTime = model.ArchiveDate
                };

                bool result = false;
                switch (model.ArchiveType)
                {
                    case 1:
                        log.Title = "归档校区";
                        log.Type = (int)LogType.CampusArchive;
                        ICampusBusiness campusBusiness = new MongoCampusBusiness();
                        result = campusBusiness.Archive(log);
                        break;
                    case 2:
                        log.Title = "归档楼群";
                        log.Type = (int)LogType.BuildingGroupArchive;
                        IBuildingGroupBusiness buildingGroupBusiness = new MongoBuildingGroupBusiness();
                        result = buildingGroupBusiness.Archive(log);
                        break;
                    case 3:
                        log.Title = "归档楼宇";
                        log.Type = (int)LogType.BuildingArchive;
                        IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
                        result = buildingBusiness.Archive(log);
                        break;
                    case 4:
                        log.Title = "归档房间";
                        log.Type = (int)LogType.RoomArchive;
                        IRoomBusiness roomBusiness = new MongoRoomBusiness();
                        result = roomBusiness.Archive(log);
                        break;
                }

                if (!result)
                {
                    ModelState.AddModelError("", "归档失败");
                }
                else
                {
                    TempData["Message"] = "归档成功";
                    return RedirectToAction("ArchiveList");
                }
            }

            return View(model);
        }

        /// <summary>
        /// 归档列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ArchiveList()
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            int[] types = { (int)LogType.CampusArchive, (int)LogType.BuildingGroupArchive, 
                              (int)LogType.BuildingArchive, (int)LogType.RoomArchive };
            var data = logBusiness.GetList(types);
            return View(data);
        }

        /// <summary>
        /// 日志列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LogList()
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            var data = logBusiness.GetList().OrderByDescending(r => r.Time);

            return View(data);
        }

        /// <summary>
        /// 日志详细
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns></returns>
        public ActionResult LogDetails(string id)
        {
            ILogBusiness logBusiness = new MongoLogBusiness();
            var data = logBusiness.Get(id);
            
            return View(data);
        }
        #endregion //Action
    }
}
