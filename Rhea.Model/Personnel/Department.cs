using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Personnel
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class Department
    {
        #region General Property
        /// <summary>
        /// 部门编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [Display(Name = "部门名称")]
        public string Name { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [Display(Name = "简称")]
        public string ShortName { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        [Required]
        [UIHint("DepartmentType")]
        [Display(Name = "部门类型")]
        public int Type { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 标志
        /// </summary>
        [Display(Name = "标志")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "介绍")]
        public string Description { get; set; }

        /// <summary>
        /// 状态，0:正常；1:删除
        /// </summary>
        public int Status { get; set; }
        #endregion //General Property

        #region College Scale Property
        /// <summary>
        /// 本科生
        /// </summary>
        [Display(Name = "本科生")]
        public int BachelorCount { get; set; }

        /// <summary>
        /// 研究生
        /// </summary>
        [Display(Name = "研究生")]
        public int GraduateCount { get; set; }

        /// <summary>
        /// 工程硕士
        /// </summary>
        [Display(Name = "工程硕士")]
        public int MasterOfEngineerCount { get; set; }

        /// <summary>
        /// 博士生
        /// </summary>
        [Display(Name = "博士生")]
        public int DoctorCount { get; set; }

        /// <summary>
        /// 在编教工总数
        /// </summary>
        [Display(Name = "在编教工总数")]
        public int StaffCount { get; set; }

        /// <summary>
        /// 党政管理人数(含处级领导)
        /// </summary>
        [Display(Name = "党政管理人数(含处级领导)")]
        public int PartyLeaderCount { get; set; }

        /// <summary>
        /// 院处级领导
        /// </summary>
        [Display(Name = "院处级领导")]
        public int SectionChiefCount { get; set; }

        /// <summary>
        /// 教授
        /// </summary>
        [Display(Name = "教授")]
        public int ProfessorCount { get; set; }

        /// <summary>
        /// 副教授
        /// </summary>
        [Display(Name = "副教授")]
        public int AssociateProfessorCount { get; set; }

        /// <summary>
        /// 中级及以下职称
        /// </summary>
        [Display(Name = "中级及以下职称")]
        public int MediumTeacherCount { get; set; }

        /// <summary>
        /// 高级教辅
        /// </summary>
        [Display(Name = "高级教辅")]
        public int AdvanceAssistantCount { get; set; }

        /// <summary>
        /// 中级及以下教辅
        /// </summary>
        [Display(Name = "中级及以下教辅")]
        public int MediumAssistantCount { get; set; }

        /// <summary>
        /// 文理科, 1:文科, 2:理科
        /// </summary>        
        [UIHint("SubjectType")]
        [Display(Name = "学科类别" )]
        public int SubjectType { get; set; }

        /// <summary>
        /// 学科系数K1
        /// </summary>
        [Display(Name = "学科系数K1" )]
        public double FactorK1 { get; set; }

        /// <summary>
        /// 科研系数K3
        /// </summary>
        [Display(Name = "科研系数K3")]
        public double FactorK3 { get; set; }
        #endregion //College Scale Property

        #region College Research Property
        /// <summary>
        /// 纵向经费
        /// </summary>
        [Display(Name = "纵向经费")]
        public double LongitudinalFunds { get; set; }

        /// <summary>
        /// 横向经费
        /// </summary>
        [Display(Name = "横向经费")]
        public double TransverseFunds { get; set; }

        /// <summary>
        /// 开发公司经费
        /// </summary>
        [Display(Name = "开发公司经费")]
        public double CompanyFunds { get; set; }            
        #endregion //College Research Property

        #region College Special Area Property
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
        #endregion //College Special Area Property

        #region Institution Scale Property
        /// <summary>
        /// 正校长(书记)
        /// </summary>
        [Display(Name = "正校长(书记)")]
        public int PresidentCount { get; set; }

        /// <summary>
        /// 副校长(书记)
        /// </summary>
        [Display(Name = "正校长(书记)")]
        public int VicePresidentCount { get; set; }

        /// <summary>
        /// 部门正职
        /// </summary>
        [Display(Name = "部门正职")]
        public int ChiefCount { get; set; }

        /// <summary>
        /// 部门副职
        /// </summary>
        [Display(Name = "部门副职")]
        public int ViceChiefCount { get; set; }

        /// <summary>
        /// 部门成员
        /// </summary>
        [Display(Name = "部门成员")]
        public int MemberCount { get; set; }
        #endregion //Institution Scale Property
    }
}
