using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Institute.Models
{
    /// <summary>
    /// 部门所在建筑模型
    /// </summary>
    public class DepartmentBuildingModel
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 建筑ID
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public double TotalUsableArea { get; set; }
    }
}