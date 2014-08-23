using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model
{
    /// <summary>
    /// 日志类
    /// </summary>
    [CollectionName("log")]
    public class Log : MongoEntity
    {
        #region Property
        /// <summary>
        /// 标题
        /// </summary>
        [BsonElement("title")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataType(DataType.MultilineText)]
        [BsonElement("content")]
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("time")]
        [Display(Name = "记录时间")]
        public DateTime Time { get; set; }

        /// <summary>
        /// 编辑人ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        [Display(Name = "编辑人ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [BsonElement("userName")]
        [Display(Name = "编辑人")]
        public string UserName { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [BsonElement("type")]
        [Display(Name = "日志类型")]
        public int Type { get; set; }

        /// <summary>
        /// 附属标记
        /// </summary>
        [BsonElement("tag")]
        [Display(Name = "附属标记")]
        public string Tag { get; set; }

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
