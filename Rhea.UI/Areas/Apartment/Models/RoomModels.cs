using Rhea.Model.Apartment;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Areas.Apartment.Models
{
    /// <summary>
    /// 房间居住模型
    /// </summary>
    public class RoomResideModel
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        [Display(Name = "房间ID")]
        public int RoomId { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        [Display(Name = "房间名称")]
        public string Name { get; set; }

        /// <summary>
        /// 房间编号
        /// </summary>
        [Display(Name = "房间编号")]
        public string Number { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Display(Name = "楼层")]
        public int Floor { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        [Display(Name = "使用面积")]
        public double UsableArea { get; set; }

        /// <summary>
        /// 户型
        /// </summary>
        [Display(Name = "户型")]
        public string HouseType { get; set; }

        /// <summary>
        /// 朝向
        /// </summary>
        [Display(Name = "朝向")]
        public string Orientation { get; set; }

        /// <summary>
        /// 所属楼宇
        /// </summary>      
        [Display(Name = "所属楼宇")]
        public string BuildingName { get; set; }

        /// <summary>
        /// 居住类型
        /// </summary>
        [Display(Name = "居住类型")]
        public ResideType RoomResideType { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        [Display(Name = "入住时间")]
        public DateTime? EnterDate { get; set; }

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
        /// 居住人类型
        /// </summary>
        [Display(Name = "居住人类型")]

        public string InhabitantType { get; set; }

        /// <summary>
        /// 房租
        /// </summary>
        [Display(Name = "房租")]
        public decimal Rent { get; set; }

        /// <summary>
        /// 居住状态
        /// </summary>
        [Display(Name = "居住状态")]
        public int RecordStatus { get; set; }
    }

    /// <summary>
    /// 房间树模型
    /// </summary>
    public class RoomTreeModel
    {
        public List<Block> Blocks;

        public List<ApartmentRoom> Rooms;
    }
}