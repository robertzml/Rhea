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
    /// 居住记录类
    /// </summary>
    [CollectionName("resideRecord")]
    public class ResideRecord : MongoEntity
    {
        #region Property
        /// <summary>
        /// 房间ID
        /// </summary>
        [BsonElement("roomId")]
        [Display(Name = "房间ID")]
        public int RoomId { get; set; }

        /// <summary>
        /// 居住人ID
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("inhabitantId")]
        [Display(Name = "居住人ID")]
        public string InhabitantId { get; set; }

        /// <summary>
        /// 居住人姓名
        /// </summary>
        [BsonElement("inhabitantName")]
        [Display(Name = "居住人姓名")]
        public string InhabitantName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [BsonElement("inhabitantDepartment")]
        [Display(Name = "部门")]
        public string InhabitantDepartment { get; set; }
        
        /// <summary>
        /// 居住状态
        /// </summary>
        /// <remarks>
        /// 0:可分配；1:正常居住；2:挂职居住；3:部门占用；4:仓库；5:保留
        /// </remarks>
        [BsonElement("resideType")]
        [Display(Name = "居住状态")]
        public int ResideType { get; set; }

        /// <summary>
        /// 房租
        /// </summary>
        [BsonElement("rent")]
        [Display(Name = "房租")]
        public decimal Rent { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        [BsonDateTimeOptions(DateOnly = true)]
        [BsonElement("enterDate")]
        [Display(Name = "入住时间")]
        public DateTime? EnterDate { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        [BsonDateTimeOptions(DateOnly = true)]
        [BsonElement("expireDate")]
        [Display(Name = "到期时间")]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 年限
        /// </summary>
        [BsonElement("termLimit")]
        [Display(Name = "年限")]
        public string TermLimit { get; set; }

        /// <summary>
        /// 离开时间
        /// </summary>
        [BsonDateTimeOptions(DateOnly = true)]
        [BsonElement("leaveDate")]
        [Display(Name = "离开时间")]
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 蠡湖家园入住情况
        /// </summary>
        [BsonElement("liHuStatus")]
        [Display(Name = "蠡湖家园入住情况")]
        public string LiHuStatus { get; set; }

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
