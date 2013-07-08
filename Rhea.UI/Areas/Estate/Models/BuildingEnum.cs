using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 楼宇使用类型
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
        Dormitory = 4
    }
}