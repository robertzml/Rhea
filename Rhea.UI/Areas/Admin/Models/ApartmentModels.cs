using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Admin.Models
{
    /// <summary>
    /// 特殊换房办理模型
    /// </summary>
    public class SpecialExchangeModel
    {
        /// <summary>
        /// 住户ID
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "住户ID")]
        public string InhabitantId { get; set; }

        /// <summary>
        /// 原房间
        /// </summary>
        [Display(Name = "原房间")]
        public int OldRoomId { get; set; }

        /// <summary>
        /// 楼宇选择
        /// </summary>
        [UIHint("ApartmentBuildingList")]
        [Display(Name = "楼宇选择")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 新房间
        /// </summary>
        [Display(Name = "新房间")]
        public int NewRoomId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}