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
        #region Constructor
        /// <summary>
        /// 居住记录类
        /// </summary>
        public ResideRecord()
        {
            this.Log = new Log();
        }
        #endregion //Constructor

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
        /// 居住类型
        /// </summary>
        /// <remarks>
        /// 0:可分配；1:正常居住；2:挂职居住；3:部门占用；4:仓库；5:保留
        /// 仅1,2类型有住户对象
        /// </remarks>
        [UIHint("ResideType")]
        [BsonElement("resideType")]
        [Display(Name = "居住类型")]
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
        [DataType(DataType.Date)]
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
        [BsonElement("enterDate")]
        [Display(Name = "入住时间")]
        public DateTime? EnterDate { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [DataType(DataType.Date)]
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
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
        /// 月数
        /// </summary>
        [BsonElement("monthCount")]
        [Display(Name = "月数")]
        public int MonthCount { get; set; }

        /// <summary>
        /// 离开时间
        /// </summary>
        [DataType(DataType.Date)]
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
        [BsonElement("leaveDate")]
        [Display(Name = "离开时间")]
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("registerTime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "登记时间")]
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 财务收据号码
        /// </summary>
        [BsonElement("receiptNumber")]
        [Display(Name = "财务收据号码")]
        public string ReceiptNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 0:居住中； 1:已删除； 50:超期; 51:延期; 52:已搬出; 53:已延期
        /// </remarks>
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
