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
        /// 开始日期
        /// </summary>
        [Required]
        [Display(Name = "开始日期")]
        public DateTime Start { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [Required]
        [Display(Name = "结束日期")]
        public DateTime End { get; set; }
    }

    /// <summary>
    /// 房租处理模型
    /// </summary>
    public class RentProcessModel
    {
        /// <summary>
        /// 居住记录ID
        /// </summary>
        [Display(Name = "居住记录ID")]
        public string ResideRecordId { get; set; }

        /// <summary>
        /// 居住人ID
        /// </summary>
        [Display(Name = "居住人ID")]
        public string InhabitantId { get; set; }

        /// <summary>
        /// 居住人姓名
        /// </summary>
        [Display(Name = "居住人姓名")]
        public string InhabitantName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Display(Name = "部门")]
        public string InhabitantDepartment { get; set; }

        /// <summary>
        /// 居住人工号
        /// </summary>
        [Display(Name = "居住人工号")]
        public string InhabitantNumber { get; set; }

        /// <summary>
        /// 居住人类型
        /// </summary>
        [Display(Name = "居住人类型")]
        public string InhabitantType { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        [Display(Name = "房间号")]
        public string RoomNumber { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "入住时间")]
        public DateTime? EnterDate { get; set; }

        /// <summary>
        /// 离开时间
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "离开时间")]
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 前房租
        /// </summary>
        [Display(Name = "前房租")]
        public decimal PreviousRent { get; set; }

        /// <summary>
        /// 现房租
        /// </summary>
        [Display(Name = "现房租")]
        public decimal CurrentRent { get; set; }

        /// <summary>
        /// 居住记录状态
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}