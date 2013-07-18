using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "文理科" )]
        public int ArtsAndScience { get; set; }
        #endregion //College Scale Property
    }
}
