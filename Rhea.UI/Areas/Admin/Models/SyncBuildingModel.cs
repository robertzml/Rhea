using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Admin.Models
{
    /// <summary>
    /// 同步原楼群模型
    /// </summary>
    public class SyncBuildingGroupModel
    {
        /// <summary>
        /// 原始楼群数据
        /// </summary>
        public Dictionary<int, string> BuildingGroups { get; set; }
    }

    /// <summary>
    /// 同步原楼宇模型
    /// </summary>
    public class SyncBuildingModel
    {
        /// <summary>
        /// 原始楼宇数据
        /// </summary>
        public Dictionary<int, string> Buildings { get; set; }
    }
}