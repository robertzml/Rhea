using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;

namespace Rhea.UI.Models
{
    public class NavModel
    {
        public List<Department> Departments { get; set; }

        public List<BuildingGroup> BuildingGroups { get; set; }
    }

    /// <summary>
    /// 房产菜单模型
    /// </summary>
    public class EstateMenuModel
    {
        public List<BuildingGroup> BuildingGroups { get; set; }
    }

    /// <summary>
    /// 部门菜单模型
    /// </summary>
    public class DepartmentMenuModel
    {
        public List<Department> Departments { get; set; }
    }
}