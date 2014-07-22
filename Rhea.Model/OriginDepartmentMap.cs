using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model
{
    /// <summary>
    /// 原始部门对应类
    /// </summary>
    public class OriginDepartmentMap
    {
        /// <summary>
        /// 原ID
        /// </summary>
        public int OldId { get; set; }

        /// <summary>
        /// 新ID
        /// </summary>
        public int NewId { get; set; }

        /// <summary>
        /// 新名称
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        /// 新类型
        /// </summary>
        public int Type { get; set; }
    }
}
