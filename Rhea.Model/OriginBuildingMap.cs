using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model
{
    /// <summary>
    /// 原始建筑对应类
    /// </summary>
    public class OriginBuildingMap
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
        /// 组织类型
        /// </summary>
        public int OrganizeType { get; set; }

        /// <summary>
        /// 原父级ID
        /// </summary>
        public int OldParentId { get; set; }

        /// <summary>
        /// 新父级ID
        /// </summary>
        public int NewParentId { get; set; }

        /// <summary>
        /// 是否有子建筑
        /// </summary>
        public bool HasChild { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
