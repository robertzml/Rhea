﻿using Rhea.Model.Apartment;
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
        /// 到期时间
        /// </summary>
        [Display(Name = "到期时间")]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 月数
        /// </summary>
        [Display(Name = "月数")]
        public int MonthCount { get; set; }

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
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Gender { get; set; }

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
        /// 身份证
        /// </summary>
        [Display(Name = "身份证")]
        public string IdentityCard { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [Display(Name = "学历")]
        public string Education { get; set; }

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
        /// 婚姻状况
        /// </summary>
        [Display(Name = "婚姻状况")]
        public string Marriage { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>
        [Display(Name = "配偶姓名")]
        public string MateName { get; set; }

        /// <summary>
        /// 是否双职工
        /// </summary>
        [Display(Name = "是否双职工")]
        public bool? IsCouple { get; set; }

        /// <summary>
        /// 房租
        /// </summary>
        [Display(Name = "房租")]
        public decimal Rent { get; set; }

        /// <summary>
        /// 蠡湖家园入住时间
        /// </summary>
        [Display(Name = "蠡湖家园入住时间")]
        public DateTime? LiHuEnterDate { get; set; }

        /// <summary>
        /// 居住状态
        /// </summary>
        [Display(Name = "居住状态")]
        public int RecordStatus { get; set; }

        /// <summary>
        /// 居住记录备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
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