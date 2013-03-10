using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Entities
{
    public class Building
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
        /// 所属楼群ID
        /// </summary>
        public int BuildingGroupId { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public double UsableArea { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }


        public List<Floor> Floors { get; set; }
    }

    public class Floor
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public double UsableArea { get; set; }
    }
}
