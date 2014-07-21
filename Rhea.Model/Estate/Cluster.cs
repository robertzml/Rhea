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
    /// 组团类
    /// </summary>
    public class Cluster : Building
    {
        #region Property
        /// <summary>
        /// 楼宇数量
        /// </summary>
        [BsonElement("blockCount")]
        [Display(Name = "楼宇数量")]
        public int BlockCount { get; set; }
        #endregion //Property
    }
}
