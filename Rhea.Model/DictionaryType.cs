using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public enum DictionaryType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Display(Name = "未知")]
        Unknown = 0,

        /// <summary>
        /// 文本类型
        /// </summary>
        [Display(Name = "文本类型")]
        Text = 1,

        /// <summary>
        /// 键值类型
        /// </summary>
        [Display(Name = "键值类型")]
        Pair = 2,

        /// <summary>
        /// 组合类型
        /// </summary>
        [Display(Name = "组合类型")]
        Combined = 3
    }
}
