using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 楼层模型
    /// </summary>
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public class Floor
    {
        /// <summary>
        /// 楼层ID
        /// </summary>
        [Required]
        [BsonElement("id")]
        [Display(Name = "楼层ID")]
        public int FloorId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [BsonElement("number")]
        [Display(Name = "编号")]
        public int Number { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>     
        [BsonElement("buildArea")]
        [Display(Name = "建筑面积")]
        public double? BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        [BsonElement("usableArea")]
        [Display(Name = "使用面积")]
        public double? UsableArea { get; set; }

        /// <summary>
        /// 楼层平面图
        /// </summary>
        [BsonElement("imageUrl")]
        [Display(Name = "楼层平面图")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}
