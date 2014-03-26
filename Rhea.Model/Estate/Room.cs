﻿using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 房间模型
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
        public double? UsableArea { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        [Required]        
        [Display(Name = "功能编码")]
        [UIHint("FunctionCodeDropDownList")]
        public RoomFunctionCode Function { get; set; }

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
        [Display(Name = "所属部门")]
        [UIHint("DepartmentDropDownList")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 开始使用日期
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "开始使用日期")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 使用期限
        /// </summary>
        [Range(0, int.MaxValue)]
        [Display(Name = "使用期限")]
        public int? FixedYear { get; set; }

        /// <summary>
        /// 房间总人数
        /// </summary>
        [Display(Name = "房间总人数")]
        public int? PersonNumber { get; set; }

        /// <summary>
        /// 管理人
        /// </summary>
        [Display(Name = "管理人")]
        public string Manager { get; set; }

        /// <summary>
        /// 房间状态
        /// </summary>
        /// <remarks>
        /// 0:未知, 1:在用, 2:闲置, 3:报废, 4:有偿转让, 5:无偿调出, 6:出租, 7:借出, 8:其他
        /// </remarks>
        [Display(Name = "房间状态")]
        [UIHint("RoomStatus")]
        public string RoomStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 日志属性
        /// </summary>
        [BsonElement("log")]
        [Display(Name = "日志属性")]
        public Log Log { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 0:正常 1:已删除 2:已合并 3:已拆分
        /// </remarks>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }

        #region Additional
        /// <summary>
        /// 供热情况
        /// </summary>
        [Display(Name = "供热情况")]
        public bool? Heating { get; set; }

        /// <summary>
        /// 消防情况
        /// </summary>
        [Display(Name = "消防情况")]
        [UIHint("FireControl")]
        public string FireControl { get; set; }

        /// <summary>
        /// 房间高度
        /// </summary>
        [Display(Name = "房间高度")]
        public double? Height { get; set; }

        /// <summary>
        /// 房间东西长度
        /// </summary>
        [Display(Name = "房间东西长度")]
        public double? EWWidth { get; set; }

        /// <summary>
        /// 房间南北长度
        /// </summary>
        [Display(Name = "房间南北长度")]
        public double? SNWidth { get; set; }

        /// <summary>
        /// 国际分类编号
        /// </summary>
        [Display(Name = "国际分类编号")]
        public int? InternationalId { get; set; }

        /// <summary>
        /// 教育部分类编号
        /// </summary>
        [Display(Name = "教育部分类编号")]
        public int? EducationId { get; set; }

        /// <summary>
        /// 供电情况
        /// </summary>
        [Display(Name = "供电情况")]
        [UIHint("PowerSupply")]
        public string PowerSupply { get; set; }

        /// <summary>
        /// 空调情况
        /// </summary>
        [Display(Name = "空调情况")]
        [UIHint("AirCondition")]
        public string AirCondition { get; set; }

        /// <summary>
        /// 是否有安全制度
        /// </summary>
        [Display(Name = "是否有安全制度")]
        public bool? HasSecurity { get; set; }

        /// <summary>
        /// 是否有危险化学品
        /// </summary>
        [Display(Name = "是否有危险化学品")]
        public bool? HasChemical { get; set; }

        /// <summary>
        /// 是否有废液处理
        /// </summary>
        [Display(Name = "是否有废液处理")]
        [UIHint("TrashHandle")]
        public string HasTrash { get; set; }

        /// <summary>
        /// 是否有安全教育检查
        /// </summary>
        [Display(Name = "是否有安全教育检查")]
        public bool? HasSecurityCheck { get; set; }

        /// <summary>
        /// 压力容器数量
        /// </summary>
        [Display(Name = "压力容器数量")]
        public int? PressureContainer { get; set; }

        /// <summary>
        /// 钢瓶数量
        /// </summary>
        [Display(Name = "钢瓶数量")]
        public int? Cylinder { get; set; }

        /// <summary>
        /// 通风是否有取暖
        /// </summary>
        [Display(Name = "通风是否有取暖")]
        public bool? HeatingInAeration { get; set; }

        /// <summary>
        /// 是否有试验台
        /// </summary>
        [Display(Name = "是否有试验台")]
        public bool? HasTestBed { get; set; }

        /// <summary>
        /// 使用费用
        /// </summary>
        [Display(Name = "使用费用")]
        public double? UsageCharge { get; set; }
        #endregion //Additional
        #endregion //Property
    }
}
