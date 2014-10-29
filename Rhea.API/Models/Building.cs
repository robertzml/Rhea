using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.API.Models
{
    /// <summary>
    /// 建筑类
    /// </summary>
    public class Building
    {
        /// <summary>
        /// 建筑ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属校区ID
        /// </summary>
        public int CampusId { get; set; }

        /// <summary>
        /// 建筑组织形式
        /// </summary>
        /// <remarks>
        /// 1:楼群,2:组团,3:独栋,4:分区,5:楼宇,6:操场
        /// </remarks>       
        public int OrganizeType { get; set; }

        /// <summary>
        /// 父级建筑ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 是否有子建筑
        /// </summary>
        public bool HasChild { get; set; }

        /// <summary>
        /// 使用类型
        /// </summary>
        /// <remarks>
        /// 1:学院楼宇,2:教学楼宇,3:行政办公,4:宿舍楼宇,5:辅助楼宇,6:基础设施,7:室外运动场
        /// </remarks>
        public int UseType { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}