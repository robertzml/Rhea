using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}