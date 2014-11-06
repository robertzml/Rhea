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
    /// 入住办理业务记录类
    /// </summary>
    public class CheckInTransaction : ApartmentTransaction
    {
        #region Property
        /// <summary>
        /// 房间
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
        /// 居住记录ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("resideRecordId")]
        [Display(Name = "居住记录ID")]
        public string ResideRecordId { get; set; }
        #endregion //Property
    }
}
