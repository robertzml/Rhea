using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Apartment
{
    /// <summary>
    /// 业务记录类
    /// </summary>
    [CollectionName("apartmentTransaction")]
    public class ApartmentTransaction : MongoEntity
    {
        #region Property
        /// <summary>
        /// 业务类型
        /// </summary>
        [Required]
        [BsonElement("type")]
        [Display(Name = "业务类型")]
        public int Type { get; set; }

        /// <summary>
        /// 办理时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("time")]
        [DataType(DataType.DateTime)]
        [Display(Name = "办理时间")]
        public DateTime Time { get; set; }

        /// <summary>
        /// 操作用户ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        [Display(Name = "操作用户ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 操作用户姓名
        /// </summary>
        [BsonElement("userName")]
        [Display(Name = "操作用户姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Property
    }
}
