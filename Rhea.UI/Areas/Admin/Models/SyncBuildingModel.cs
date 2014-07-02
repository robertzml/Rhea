using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Admin.Models
{
    public class SyncBuildingModel
    {
        /// <summary>
        /// 原始楼群数据
        /// </summary>
        public Dictionary<int, string> BuildingGroups { get; set; }
    }
}