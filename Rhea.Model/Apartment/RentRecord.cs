using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model.Apartment
{
    /// <summary>
    /// 房租记录类
    /// </summary>
    public class RentRecord
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
