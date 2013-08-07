﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Models
{
    /// <summary>
    /// 部门侧边栏模型
    /// </summary>
    public class DepartmentSectionModel
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 部门楼宇列表
        /// </summary>
        public List<Building> Buildings { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }

        /// <summary>
        /// 使用总面积
        /// </summary>
        public int TotalArea { get; set; }

        /// <summary>
        /// 教职工人数
        /// </summary>
        public int StaffCount { get; set; }

        /// <summary>
        /// 指标应有面积
        /// </summary>
        public double DeservedArea { get; set; }

        /// <summary>
        /// 指标现有面积
        /// </summary>
        public double ExistingArea { get; set; }

        /// <summary>
        /// 超标比值
        /// </summary>
        public double Overproof { get; set; }
    }
}