using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 建筑入驻部门模型
    /// </summary>
    public class BuildingDepartmentModel
    {
        /// <summary>
        /// 建筑ID
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

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