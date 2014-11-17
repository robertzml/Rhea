using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Apartment.Models
{
    /// <summary>
    /// 变更房租模型
    /// </summary>
    public class ChangeRentModel
    {
        /// <summary>
        /// 居住记录ID
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// 住户姓名
        /// </summary>
        [Display(Name = "住户姓名")]
        public string InhabitantName { get; set; }

        /// <summary>
        /// 当前房租
        /// </summary>
        [Display(Name = "当前房租")]
        public decimal CurrentRent { get; set; }

        /// <summary>
        /// 上次房租
        /// </summary>
        [Display(Name = "上次房租")]
        public decimal LastRent { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "开始日期")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}