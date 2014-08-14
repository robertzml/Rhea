using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model
{
    /// <summary>
    /// 实体类 Stauts 属性
    /// </summary>
    public enum EntityStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 已删除
        /// </summary>
        [Display(Name = "已删除")]
        Deleted = 1,

        /// <summary>
        /// 用户类
        /// 已禁用
        /// </summary>
        [Display(Name = "已禁用")]
        UserDisable = 2,

        /// <summary>
        /// 居住记录类
        /// 超期
        /// </summary>
        [Display(Name = "超期")]
        OverTime = 50,

        /// <summary>
        /// 居住记录类
        /// 已搬出
        /// </summary>
        [Display(Name = "已搬出")]
        MoveOut = 51,

        /// <summary>
        /// 住户类
        /// 已搬出
        /// </summary>
        [Display(Name = "已搬出")]
        InhabitantMoveOut = 55,

        /// <summary>
        /// 住户类
        /// 延期居住
        /// </summary>
        [Display(Name = "延期居住")]
        InhabitantExtend = 56
    }
 
}
