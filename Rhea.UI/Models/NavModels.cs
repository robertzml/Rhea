using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;
using Rhea.Data.Estate;

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
    /// FrontView插件页面模型
    /// </summary>
    public class FrontPluginModel
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }

        public double OfficeArea { get; set; }

        public double EducationArea { get; set; }

        public double ExperimentArea { get; set; }

        public double ResearchArea { get; set; }

        /// <summary>
        /// 面积数据
        /// </summary>
        public DepartmentTotalAreaModel Area { get; set; }
    }
}