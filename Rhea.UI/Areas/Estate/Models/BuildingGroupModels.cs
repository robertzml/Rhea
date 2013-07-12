using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}