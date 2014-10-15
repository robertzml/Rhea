using Rhea.Business;
using Rhea.Common;
using Rhea.Model;
using Rhea.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Model.Account;
using Rhea.UI.Services;

namespace Rhea.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 字典控制器
    /// </summary>
    [EnhancedAuthorize(Rank = 900)]
    public class DictionaryController : Controller
    {
        #region Field
        /// <summary>
        /// 字典业务
        /// </summary>
        private DictionaryBusiness dictionaryBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (dictionaryBusiness == null)
            {
                dictionaryBusiness = new DictionaryBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
        // GET: Admin/Dictionary
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 字典集列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var data = this.dictionaryBusiness.Get();
            return View(data);
        }

        /// <summary>
        /// 字典集信息
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        public ActionResult Details(string name)
        {
            var data = this.dictionaryBusiness.Get(name);
            return View(data);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="model">字典模型</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Dictionary model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.dictionaryBusiness.Create(model);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "添加字典失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                string[] lines = Regex.Split(model.PropertyString, "\r\n");
                if (model.Type == (int)DictionaryType.Text)
                {
                    result = this.dictionaryBusiness.UpdateTextProperty(model.Name, lines.ToList());
                }
                else if (model.Type == (int)DictionaryType.Pair)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrEmpty(line))
                            continue;
                        string[] pair = Regex.Split(line, ":");
                        dict.Add(Convert.ToInt32(pair[0]), pair[1]);
                    }

                    result = this.dictionaryBusiness.UpdatePairProperty(model.Name, dict);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台添加字典",
                    Time = DateTime.Now,
                    Type = (int)LogType.DictionaryCreate,
                    Content = string.Format("添加字典, 名称:{0}, 标题:{1}。", model.Name, model.Title),
                    UserId = user._id,
                    UserName = user.Name
                };
                LogBusiness logBusiness = new LogBusiness();
                result = logBusiness.Create(log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "添加字典成功";
                return RedirectToAction("List", "Dictionary");
            }

            return View(model);
        }

        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(string name)
        {
            var data = this.dictionaryBusiness.Get(name);
            data.PropertyString = Regex.Replace(data.PropertyString, "<br />", "\r\n");
            return View(data);
        }

        /// <summary>
        /// 编辑字典
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Dictionary model)
        {
            if (ModelState.IsValid)
            {
                ErrorCode result = this.dictionaryBusiness.Edit(model);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "编辑字典失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                string[] lines = Regex.Split(model.PropertyString, "\r\n");
                if (model.Type == (int)DictionaryType.Text)
                {
                    result = this.dictionaryBusiness.UpdateTextProperty(model.Name, lines.ToList());
                }
                else if (model.Type == (int)DictionaryType.Pair)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrEmpty(line))
                            continue;
                        string[] pair = Regex.Split(line, ":");
                        dict.Add(Convert.ToInt32(pair[0]), pair[1]);
                    }

                    result = this.dictionaryBusiness.UpdatePairProperty(model.Name, dict);
                }

                //log
                User user = PageService.GetCurrentUser(User.Identity.Name);
                Log log = new Log
                {
                    Title = "后台编辑字典",
                    Time = DateTime.Now,
                    Type = (int)LogType.DictionaryEdit,
                    Content = string.Format("编辑字典, 名称:{0}, 标题:{1}。", model.Name, model.Title),
                    UserId = user._id,
                    UserName = user.Name
                };
                LogBusiness logBusiness = new LogBusiness();
                result = logBusiness.Create(log);
                if (result != ErrorCode.Success)
                {
                    TempData["Message"] = "记录日志失败";
                    ModelState.AddModelError("", result.DisplayName());
                    return View(model);
                }

                TempData["Message"] = "编辑字典成功";
                return RedirectToAction("List", "Dictionary");
            }

            return View(model);
        }
        #endregion //Action
    }
}