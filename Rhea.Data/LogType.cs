using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Data
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 信息初始化
        /// </summary>
        [Display(Name = "信息初始化")]
        Initialize = 1,

        /// <summary>
        /// 校区添加
        /// </summary>
        [Display(Name = "校区添加")]
        CampusCreate = 10,

        /// <summary>
        /// 校区编辑
        /// </summary>
        [Display(Name = "校区编辑")]
        CampusEdit = 11,

        /// <summary>
        /// 校区删除
        /// </summary>
        [Display(Name = "校区删除")]
        CampusDelete = 12,

        /// <summary>
        /// 楼群添加
        /// </summary>
        [Display(Name = "楼群添加")]
        BuildingGroupCreate = 20,

        /// <summary>
        /// 楼群编辑
        /// </summary>
        [Display(Name = "楼群编辑")]
        BuildingGroupEdit = 21,

        /// <summary>
        /// 楼群删除
        /// </summary>
        [Display(Name = "楼群删除")]
        BuildingGroupDelete = 22,

        /// <summary>
        /// 楼宇添加
        /// </summary>
        [Display(Name = "楼宇添加")]
        BuildingCreate = 30,

        /// <summary>
        /// 楼宇编辑
        /// </summary>
        [Display(Name = "楼宇编辑")]
        BuildingEdit = 31,

        /// <summary>
        /// 楼宇删除
        /// </summary>
        [Display(Name = "楼宇删除")]
        BuildingDelete = 32,

        /// <summary>
        /// 楼层添加
        /// </summary>
        [Display(Name = "楼层添加")]
        FloorCreate = 33,

        /// <summary>
        /// 楼层编辑
        /// </summary>
        [Display(Name = "楼层编辑")]
        FloorEdit = 34,

        /// <summary>
        /// 楼层删除
        /// </summary>
        [Display(Name = "楼层删除")]
        FloorDelete = 35,

        /// <summary>
        /// 房间添加
        /// </summary>
        [Display(Name = "房间添加")]
        RoomCreate = 40,

        /// <summary>
        /// 房间编辑
        /// </summary>
        [Display(Name = "房间编辑")]
        RoomEdit = 41,

        /// <summary>
        /// 房间删除
        /// </summary>
        [Display(Name = "房间删除")]
        RoomDelete = 42,

        /// <summary>
        /// 部门添加
        /// </summary>
        [Display(Name = "部门添加")]
        DepartmentCreate = 50,

        /// <summary>
        /// 部门编辑
        /// </summary>
        [Display(Name = "部门编辑")]
        DepartmentEdit = 51,

        /// <summary>
        /// 部门删除
        /// </summary>
        [Display(Name = "部门删除")]
        DepartmentDelete = 52,

        /// <summary>
        /// 校区历史归档
        /// </summary>
        [Display(Name = "校区历史归档")]
        CampusArchive = 60,

        /// <summary>
        /// 楼群历史归档
        /// </summary>
        [Display(Name = "楼群历史归档")]
        BuildingGroupArchive = 61,

        /// <summary>
        /// 楼宇历史归档
        /// </summary>
        [Display(Name = "楼宇历史归档")]
        BuildingArchive = 62,

        /// <summary>
        /// 房间历史归档
        /// </summary>
        [Display(Name = "房间历史归档")]
        RoomArchive = 63,

        /// <summary>
        /// 部门历史归档
        /// </summary>
        [Display(Name = "部门历史归档")]
        DepartmentArchive = 64,

        /// <summary>
        /// 房间分配
        /// </summary>
        [Display(Name = "房间分配")]
        RoomAssign = 70
    }
}
