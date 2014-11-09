using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Personnel;

namespace Rhea.UI.Areas.Institute.Models
{
    /// <summary>
    /// 部门主页模型
    /// </summary>
    public class DepartmentIndexModel
    {
        /// <summary>
        /// 部门对象
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// 所在建筑
        /// </summary>
        public List<DepartmentBuildingModel> EnterBuilding { get; set; }
    }
}