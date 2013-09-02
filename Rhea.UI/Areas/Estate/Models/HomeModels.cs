using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 房产主页模型
    /// </summary>
    public class EstateHomeModel
    {
        /// <summary>
        /// 陆地面积
        /// </summary>
        public double LandArea { get; set; }

        /// <summary>
        /// 水域面积
        /// </summary>
        public double WaterArea { get; set; }

        /// <summary>
        /// 陆地比例
        /// </summary>
        public double LandPercentage { get; set; }
    }

    /// <summary>
    /// 房产摘要模型
    /// </summary>
    public class EstateSummaryModel
    {
        public int CampusCount { get; set; }

        public int BuildingGroupCount { get; set; }

        public int BuildingCount { get; set; }

        public int FloorCount { get; set; }

        public int RoomCount { get; set; }
    }
}