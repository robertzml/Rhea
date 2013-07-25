using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Models
{
    /// <summary>
    /// 部门侧边栏模型
    /// </summary>
    public class DepartmentSectionModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public List<Building> Buildings { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }

        /// <summary>
        /// 使用总面积
        /// </summary>
        public int TotalArea { get; set; }
    }
}