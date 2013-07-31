using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 楼群入住部门模型
    /// </summary>
    public class BuildingGroupDepartmentModel
    {
        public int BuildingGroupId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int RoomCount { get; set; }

        public double TotalUsableArea { get; set; }
    }

    /// <summary>
    /// 楼群侧边栏模型
    /// </summary>
    public class BuildingGroupSectionModel
    {
        public int BuildingGroupId { get; set; }

        public string BuildingGroupName { get; set; }

        public List<Building> Buildings { get; set; }

        public int RoomCount { get; set; }

        public int BuildArea { get; set; }

        public int UsableArea { get; set; }
    }
}