using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 建筑基类
    /// </summary>
    /// <remarks>
    /// 共29个属性
    /// </remarks>
    [CollectionName("building")]
    public class Building : MongoEntity
    {
        #region Property
        /// <summary>
        /// ID
        /// </summary>
        [BsonElement("id")]
        [Display(Name = "建筑ID")]
        public int BuildingId { get; set; }

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
        [BsonElement("imageUrl")]
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 局部导航
        /// </summary>
        [BsonElement("partMapUrl")]
        [Display(Name = "局部导航")]
        public string PartMapUrl { get; set; }

        /// <summary>
        /// 所属校区ID
        /// </summary>
        [UIHint("CampusDropDownList")]
        [BsonElement("campusId")]
        [Display(Name = "所属校区")]
        public int CampusId { get; set; }

        /// <summary>
        /// 建筑组织形式
        /// </summary>
        /// <remarks>
        /// 1:楼群,2:组团,3:独栋,4:分区,5:楼宇,6:操场
        /// </remarks>
        [BsonElement("organizeType")]
        [UIHint("BuildingOrganizeType")]
        [Display(Name = "建筑组织形式")]
        public int OrganizeType { get; set; }

        /// <summary>
        /// 父级建筑ID
        /// </summary>
        [BsonElement("parentId")]
        [UIHint("ParentBuildingList")]
        [Display(Name = "父级建筑")]
        public int ParentId { get; set; }

        /// <summary>
        /// 是否有子建筑
        /// </summary>
        [BsonElement("hasChild")]
        [Display(Name = "是否有子建筑")]
        public bool HasChild { get; set; }

        /// <summary>
        /// 使用类型
        /// </summary>
        /// <remarks>
        /// 1:学院楼宇,2:教学楼宇,3:行政办公,4:宿舍楼宇,5:辅助楼宇,6:基础设施,7:室外运动场
        /// </remarks>
        [Required]
        [BsonElement("useType")]
        [UIHint("BuildingUseType")]
        [Display(Name = "使用类型")]
        public int UseType { get; set; }

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
        /// 计划面积
        /// </summary>
        [BsonElement("planningArea")]
        [Range(0.0, double.MaxValue)]
        [Display(Name = "计划面积")]
        public double? PlanningArea { get; set; }

        /// <summary>
        /// 占地面积
        /// </summary>
        [BsonElement("floorage")]
        [Range(0.0, double.MaxValue)]
        [Display(Name = "占地面积")]
        public double? Floorage { get; set; }

        /// <summary>
        /// 面积系数
        /// </summary>
        [BsonElement("areaCoeffcient")]
        [Display(Name = "面积系数")]
        public string AreaCoeffcient { get; set; }

        /// <summary>
        /// 建造方式
        /// </summary>
        [BsonElement("buildType")]
        [UIHint("BuildType")]
        [Display(Name = "建造方式")]
        public string BuildType { get; set; }

        /// <summary>
        /// 建筑结构
        /// </summary>
        [BsonElement("buildStructure")]
        [UIHint("BuildStructure")]
        [Display(Name = "建筑结构")]
        public string BuildStructure { get; set; }

        /// <summary>
        /// 建筑物造价
        /// </summary>
        [BsonElement("buildCost")]
        [Range(0.0, double.MaxValue)]
        [Display(Name = "建筑物造价")]
        public double? BuildCost { get; set; }

        /// <summary>
        /// 折旧后现值
        /// </summary>
        [BsonElement("currentValue")]
        [Display(Name = "折旧后现值")]
        public double? CurrentValue { get; set; }

        /// <summary>
        /// 建筑物产别
        /// </summary>
        [BsonElement("classified")]
        [Display(Name = "建筑物产别")]
        public string Classified { get; set; }

        /// <summary>
        /// 建筑物经费科目
        /// </summary>
        [BsonElement("fundsSubject")]
        [UIHint("FundsSubject")]
        [Display(Name = "建筑物经费科目")]
        public string FundsSubject { get; set; }

        /// <summary>
        /// 建筑物产权证号
        /// </summary>
        [BsonElement("equityNumber")]
        [Display(Name = "建筑物产权证号")]
        public string EquityNumber { get; set; }

        /// <summary>
        /// 建成日期
        /// </summary>
        [BsonDateTimeOptions(DateOnly = true)]
        [BsonElement("buildDate")]
        [DataType(DataType.Date)]
        [Display(Name = "建成日期")]
        public DateTime? BuildDate { get; set; }

        /// <summary>
        /// 使用年限
        /// </summary>
        [BsonElement("fixedYear")]
        [Range(0, int.MaxValue)]
        [Display(Name = "使用年限")]
        public int? FixedYear { get; set; }

        /// <summary>
        /// 建筑设计单位
        /// </summary>
        [BsonElement("designCompany")]
        [Display(Name = "建筑设计单位")]
        public string DesignCompany { get; set; }

        /// <summary>
        /// 建筑物施工单位
        /// </summary>
        [BsonElement("constructCompany")]
        [Display(Name = "建筑物施工单位")]
        public string ConstructCompany { get; set; }

        /// <summary>
        /// 建筑物房管形式
        /// </summary>
        [BsonElement("manageType")]
        [UIHint("ManageType")]
        [Display(Name = "建筑物房管形式")]
        public string ManageType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [BsonElement("sort")]
        [Display(Name = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
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
