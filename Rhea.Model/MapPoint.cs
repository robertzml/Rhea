using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Rhea.Model
{
    /// <summary>
    /// 地图标记点模型
    /// </summary>
    public class MapPoint
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Display(Name = "系统ID")]
        public string _id { get; set; }

        /// <summary>
        /// 目标ID
        /// </summary>
        [Required]
        [Display(Name = "目标ID")]
        public int TargetId { get; set; }

        /// <summary>
        /// 目标类型
        /// 1:楼群, 2:学院
        /// </summary>
        [Required]
        [Display(Name = "目标类型")]
        public int TargetType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        [Required]
        [Display(Name = "X坐标")]
        public double PointX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        [Required]
        [Display(Name = "Y坐标")]
        public double PointY { get; set; }

        /// <summary>
        /// 缩放级别
        /// </summary>
        [Required]
        [Display(Name = "缩放级别")]
        public int Zoom { get; set; }

        /// <summary>
        /// Pin标记
        /// </summary>
        [Required]
        [Display(Name = "Pin标记")]
        public string Pin { get; set; }

        public string Symbol { get; set; }
    }
}
