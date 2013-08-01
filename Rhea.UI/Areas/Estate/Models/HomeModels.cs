using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Estate.Models
{
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