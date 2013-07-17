using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Models
{
    /// <summary>
    /// 部门分楼宇模型
    /// </summary>
    public class DepartmentBuildingModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int BuildingId { get; set; }

        public string BuildingName { get; set; }

        public int RoomCount { get; set; }

        public double TotalUsableArea { get; set; }
    }
}