using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Data.Entities
{
    public class Building
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Display(Name = "名称" )]
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 所属楼群ID
        /// </summary>
        [Required]
        [Display(Name = "所属楼群")]
        [UIHint("BuildingGroupDropDownList")]
        public int BuildingGroupId { get; set; }


        /// <summary>
        /// 建筑面积
        /// </summary>        
        [Range(0.0, double.MaxValue)]
        [Display(Name = "建筑面积")]
        public double? BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>        
        [Range(0.0, double.MaxValue)]
        [Display(Name = "使用面积")]
        public double? UsableArea { get; set; }

        /// <summary>
        /// 地上楼层数
        /// </summary>
        [Range(0, int.MaxValue)]
        [Display(Name = "地上楼层数")]
        public int? AboveGroundFloor { get; set; }

        /// <summary>
        /// 地下楼层数
        /// </summary>
        [Range(0, int.MaxValue)]
        [Display(Name = "地下楼层数")]
        public int? UnderGroundFloor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; } 

        public List<Floor> Floors { get; set; }
    }

    public class Floor
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public double UsableArea { get; set; }
    }
}
