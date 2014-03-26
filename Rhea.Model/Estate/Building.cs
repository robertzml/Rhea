using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 楼宇模型
    /// </summary>
    [CollectionName("building")]
    public class Building : MongoEntity
    {
        #region Constructor
        public Building()
        {
            this.Floors = new List<Floor>();
            this.Log = new Log();
        }
        #endregion //Constructor

        #region Property
        /// <summary>
        /// 楼宇ID
        /// </summary>
        [BsonElement("id")]
        [Display(Name = "楼宇ID")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 所属楼群ID
        /// </summary>
        [Required]
        [BsonElement("buildingGroupId")]
        [Display(Name = "所属楼群")]
        [UIHint("BuildingGroupDropDownList")]
        public int BuildingGroupId { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>        
        [BsonElement("buildArea")]
        [Range(0.0, double.MaxValue)]
        [Display(Name = "建筑面积")]
        public double? BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>        
        [BsonElement("usableArea")]
        [Range(0.0, double.MaxValue)]
        [Display(Name = "使用面积")]
        public double? UsableArea { get; set; }

        /// <summary>
        /// 地上楼层数
        /// </summary>
        [BsonElement("aboveGroundFloor")]
        [Range(0, int.MaxValue)]
        [Display(Name = "地上楼层数")]
        public int? AboveGroundFloor { get; set; }

        /// <summary>
        /// 地下楼层数
        /// </summary>
        [BsonElement("underGroundFloor")]
        [Range(0, int.MaxValue)]
        [Display(Name = "地下楼层数")]
        public int? UnderGroundFloor { get; set; }

        /// <summary>
        /// 使用类型
        /// </summary>
        /// <remarks>
        /// 1:学院楼宇,2:教学楼宇,3:行政办公,4:宿舍楼宇,5:辅助楼宇
        /// </remarks>
        [Required]
        [UIHint("BuildingUseType")]
        [Display(Name = "使用类型")]
        public int UseType { get; set; }

        /// <summary>
        /// 局部平面图
        /// </summary>
        [Display(Name = "局部导航")]
        public string PartMapUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 日志属性
        /// </summary>
        [BsonElement("log")]
        [Display(Name = "日志属性")]
        public Log Log { get; set; }

        /// <summary>
        /// 楼层列表
        /// </summary>
        [BsonElement("floors")]
        public List<Floor> Floors { get; set; }
        #endregion //Property
    }

    /// <summary>
    /// 楼层模型
    /// </summary>
    public class Floor : MongoEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [BsonElement("id")]
        public int FloorId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [BsonElement("number")]
        [Display(Name = "编号")]
        public int Number { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>     
        [BsonElement("buildArea")]
        [Display(Name = "建筑面积")]
        public double? BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        [Display(Name = "使用面积")]
        public double? UsableArea { get; set; }

        /// <summary>
        /// 楼层平面图
        /// </summary>
        [Display(Name = "楼层平面图")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}
