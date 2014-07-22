using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Personnel
{
    [CollectionName("department")]
    public class Department : MongoEntity
    {
        #region Property
        /// <summary>
        /// 部门代码
        /// </summary>
        [BsonElement("id")]
        [Display(Name = "部门代码")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "部门名称")]
        public string Name { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [BsonElement("shortName")]
        [Display(Name = "简称")]
        public string ShortName { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        [Required]
        [UIHint("DepartmentType")]
        [BsonElement("type")]
        [Display(Name = "部门类型")]
        public int Type { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [BsonElement("imageUrl")]
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 标志
        /// </summary>
        [BsonElement("logoUrl")]
        [Display(Name = "标志")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// 部门局部地图
        /// </summary>
        [BsonElement("partMapUrl")]
        [Display(Name = "局部地图")]
        public string PartMapUrl { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [BsonElement("description")]
        [Display(Name = "介绍")]
        public string Description { get; set; }

        /// <summary>
        /// 状态，0:正常；1:删除
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Property
    }
}
