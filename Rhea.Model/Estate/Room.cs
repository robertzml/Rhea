using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 房间类
    /// </summary>
    [CollectionName("room")]
    public class Room : MongoEntity
    {
        #region Property
        /// <summary>
        /// 房间ID
        /// </summary>
        [BsonElement("id")]
        [Display(Name = "房间ID")]
        public int RoomId { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "房间名称")]
        public string Name { get; set; }

        /// <summary>
        /// 房间编号
        /// </summary>
        [Required]
        [BsonElement("number")]
        [Display(Name = "房间编号")]
        public string Number { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Required]
        [BsonElement("floor")]
        [Display(Name = "楼层")]
        public int Floor { get; set; }

        /// <summary>
        /// 跨数
        /// </summary>
        [BsonElement("span")]
        [Display(Name = "跨数")]
        public double? Span { get; set; }

        /// <summary>
        /// 朝向
        /// </summary>
        [BsonElement("orientation")]
        [Display(Name = "朝向")]
        [UIHint("Orientation")]
        public string Orientation { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>        
        [BsonElement("buildArea")]
        [Display(Name = "建筑面积")]
        public double? BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        [BsonElement("usableArea")]
        [Display(Name = "使用面积")]
        public double UsableArea { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [BsonElement("imageUrl")]
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        //[Required]
        //[Display(Name = "功能编码")]
        //[UIHint("FunctionCodeDropDownList")]
        //public RoomFunction Function { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        [Required]
        [BsonElement("buildingId")]
        [Display(Name = "所属楼宇")]
        [UIHint("BuildingDropDownList")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Required]
        [BsonElement("departmentId")]
        [Display(Name = "所属部门")]
        [UIHint("DepartmentDropDownList")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 开始使用日期
        /// </summary>
        [DataType(DataType.Date)]
        [BsonElement("startDate")]
        [Display(Name = "开始使用日期")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 使用期限
        /// </summary>
        [Range(0, int.MaxValue)]
        [BsonElement("fixedYear")]
        [Display(Name = "使用期限")]
        public int? FixedYear { get; set; }

        /// <summary>
        /// 房间总人数
        /// </summary>
        [BsonElement("personNumber")]
        [Display(Name = "房间总人数")]
        public int? PersonNumber { get; set; }

        /// <summary>
        /// 管理人
        /// </summary>
        [BsonElement("manager")]
        [Display(Name = "管理人")]
        public string Manager { get; set; }

        /// <summary>
        /// 房间状态
        /// </summary>
        /// <remarks>
        /// 0:未知, 1:在用, 2:闲置, 3:报废, 4:有偿转让, 5:无偿调出, 6:出租, 7:借出, 8:其他
        /// </remarks>
        [BsonElement("roomStatus")]
        [Display(Name = "房间状态")]
        [UIHint("RoomStatus")]
        public string RoomStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }     

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 0:正常 1:已删除 2:已合并 3:已拆分
        /// </remarks>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Property
    }
}
