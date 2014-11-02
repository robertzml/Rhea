using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.API.Models
{
    /// <summary>
    /// 房间类
    /// </summary>
    public class Room
    {
        /// <summary>
        /// 建筑ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 房间编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// 跨数
        /// </summary>
        public double? Span { get; set; }

        /// <summary>
        /// 朝向
        /// </summary>
        public string Orientation { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public double UsableArea { get; set; }

        /// <summary>
        /// 建筑ID
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 功能一级编码
        /// </summary>
        public int FirstCode { get; set; }

        /// <summary>
        /// 功能二级编码
        /// </summary>
        public int SecondCode { get; set; }

        /// <summary>
        /// 功能一级分类名称
        /// </summary>
        public string ClassifyName { get; set; }

        /// <summary>
        /// 功能属性
        /// </summary>
        public string FunctionProperty { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}