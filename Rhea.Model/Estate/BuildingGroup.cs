using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 楼群类
    /// </summary>
    [CollectionName("buildingGroup")]
    public class BuildingGroup : MongoEntity
    {
        #region Constructor
        public BuildingGroup()
        {
            this.Log = new Log();
        }
        #endregion //Constructor

        #region Database Property
        /// <summary>
        /// 楼群ID
        /// </summary>
        [BsonElement("id")]
        public int BuildingGroupId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 局部平面图
        /// </summary>
        [Display(Name = "局部导航")]
        public string PartMapUrl { get; set; }

        /// <summary>
        /// 所属校区ID
        /// </summary>
        [UIHint("CampusDropDownList")]
        [Display(Name = "所属校区")]
        public int CampusId { get; set; }

        /// <summary>
        /// 楼宇栋数
        /// </summary>
        [Range(1, int.MaxValue)]
        [Display(Name = "楼宇栋数")]
        public int? BuildingCount { get; set; }

        /// <summary>
        /// 面积系数
        /// </summary>
        [Display(Name = "面积系数")]
        public string AreaCoeffcient { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        [Range(0.0, double.MaxValue)]
        [Display(Name = "建筑面积")]
        public double? BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        [Range(0.0, double.MaxValue)]
        [Display(Name = "使用面积")]
        public double? UsableArea { get; set; }

        /// <summary>
        /// 占地面积
        /// </summary>
        [Range(0.0, double.MaxValue)]
        [Display(Name = "占地面积")]
        public double? Floorage { get; set; }

        /// <summary>
        /// 建造方式
        /// </summary>
        [UIHint("BuildType")]
        [Display(Name = "建造方式")]
        public string BuildType { get; set; }

        /// <summary>
        /// 建筑结构
        /// </summary>
        [UIHint("BuildStructure")]
        [Display(Name = "建筑结构")]
        public string BuildStructure { get; set; }

        /// <summary>
        /// 建筑物造价
        /// </summary>
        [Range(0.0, double.MaxValue)]
        [Display(Name = "建筑物造价")]
        public double? BuildCost { get; set; }

        /// <summary>
        /// 折旧后现值
        /// </summary>
        [Display(Name = "折旧后现值")]
        public double? CurrentValue { get; set; }

        /// <summary>
        /// 建筑物产别
        /// </summary>
        [Display(Name = "建筑物产别")]
        public string Classified { get; set; }

        /// <summary>
        /// 建筑物经费科目
        /// </summary>
        [UIHint("FundsSubject")]
        [Display(Name = "建筑物经费科目")]
        public string FundsSubject { get; set; }

        /// <summary>
        /// 建筑物产权证号
        /// </summary>
        [Display(Name = "建筑物产权证号")]
        public string EquityNumber { get; set; }

        /// <summary>
        /// 建成日期
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "建成日期")]
        public DateTime? BuildDate { get; set; }

        /// <summary>
        /// 使用年限
        /// </summary>
        [Range(0, int.MaxValue)]
        [Display(Name = "使用年限")]
        public int? FixedYear { get; set; }

        /// <summary>
        /// 建筑设计单位
        /// </summary>
        [Display(Name = "建筑设计单位")]
        public string DesignCompany { get; set; }

        /// <summary>
        /// 建筑物施工单位
        /// </summary>
        [Display(Name = "建筑物施工单位")]
        public string ConstructCompany { get; set; }

        /// <summary>
        /// 建筑物房管形式
        /// </summary>
        [UIHint("ManageType")]
        [Display(Name = "建筑物房管形式")]
        public string ManageType { get; set; }

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
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 图片展示
        /// </summary>
        [Display(Name = "图片展示")]
        public string[] Gallery { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 日志属性
        /// </summary>
        [Display(Name = "日志属性")]
        public Log Log { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Database Property
    }
}
