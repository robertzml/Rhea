using System;
using System.Collections.Generic;
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
        public string Name { get; set; }

        /// <summary>
        /// 类型，1:部门; 2:学院
        /// </summary>
        public int Type { get; set; }
    }
}
