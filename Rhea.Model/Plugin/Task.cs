using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Plugin
{
    /// <summary>
    /// 任务类
    /// </summary>
    [CollectionName("task")]
    public class Task : MongoEntity
    {
        #region Property
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [BsonElement("title")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [BsonElement("content")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createTime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        [Display(Name = "用户")]
        public string UserId { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
        [BsonElement("remindTime")]
        [Display(Name = "提醒时间")]
        public DateTime RemindTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Property
    }
}
