using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Apartment.Models
{
    /// <summary>
    /// 青教楼层模型
    /// </summary>
    public class FloorModel
    {
        /// <summary>
        /// 楼宇ID
        /// </summary>
        [Display(Name = "楼宇ID")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        [Display(Name = "楼宇名称")]
        public string BuildingName { get; set; }

        /// <summary>
        /// 楼层信息
        /// </summary>
        [Display(Name = "楼层信息")]
        public Floor Floor { get; set; }

        /// <summary>
        /// 房间列表
        /// </summary>
        [Display(Name = "房间列表")]
        public List<Room> Rooms { get; set; }

    }
}