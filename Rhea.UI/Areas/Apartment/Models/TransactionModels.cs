using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Apartment.Models
{
    /// <summary>
    /// 入住办理模型
    /// </summary>
    public class CheckInModel
    {
        /// <summary>
        /// 楼宇选择
        /// </summary>
        [UIHint("BuildingList")]
        [Display(Name = "楼宇选择")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 房间选择
        /// </summary>
        [Required]
        [Display(Name = "房间选择")]
        public int RoomId { get; set; }

        /// <summary>
        /// 历史住户
        /// </summary>
        [Display(Name = "历史住户")]
        public string OldInhabitant { get; set; }

        /// <summary>
        /// 工号、学号或其它
        /// </summary>
        [Display(Name = "工号")]
        public string JobNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Gender { get; set; }

        /// <summary>
        /// 住户类型
        /// 1:教职工；2:外聘人员；3:挂职；4:学生；5:其他
        /// </summary>
        [Required]
        [UIHint("InhabitantType")]
        [Display(Name = "住户类型")]
        public int Type { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Display(Name = "所属部门")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [Display(Name = "职务")]
        public string Duty { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = "电话")]
        public string Telephone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Display(Name = "身份证")]
        public string IdentityCard { get; set; }

        /// <summary>
        /// 是否双职工
        /// </summary>
        [UIHint("Boolean2")]
        [Display(Name = "是否双职工")]
        public bool? IsCouple { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        [Display(Name = "婚姻状况")]
        public string Marriage { get; set; }

        /// <summary>
        /// 公积金领取时间
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "公积金领取时间")]
        public DateTime? AccumulatedFundsDate { get; set; }

        /// <summary>
        /// 蠡湖家园入住时间
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "蠡湖家园入住时间")]
        public DateTime? LiHuEnterDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string InhabitantRemark { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "入住时间")]
        public DateTime? EnterDate { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "到期时间")]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 年限
        /// </summary>
        [Display(Name = "年限")]
        public string TermLimit { get; set; }

        /// <summary>
        /// 房租
        /// </summary>
        [Display(Name = "房租")]
        public decimal Rent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string RecordRemark { get; set; }
    }

    /// <summary>
    /// 退房办理模型
    /// </summary>
    public class CheckOutModel
    {
        /// <summary>
        /// 住户ID
        /// </summary>
        [Required]
        [Display(Name = "住户ID")]
        public string InhabitantId { get; set; }

        /// <summary>
        /// 住户姓名
        /// </summary>
        [Display(Name = "住户姓名")]
        public string InhabitantName { get; set; }

        /// <summary>
        /// 住户部门
        /// </summary>
        [Display(Name = "住户部门")]
        public string InhabitantDepartment { get; set; }

        /// <summary>
        /// 楼宇选择
        /// </summary>
        [UIHint("BuildingList")]
        [Display(Name = "楼宇选择")]
        public int BuildingId { get; set; }

        /// <summary>
        /// 房间选择
        /// </summary>
        [Required]
        [Display(Name = "房间选择")]
        public int RoomId { get; set; }
    }
}