using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Energy
{
    /// <summary>
    /// 小时用电模型
    /// </summary>
    public class HourElectric
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        [Display(Name = "房间ID")]
        public int RoomId { get; set; }

        /// <summary>
        /// 电表ID
        /// </summary>
        [Display(Name = "电表ID")]
        public long NodeId { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [Display(Name = "记录时间")]
        public DateTime HourDate { get; set; }

        /// <summary>
        /// 记录值
        /// </summary>
        [Display(Name = "记录值")]
        public decimal HourDataValue { get; set; }

        /// <summary>
        /// 开始记录时间
        /// </summary>
        [Display(Name = "开始记录时间")]
        public DateTime StartHourDate { get; set; }

        /// <summary>
        /// 结束记录时间
        /// </summary>
        [Display(Name = "结束记录时间")]
        public DateTime EndHourDate { get; set; }

        /// <summary>
        /// 开始记录值
        /// </summary>
        [Display(Name = "开始记录值")]
        public decimal StartHourData { get; set; }

        /// <summary>
        /// 结束记录值
        /// </summary>
        [Display(Name = "结束记录值")]
        public decimal EndHourData { get; set; }
    }
}
