using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;

namespace Rhea.UI.Models
{
    /// <summary>
    /// 房产菜单模型
    /// </summary>
    public class EstateMenuModel
    {
        public List<BuildingGroup> BuildingGroups { get; set; }
    }

    /// <summary>
    /// 部门菜单模型
    /// </summary>
    public class DepartmentMenuModel
    {
        public List<Department> Departments { get; set; }

        /// <summary>
        /// 部门管理员相关
        /// </summary>
        public Department Single { get; set; }
    }

    /// <summary>
    /// 地图标记点模型
    /// </summary>
    public class MapPointModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public double PointX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double PointY { get; set; }

        /// <summary>
        /// 缩放级别
        /// </summary>
        public int Zoom { get; set; }

        public string Pin { get; set; }

        public string Symbol { get; set; }
    }
}