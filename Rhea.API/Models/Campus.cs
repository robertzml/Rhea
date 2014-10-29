using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.API.Models
{
    /// <summary>
    /// 校区类
    /// </summary>
    public class Campus
    {
        /// <summary>
        /// 校区ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}