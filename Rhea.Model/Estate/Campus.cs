using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 校区类
    /// </summary>
    [CollectionName("campus")]
    public class Campus : MongoEntity
    {
        #region Constructor
        /// <summary>
        /// 校区类
        /// </summary>
        public Campus()
        {
            this.Log = new Log();
        }
        #endregion //Constructor

        #region Property
        /// <summary>
        /// ID
        /// </summary>
        [BsonElement("id")]
        [Display(Name = "ID")]
        public int CampusId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [BsonElement("imageUrl")]
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// 0:在用; 1:删除
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 日志属性
        /// </summary>
        [BsonElement("log")]
        [Display(Name = "日志属性")]
        public Log Log { get; set; }
        #endregion //Property
    }
}
