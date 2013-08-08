using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 楼群入住部门模型
    /// </summary>
    public class BuildingGroupDepartmentModel
    {
        public int BuildingGroupId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int RoomCount { get; set; }

        public double TotalUsableArea { get; set; }
    }

    /// <summary>
    /// 楼群侧边栏模型
    /// </summary>
    public class BuildingGroupSectionModel
    {
        /// <summary>
        /// 楼群ID
        /// </summary>
        public int BuildingGroupId { get; set; }

        /// <summary>
        /// 楼群名称
        /// </summary>
        public string BuildingGroupName { get; set; }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        public List<Building> Buildings { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public int BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public int UsableArea { get; set; }

        /// <summary>
        /// 办公用房面积比例
        /// </summary>
        public double OfficeAreaRatio { get; set; }

        /// <summary>
        /// 教学用房面积比例
        /// </summary>
        public double EducationAreaRatio { get; set; }

        /// <summary>
        /// 实验用房面积比例
        /// </summary>
        public double ExperimentAreaRatio { get; set; }

        /// <summary>
        /// 科研用房面积比例
        /// </summary>
        public double ResearchAreaRatio { get; set; }
    }
}