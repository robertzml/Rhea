using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 建筑使用类型
    /// </summary>
    public enum BuildingUseType
    {
        /// <summary>
        /// 学院楼宇
        /// </summary>
        [Display(Name = "学院楼宇")]
        CollegeBuilding = 1,

        /// <summary>
        /// 教学楼宇
        /// </summary>
        [Display(Name = "教学楼宇")]
        EducationBuilding = 2,

        /// <summary>
        /// 行政办公楼宇
        /// </summary>
        [Display(Name = "行政办公楼宇")]
        OfficeBuilding = 3,

        /// <summary>
        /// 宿舍楼宇
        /// </summary>
        [Display(Name = "宿舍楼宇")]
        Dormitory = 4,

        /// <summary>
        /// 辅助楼宇
        /// </summary>
        [Display(Name = "辅助楼宇")]
        Auxiliary = 5,

        /// <summary>
        /// 基础设施
        /// </summary>
        [Display(Name = "基础设施")]
        Infrastructure = 6,

        /// <summary>
        /// 室外运动场
        /// </summary>
        [Display(Name = "室外运动场")]
        Stadium = 7
    }

    public enum BuildingOrganizeType
    {
        /// <summary>
        /// 楼群
        /// </summary>
        [Display(Name = "楼群")]
        BuildingGroup = 1,

        /// <summary>
        /// 组团
        /// </summary>
        [Display(Name = "组团")]
        Cluster = 2,

        /// <summary>
        /// 独栋
        /// </summary>
        [Display(Name = "独栋")]
        Cottage = 3,

        /// <summary>
        /// 分区
        /// </summary>
        [Display(Name = "分区")]
        Subregion = 4,

        /// <summary>
        /// 楼宇
        /// </summary>
        [Display(Name = "楼宇")]
        Block = 5,

        /// <summary>
        /// 操场
        /// </summary>
        [Display(Name = "操场")]
        Playground = 6
    }
}
