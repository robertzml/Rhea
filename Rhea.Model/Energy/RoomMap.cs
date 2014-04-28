using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Energy
{
    /// <summary>
    /// 房间映射
    /// </summary>
    public class RoomMap
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        [Display(Name = "房间ID")]
        public int RoomId { get; set; }

        /// <summary>
        /// 电系统电表ID
        /// </summary>
        [Display(Name = "电系统电表ID")]
        public long NodeId { get; set; }

        /// <summary>
        /// 电表节点标识
        /// </summary>
        [Display(Name = "电表节点标识")]
        public string szOPCNode { get; set; }

        /// <summary>
        /// 倍率
        /// </summary>
        [Display(Name = "倍率")]
        public int Multiplying { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态" )]
        public int Status { get; set; }
    }
}
