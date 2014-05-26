using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model
{
    /// <summary>
    /// 字典类
    /// </summary>
    [CollectionName("dictionary")]
    public class Dictionary : MongoEntity
    {
        #region Property
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [BsonElement("title")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 属性集
        /// </summary>
        [BsonElement("property")]
        [Display(Name = "属性集")]
        public string[] Property { get; set; }

        /// <summary>
        /// 是否组合
        /// </summary>
        [BsonElement("isCombined")]
        [Display(Name = "是否组合")]
        public bool IsCombined { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }
        #endregion //Property
    }
}
