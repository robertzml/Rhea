using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 房间朝向
    /// </summary>
    public enum Orientation
    {
        北 = 1,

        南,

        西,

        东,

        中,

        东北,

        东南,

        西北,

        西南
    }

    /// <summary>
    /// 房间状态
    /// </summary>
    public enum RoomStatus
    {
        /// <summary>
        /// 闲置
        /// </summary>
        [Display(Name = "闲置")]
        Unused = 1,

        /// <summary>
        /// 在用
        /// </summary>
        [Display(Name = "在用")]
        Using = 2,

        /// <summary>
        /// 报废
        /// </summary>
        [Display(Name = "报废")]
        Discard = 3,

        /// <summary>
        /// 有偿转让
        /// </summary>
        [Display(Name = "有偿转让")]
        Transfer = 4,

        /// <summary>
        /// 无偿调出
        /// </summary>
        [Display(Name = "无偿调出")]
        Callout = 5,


        /// <summary>
        /// 出租
        /// </summary>
        [Display(Name = "出租")]
        Rent = 6,

        /// <summary>
        /// 其它
        /// </summary>
        [Display(Name = "其它")]
        Other = 7
    }

    /// <summary>
    /// 空调情况
    /// </summary>
    public enum AirCondition
    {
        无空调 = 1,

        室内空调,

        室内中央空调,

        中央空调
    }

    /// <summary>
    /// 消防情况
    /// </summary>
    public enum FireControl
    {
        灭火器 = 1,

        统一监控,

        其它
    }

    /// <summary>
    /// 是否有废液处理
    /// </summary>
    public enum TrashHandle
    {
        无,

        有但无固定位置,

        有且有固定位置
    }
}