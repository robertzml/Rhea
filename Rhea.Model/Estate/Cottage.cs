﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 独栋建筑类
    /// </summary>
    public class Cottage : Building
    {
        #region Constructor
        /// <summary>
        /// 独栋建筑类
        /// </summary>
        public Cottage()
        {
            this.Floors = new List<Floor>();
        }
        #endregion //Constructor

        #region Property
        /// <summary>
        /// 地上楼层数
        /// </summary>
        [BsonElement("aboveGroundFloor")]
        [Display(Name = "地上楼层数")]
        public int AboveGroundFloor { get; set; }

        /// <summary>
        /// 地下楼层数
        /// </summary>
        [BsonElement("underGroundFloor")]
        [Display(Name = "地下楼层数")]
        public int UnderGroundFloor { get; set; }

        /// <summary>
        /// 楼层列表
        /// </summary>
        [BsonElement("floors")]
        public List<Floor> Floors { get; set; }
        #endregion //Property
    }
}
