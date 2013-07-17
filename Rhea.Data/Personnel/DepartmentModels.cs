using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Data.Personnel
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public enum DepartmentType
    {
        /// <summary>
        /// 教学院系
        /// </summary>
        [Display(Name = "教学院系")]
        Type1 = 1,

        /// <summary>
        /// 科研机构
        /// </summary>
        [Display(Name = "科研机构")]
        Type2,

        /// <summary>
        /// 公共服务
        /// </summary>
        [Display(Name = "公共服务")]
        Type3,

        /// <summary>
        /// 党务部门
        /// </summary>
        [Display(Name = "党务部门")]
        Type4,

        /// <summary>
        /// 行政单位
        /// </summary>
        [Display(Name = "行政单位")]
        Type5,

        /// <summary>
        /// 附(直)属单位
        /// </summary>
        [Display(Name = "附(直)属单位")]
        Type6,

        /// <summary>
        /// 后勤部门
        /// </summary>
        [Display(Name = "后勤部门")]
        Type7,

        /// <summary>
        /// 校办产业
        /// </summary>
        [Display(Name = "校办产业")]
        Type8,

        /// <summary>
        /// 其它
        /// </summary>
        [Display(Name = "其它")]
        Type9,
    }

    /// <summary>
    /// 部门附加数据类型
    /// </summary>
    public enum DepartmentAdditionType
    {
        /// <summary>
        /// 规模数据
        /// </summary>
        /// <remarks>教学院系与行政部门不同</remarks>
        ScaleData = 1
    }
}
