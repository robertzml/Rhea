using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 楼层模型
    /// </summary>
    public class FloorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public string SvgPath { get; set; }

        public double BuildArea { get; set; }

        public double UsableArea { get; set; }

        public int RoomCount { get; set; }
    }

    /// <summary>
    /// 楼宇总面积模型
    /// </summary>
    public class BuildingTotalAreaModel
    {
        public int BuildingId { get; set; }

        public string BuildingName { get; set; }

        public double BuildArea { get; set; }

        public double UsableArea { get; set; }

        public int RoomCount { get; set; }
    }

    /// <summary>
    /// 楼宇入住部门模型
    /// </summary>
    public class BuildingDepartmentModel
    {
        public int BuildingId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int RoomCount { get; set; }

        public double TotalUsableArea { get; set; }
    }

    /// <summary>
    /// 楼宇侧边栏模型
    /// </summary>
    public class BuildingSectionModel
    {
        public int BuildingId { get; set; }

        public string BuildingName { get; set; }

        public int BuildingGroupId { get; set; }

        public int RoomCount { get; set; }

        public int BuildArea { get; set; }

        public int UsableArea { get; set; }

        public List<Floor> Floors { get; set; }
    }
}