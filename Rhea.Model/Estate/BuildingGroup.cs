using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 楼群类
    /// </summary>
    public class BuildingGroup : Building
    {
        #region Property
        /// <summary>
        /// 分区数量
        /// </summary>
        [BsonElement("subregionCount")]
        [Display(Name = "分区数量")]
        public int SubregionCount { get; set; }
        #endregion //Property
    }
}
