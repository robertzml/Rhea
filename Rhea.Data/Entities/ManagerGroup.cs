using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Entities
{
    /// <summary>
    /// 管理组
    /// </summary>
    public class ManagerGroup
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Rank { get; set; }
    }
}
