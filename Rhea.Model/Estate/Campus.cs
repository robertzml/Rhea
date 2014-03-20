﻿using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 校区类
    /// </summary>
    [CollectionName("campus")]
    public class Campus : CampusBase
    {
        #region Constructor
        /// <summary>
        /// 校区类
        /// </summary>
        public Campus()
        {
            this.Log = new Log();
        }
        #endregion //Constructor

        #region Property
        /// <summary>
        /// 日志属性
        /// </summary>
        [BsonElement("log")]
        [Display(Name = "日志属性")]
        public Log Log { get; set; }
        #endregion //Property
    }
}
