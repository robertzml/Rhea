using MongoDB.Bson.Serialization.Attributes;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model.Apartment
{
    /// <summary>
    /// 青教房间
    /// 普通房间扩展
    /// </summary>
    public class ApartmentRoom : Room
    {
        #region Property
        /// <summary>
        /// 户型
        /// </summary>
        [BsonElement("houseType")]
        [Display(Name = "户型")]
        public string HouseType { get; set; }

        /// <summary>
        /// 是否有空调
        /// </summary>
        [BsonElement("hasAirCondition")]
        [Display(Name = "是否有空调")]
        public bool HasAirCondition { get; set; }

        /// <summary>
        /// 是否有热水器
        /// </summary>
        [BsonElement("hasWaterHeater")]
        [Display(Name = "是否有热水器")]
        public bool HasWaterHeater { get; set; }

        /// <summary>
        /// 居住状态
        /// </summary>
        /// <remarks>
        /// 0:可分配；1:正常居住；2:挂职居住；3:部门占用；4:仓库；5:保留
        /// </remarks>
        [BsonElement("resideType")]
        [Display(Name = "居住状态")]
        public int ResideType { get; set; }
        #endregion //Property
    }
}
