using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Rhea.Data.Entities
{
    /// <summary>
    /// 楼群
    /// </summary>    
    public class BuildingGroup
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
        /// 楼宇列表
        /// </summary>
        public List<Building> Buildings { get; set; }
    }
}
