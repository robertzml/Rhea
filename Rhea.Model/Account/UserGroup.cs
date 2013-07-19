using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Account
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Display(Name = "显示名称")]
        public string Title { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Display(Name = "级别")]
        public int Rank { get; set; }

        /// <summary>
        /// 类型，为2
        /// </summary>
        [Display(Name = "类型")]
        public int Type { get; set; }

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
        /// <remarks>
        /// 0:正常，1:删除
        /// </remarks>
        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}
