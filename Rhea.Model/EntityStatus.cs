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
        /// 0:正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 1:已删除
        /// </summary>
        [Display(Name = "已删除")]
        Deleted = 1,

        /// <summary>
        /// 用户类
        /// 11:已禁用
        /// </summary>
        [Display(Name = "已禁用")]
        UserDisable = 11,

        /// <summary>
        /// 居住记录类
        /// 50:超期
        /// </summary>
        [Display(Name = "超期")]
        OverTime = 50,

        /// <summary>
        /// 居住记录类
        /// 51:延期
        /// </summary>
        [Display(Name = "延期")]
        ExtendTime = 51,

        /// <summary>
        /// 居住记录类
        /// 52:已搬出
        /// </summary>
        [Display(Name = "已搬出")]
        MoveOut = 52,

        /// <summary>
        /// 居住记录类
        /// 53:已延期，记录关闭
        /// </summary>
        [Display(Name = "已延期")]
        ExtendOut = 53,

        /// <summary>
        /// 居住记录类
        /// 54:已换房
        /// </summary>
        [Display(Name = "已换房")]
        ExchangeOut = 54,

        /// <summary>
        /// 住户类
        /// 55:已搬出
        /// </summary>
        [Display(Name = "已搬出")]
        InhabitantMoveOut = 55,

        /// <summary>
        /// 住户类
        /// 56:延期居住
        /// </summary>
        [Display(Name = "延期居住")]
        InhabitantExtend = 56,

        /// <summary>
        /// 住户类
        /// 57:超期居住
        /// </summary>
        [Display(Name = "超期居住")]
        InhabitantExpire = 57
    }
 
}
