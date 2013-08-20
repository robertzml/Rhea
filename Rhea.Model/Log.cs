using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rhea.Model
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 日志系统ID
        /// </summary>
        [BsonId]
        [Display(Name = "日志系统ID")]
        public ObjectId _id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [Display(Name = "记录时间")]
        public DateTime Time { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public ObjectId UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Display(Name = "日志类型")]
        public int Type { get; set; }

        /// <summary>
        /// 相关时间
        /// </summary>
        [Display(Name = "相关时间")]
        public DateTime? RelateTime { get; set; }
    }
}
