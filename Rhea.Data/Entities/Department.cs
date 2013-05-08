using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Data.Entities
{
    /// <summary>
    /// 部门模型
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
        [Display(Name = "部门名称" )]
        public string Name { get; set; }

        /// <summary>
        /// 类型，1:部门; 2:学院
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

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
