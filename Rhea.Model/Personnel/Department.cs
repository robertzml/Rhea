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
        /// 部门类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 标志
        /// </summary>
        [Display(Name = "标志" )]
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
    }
}
