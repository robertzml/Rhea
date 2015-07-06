using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Apartment.Models
{
    /// <summary>
    /// 青教基本数据模型
    /// </summary>
    public class BaseInfoModel
    {
        /// <summary>
        /// 楼宇数量
        /// </summary>
        [Display(Name = "楼宇数量")]
        public int BlockCount { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        [Display(Name = "房间数量")]
        public int RoomCount { get; set; }

        /// <summary>
        /// 两室一厅数量
        /// </summary>
        [Display(Name = "两室一厅数量")]
        public int TwoBedroomsCount { get; set; }

        /// <summary>
        /// 大一室一厅数量
        /// </summary>
        [Display(Name = "大一室一厅数量")]
        public int BigOneBedroomsCount { get; set; }

        /// <summary>
        /// 一室一厅数量
        /// </summary>
        [Display(Name = "一室一厅数量")]
        public int OneBedroomsCount { get; set; }

        /// <summary>
        /// 大单间数量
        /// </summary>
        [Display(Name = "大单间数量")]
        public int BigSimpleRoomCount { get; set; }

        /// <summary>
        /// 单间数量
        /// </summary>
        [Display(Name = "单间数量")]
        public int SimpleRoomCount { get; set; }

        /// <summary>
        /// 空房间数量
        /// </summary>
        [Display(Name = "空房间数量")]
        public int EmptyRoomCount { get; set; }

        /// <summary>
        /// 空两室一厅数量
        /// </summary>
        [Display(Name = "空两室一厅数量")]
        public int EmptyTwoBedroomsCount { get; set; }

        /// <summary>
        /// 空大一室一厅数量
        /// </summary>
        [Display(Name = "空大一室一厅数量")]
        public int EmptyBigOneBedroomsCount { get; set; }

        /// <summary>
        /// 空一室一厅数量
        /// </summary>
        [Display(Name = "空一室一厅数量")]
        public int EmptyOneBedroomsCount { get; set; }

        /// <summary>
        /// 空大单间数量
        /// </summary>
        [Display(Name = "空大单间数量")]
        public int EmptyBigSimpleRoomCount { get; set; }

        /// <summary>
        /// 空单间数量
        /// </summary>
        [Display(Name = "空单间数量")]
        public int EmptySimpleRoomCount { get; set; }
    }
}