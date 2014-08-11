﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 楼群主页模型
    /// </summary>
    public class BuildingGroupIndexModel
    {
        /// <summary>
        /// 楼群对象
        /// </summary>
        public BuildingGroup BuildingGroup { get; set; }

        /// <summary>
        /// 下属分区
        /// </summary>
        public List<Subregion> Subregions { get; set; }

        /// <summary>
        /// 入驻部门
        /// </summary>
        public List<BuildingDepartmentModel> EnterDepartment { get; set; }
    }

    /// <summary>
    /// 分区主页模型
    /// </summary>
    public class SubregionIndexModel
    {
        /// <summary>
        /// 分区对象
        /// </summary>
        public Subregion Subregion { get; set; }

        /// <summary>
        /// 上级楼群对象
        /// </summary>
        public BuildingGroup Parent { get; set; }

        /// <summary>
        /// 入驻部门
        /// </summary>
        public List<BuildingDepartmentModel> EnterDepartment { get; set; }
    }

    /// <summary>
    /// 组团主页模型
    /// </summary>
    public class ClusterIndexModel
    {
        /// <summary>
        /// 组团对象
        /// </summary>
        public Cluster Cluster { get; set; }

        /// <summary>
        /// 下属楼宇
        /// </summary>
        public List<Block> Blocks { get; set; }

        /// <summary>
        /// 入驻部门
        /// </summary>
        public List<BuildingDepartmentModel> EnterDepartment { get; set; }
    }

    /// <summary>
    /// 楼宇主页模型
    /// </summary>
    public class BlockIndexModel
    {
        /// <summary>
        /// 楼宇对象
        /// </summary>
        public Block Block { get; set; }

        /// <summary>
        /// 上级组团
        /// </summary>
        public Cluster Parent { get; set; }

        /// <summary>
        /// 入驻部门
        /// </summary>
        public List<BuildingDepartmentModel> EnterDepartment { get; set; }
    }

    /// <summary>
    /// 独栋首页模型
    /// </summary>
    public class CottageIndexModel
    {
        /// <summary>
        /// 独栋对象
        /// </summary>
        public Cottage Cottage { get; set; }

        /// <summary>
        /// 入驻部门
        /// </summary>
        public List<BuildingDepartmentModel> EnterDepartment { get; set; }
    }

    /// <summary>
    /// 建筑入驻部门模型
    /// </summary>
    public class BuildingDepartmentModel
    {
        /// <summary>
        /// 建筑ID
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public double TotalUsableArea { get; set; }
    }
}