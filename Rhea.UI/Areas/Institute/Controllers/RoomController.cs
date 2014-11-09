using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rhea.Business.Estate;
using Rhea.Model.Estate;
using Rhea.UI.Filters;

namespace Rhea.UI.Areas.Institute.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    [Privilege(Require = "InstituteManage")]
    public class RoomController : Controller
    {
        #region Field
        /// <summary>
        /// 房间业务对象
        /// </summary>
        private RoomBusiness roomBusiness;
        #endregion //Field

        #region Constructor
        public RoomController()
        {
            this.roomBusiness = new RoomBusiness();
        }
        #endregion //Constructor

        #region Action
        /// <summary>
        /// 房间信息页
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var data = this.roomBusiness.Get(id);
            return View(data);
        }

        /// <summary>
        /// 根据部门获取房间
        /// </summary>
        /// <param name="departmentId">所属部门ID</param>
        /// <returns></returns>
        public ActionResult ListByDepartment(int departmentId)
        {
            var data = this.roomBusiness.GetByDepartment(departmentId);
            return View(data);
        }
        #endregion //Action
    }
}