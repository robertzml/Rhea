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
    /// 退房办理业务记录类
    /// </summary>
    public class CheckOutTransaction : ApartmentTransaction
    {
        #region Property
        /// <summary>
        /// 房间ID
        /// </summary>
        [BsonElement("roomId")]
        [Display(Name = "房间ID")]
        public int RoomId { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        [BsonElement("roomName")]
        [Display(Name = "房间名称")]
        public string RoomName { get; set; }

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
