using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Apartment.Models
{
    /// <summary>
    /// 房租表单提交模型
    /// </summary>
    public class RentRequestModel
    {
        /// <summary>
        /// 月份
        /// </summary>
        [Required]
        [Display(Name = "月份")]
        public DateTime Month { get; set; }
    }
}