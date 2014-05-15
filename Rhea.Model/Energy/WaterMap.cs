using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rhea.Model.Energy
{
    /// <summary>
    /// 水资源数据映射
    /// </summary>
    /// <remarks>
    /// 对应FrontView CWMS数据库，主要将Building 和 NodeId映射。
    /// </remarks>
    public class WaterMap
    {
        /// <summary>
        /// 楼宇ID
        /// </summary>
        [Display(Name = "楼宇ID")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 水系统水表ID
        /// </summary>
        [Display(Name = "水系统水表ID")]
        public long NodeId { get; set; }

        /// <summary>
        /// 水表节点标识
        /// </summary>
        [Display(Name = "水表节点标识")]
        public string szOPCNode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}
