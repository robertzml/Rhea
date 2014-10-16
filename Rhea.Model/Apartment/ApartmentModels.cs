using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Apartment
{
    /// <summary>
    /// 居住类型
    /// </summary>
    public enum ResideType
    {
        /// <summary>
        /// 可分配
        /// </summary>
        [Display(Name = "可分配")]
        Available = 0,

        /// <summary>
        /// 正常居住
        /// </summary>
        [Display(Name = "正常居住")]
        Normal = 1,

        /// <summary>
        /// 挂职居住
        /// </summary>
        [Display(Name = "挂职居住")]
        Guest = 2,

        /// <summary>
        /// 部门占用
        /// </summary>
        [Display(Name = "部门占用")]
        Department = 3,

        /// <summary>
        /// 仓库
        /// </summary>
        [Display(Name = "仓库")]
        Warehouse = 4,

        /// <summary>
        /// 保留
        /// </summary>
        [Display(Name = "保留")]
        Reserved = 5
    }

    /// <summary>
    /// 住户类型
    /// </summary>
    //public enum InhabitantType
    //{
    //    /// <summary>
    //    /// 教职工
    //    /// </summary>
    //    [Display(Name = "教职工")]
    //    Teacher = 1,

    //    /// <summary>
    //    /// 外聘人员
    //    /// </summary>
    //    [Display(Name = "外聘人员")]
    //    External = 2,

    //    /// <summary>
    //    /// 挂职
    //    /// </summary>
    //    [Display(Name = "挂职")]
    //    Guest = 3,

    //    /// <summary>
    //    /// 学生
    //    /// </summary>
    //    [Display(Name = "学生")]
    //    Student = 4,

    //    /// <summary>
    //    /// 后勤职工
    //    /// </summary>
    //    [Display(Name = "后勤职工")]
    //    Logistics = 5,

    //    /// <summary>
    //    /// 其他
    //    /// </summary>
    //    [Display(Name = "其他")]
    //    Other = 6
    //}
}
