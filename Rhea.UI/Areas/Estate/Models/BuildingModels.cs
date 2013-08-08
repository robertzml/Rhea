using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rhea.Model.Estate;

namespace Rhea.UI.Areas.Estate.Models
{
    /// <summary>
    /// 楼层模型
    /// </summary>
    public class FloorViewModel
    {
        /// <summary>
        /// 楼层ID
        /// </summary>
        [Display(Name = "楼层ID")]
        public int Id { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 楼层编号
        /// </summary>
        [Display(Name = "楼层编号")]
        public int Number { get; set; }

        /// <summary>
        /// SVG路径
        /// </summary>
        [Display(Name = "SVG路径")]
        public string SvgPath { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        [Display(Name = "建筑面积")]
        public double BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        [Display(Name = "使用面积")]
        public double UsableArea { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        [Display(Name = "房间数量")]
        public int RoomCount { get; set; }

        /// <summary>
        /// 楼宇地上楼层数
        /// </summary>
        [Display(Name = "楼宇地上楼层数")]
        public int AboveGroundFloor { get; set; }
    }

    /// <summary>
    /// 楼宇总面积模型
    /// </summary>
    public class BuildingTotalAreaModel
    {
        public int BuildingId { get; set; }

        public string BuildingName { get; set; }

        public double BuildArea { get; set; }

        public double UsableArea { get; set; }

        public int RoomCount { get; set; }
    }

    /// <summary>
    /// 楼宇入住部门模型
    /// </summary>
    public class BuildingDepartmentModel
    {
        public int BuildingId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int RoomCount { get; set; }

        public double TotalUsableArea { get; set; }
    }

    /// <summary>
    /// 楼层入住部门模型
    /// </summary>
    public class FloorDepartmentModel
    {
        public int BuildingId { get; set; }

        public int Floor { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int RoomCount { get; set; }

        public double TotalUsableArea { get; set; }
    }

    /// <summary>
    /// 楼宇侧边栏模型
    /// </summary>
    public class BuildingSectionModel
    {
        public int BuildingId { get; set; }

        public string BuildingName { get; set; }

        public int BuildingGroupId { get; set; }

        public int RoomCount { get; set; }

        public int BuildArea { get; set; }

        public int UsableArea { get; set; }

        public List<Floor> Floors { get; set; }
    }
}