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
}
