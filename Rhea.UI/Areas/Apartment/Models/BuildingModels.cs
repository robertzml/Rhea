using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rhea.Model.Apartment;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Apartment.Models
{
    /// <summary>
    /// 青教楼宇模型
    /// </summary>
    public class BlockModel
    {
        /// <summary>
        /// 楼宇信息
        /// </summary>
        [Display(Name = "楼宇信息")]
        public Block Block { get; set; }

        /// <summary>
        /// 房间列表
        /// </summary>
        [Display(Name = "房间列表")]
        public List<ApartmentRoom> Rooms { get; set; }

        /// <summary>
        /// 总使用面积
        /// </summary>
        [Display(Name = "总面积")]
        public double TotalArea { get; set; }
    }

    /// <summary>
    /// 青教楼层模型
    /// </summary>
    public class FloorModel
    {
        /// <summary>
        /// 父级楼宇
        /// </summary>
        public Block Parent { get; set; }

        /// <summary>
        /// 楼层信息
        /// </summary>
        [Display(Name = "楼层信息")]
        public Floor Floor { get; set; }

        /// <summary>
        /// 平面图路径
        /// </summary>
        [Display(Name = "平面图")]
        public string SvgPath { get; set; }

        /// <summary>
        /// 房间列表
        /// </summary>
        [Display(Name = "房间列表")]
        public List<ApartmentRoom> Rooms { get; set; }

    }
}