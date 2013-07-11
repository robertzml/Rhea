using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Models
{
    /// <summary>
    /// 建筑分类面积
    /// </summary>
    public class UseTypeAreaModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }
    }
}