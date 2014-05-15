using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Energy
{
    /// <summary>
    /// 小时用水模型
    /// </summary>
    public class HourWater
    {
        /// <summary>
        /// 楼宇ID
        /// </summary>
        [Display(Name = "楼宇ID")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 水表ID
        /// </summary>
        [Display(Name = "水表ID")]
        public long NodeId { get; set; }

        /// <summary>
        /// 读取时间
        /// </summary>
        [Display(Name = "读取时间")]
        public DateTime ReadingDate { get; set; }

        /// <summary>
        /// 水表示数
        /// </summary>
        [Display(Name = "水表示数")]
        public decimal AmmeterData { get; set; }

        /// <summary>
        /// 小时用水量
        /// </summary>
        [Display(Name = "小时用水量")]
        public decimal HourValue { get; set; }
    }
}
