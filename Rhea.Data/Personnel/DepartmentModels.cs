using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Data.Personnel
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public enum DepartmentType
    {
        /// <summary>
        /// 教学院系
        /// </summary>
        [Display(Name = "教学院系")]
        Type1 = 1,

        /// <summary>
        /// 科研机构
        /// </summary>
        [Display(Name = "科研机构")]
        Type2,

        /// <summary>
        /// 公共服务
        /// </summary>
        [Display(Name = "公共服务")]
        Type3,

        /// <summary>
        /// 党务部门
        /// </summary>
        [Display(Name = "党务部门")]
        Type4,

        /// <summary>
        /// 行政单位
        /// </summary>
        [Display(Name = "行政单位")]
        Type5,

        /// <summary>
        /// 附(直)属单位
        /// </summary>
        [Display(Name = "附(直)属单位")]
        Type6,

        /// <summary>
        /// 后勤部门
        /// </summary>
        [Display(Name = "后勤部门")]
        Type7,

        /// <summary>
        /// 校办产业
        /// </summary>
        [Display(Name = "校办产业")]
        Type8,

        /// <summary>
        /// 其它
        /// </summary>
        [Display(Name = "其它")]
        Type9,
    }

    /// <summary>
    /// 部门附加数据类型
    /// </summary>
    public enum DepartmentAdditionType
    {
        /// <summary>
        /// 规模数据
        /// </summary>
        /// <remarks>教学院系与行政部门不同</remarks>
        ScaleData = 1,

        /// <summary>
        /// 科研数据
        /// </summary>
        ResearchData = 2,

        /// <summary>
        /// 特殊面积数据
        /// </summary>
        SpecialAreaData = 4
    }

    /// <summary>
    /// 部门指标模型
    /// </summary>
    public class DepartmentIndicatorModel
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 办公用房
        /// </summary>
        [Display(Name = "办公用房")]
        public double OfficeArea { get; set; }

        /// <summary>
        /// 机动用房
        /// </summary>
        [Display(Name = "机动用房")]
        public double FlexibleArea { get; set; }

        /// <summary>
        /// 实验用房
        /// </summary>
        [Display(Name = "实验用房")]
        public double ExperimentArea { get; set; }

        /// <summary>
        /// 研究生用房
        /// </summary>
        [Display(Name = "研究生用房")]
        public double GraduateArea { get; set; }

        /// <summary>
        /// 博士生用房
        /// </summary>
        [Display(Name = "博士生用房")]
        public double DoctorArea { get; set; }

        /// <summary>
        /// 工程硕士用房
        /// </summary>
        [Display(Name = "工程硕士用房")]
        public double MasterOfEngineerArea { get; set; }

        /// <summary>
        /// 科研用房
        /// </summary>
        [Display(Name = "科研用房")]
        public double ResearchArea { get; set; }

        /// <summary>
        /// 对公用房
        /// </summary>
        [Display(Name = "对公用房")]
        public double PublicArea { get; set; }

        /// <summary>
        /// 教学补贴用房
        /// </summary>
        [Display(Name = "教学补贴用房")]
        public double EducationBonusArea { get; set; }

        /// <summary>
        /// 特殊人才用房面积
        /// </summary>
        [Display(Name = "特殊人才用房面积")]
        public double TalentArea { get; set; }

        /// <summary>
        /// 科研平台补贴面积
        /// </summary>
        [Display(Name = "科研平台补贴面积")]
        public double ResearchBonusArea { get; set; }

        /// <summary>
        /// 实验教学平台补贴面积
        /// </summary>
        [Display(Name = "实验教学平台补贴面积")]
        public double ExperimentBonusArea { get; set; }

        /// <summary>
        /// 调整面积
        /// </summary>
        [Display(Name = "调整面积")]
        public double AdjustArea { get; set; }

        /// <summary>
        /// 应有面积
        /// </summary>
        [Display(Name = "应有面积")]
        public double DeservedArea { get; set; }

        /// <summary>
        /// 现有面积
        /// </summary>
        [Display(Name = "现有面积")]
        public double ExistingArea { get; set; }

        /// <summary>
        /// 超标比值
        /// </summary>
        [Display(Name = "超标比值")]
        public double Overproof { get; set; }
    }
}
