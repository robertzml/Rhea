using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Data.Entities
{
    /// <summary>
    /// 楼群模型
    /// </summary>    
    public class BuildingGroup
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>        
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

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
        /// 备注
        /// </summary>        
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态" )]
        public int Status { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        /// <remarks>
        /// 1:学院楼宇,2:教学楼宇,3:行政办公楼宇,4:宿舍楼宇
        /// </remarks>
        [Display(Name = "类型" )]
        public int Type { get; set; }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        public List<Building> Buildings { get; set; }
    }
}
