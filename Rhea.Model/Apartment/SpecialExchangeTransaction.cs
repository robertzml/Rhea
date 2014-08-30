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
    /// 特殊换房业务
    /// </summary>
    public class SpecialExchangeTransaction : ApartmentTransaction
    {
        #region Property
        /// <summary>
        /// 原房间ID
        /// </summary>
        [BsonElement("oldRoomId")]
        [Display(Name = "原房间ID")]
        public int OldRoomId { get; set; }

        /// <summary>
        /// 原房间名称
        /// </summary>
        [BsonElement("oldRoomName")]
        [Display(Name = "原房间名称")]
        public string OldRoomName { get; set; }

        /// <summary>
        /// 新房间ID
        /// </summary>
        [BsonElement("newRoomId")]
        [Display(Name = "新房间ID")]
        public int NewRoomId { get; set; }

        /// <summary>
        /// 新房间名称
        /// </summary>
        [BsonElement("newRoomName")]
        [Display(Name = "新房间名称")]
        public string NewRoomName { get; set; }

        /// <summary>
        /// 居住人ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("inhabitantId")]
        [Display(Name = "居住人ID")]
        public string InhabitantId { get; set; }

        /// <summary>
        /// 住户姓名
        /// </summary>
        [BsonElement("inhabitantName")]
        [Display(Name = "住户姓名")]
        public string InhabitantName { get; set; }

        /// <summary>
        /// 居住记录ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("resideRecordId")]
        [Display(Name = "居住记录ID")]
        public string ResideRecordId { get; set; }
        #endregion //Property
    }
}
