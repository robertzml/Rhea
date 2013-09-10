using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Admin.Models
{
    /// <summary>
    /// 归档模型
    /// </summary>
    public class ArchiveModel
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        [Display(Name = "选择数据")]
        public int ArchiveType { get; set; }

        /// <summary>
        /// 归档日期
        /// </summary>
        [Required]
        [Display(Name = "选择日期")]
        public DateTime ArchiveDate { get; set; }

        /// <summary>
        /// 备注内容
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注内容")]
        public string ArchiveContent { get; set; }
    }
}