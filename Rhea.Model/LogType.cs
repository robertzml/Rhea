using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model
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
        /// 建筑添加
        /// </summary>
        [Display(Name = "建筑添加")]
        BuildingCreate = 20,

        /// <summary>
        /// 建筑编辑
        /// </summary>
        [Display(Name = "建筑编辑")]
        BuildingEdit = 21,

        /// <summary>
        /// 建筑删除
        /// </summary>
        [Display(Name = "建筑删除")]
        BuildingDelete = 22,

        /// <summary>
        /// 楼层添加
        /// </summary>
        [Display(Name = "楼层添加")]
        FloorCreate = 23,

        /// <summary>
        /// 楼层编辑
        /// </summary>
        [Display(Name = "楼层编辑")]
        FloorEdit = 24,

        /// <summary>
        /// 楼层删除
        /// </summary>
        [Display(Name = "楼层删除")]
        FloorDelete = 25,

        /// <summary>
        /// 上传楼层平面图
        /// </summary>
        [Display(Name = "上传楼层平面图")]
        FloorSvgUpload = 26,

        /// <summary>
        /// 房间添加
        /// </summary>
        [Display(Name = "房间添加")]
        RoomCreate = 30,

        /// <summary>
        /// 房间编辑
        /// </summary>
        [Display(Name = "房间编辑")]
        RoomEdit = 31,

        /// <summary>
        /// 房间删除
        /// </summary>
        [Display(Name = "房间删除")]
        RoomDelete = 32,

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
        /// 建筑历史归档
        /// </summary>
        [Display(Name = "建筑历史归档")]
        BuildingArchive = 61,

        /// <summary>
        /// 房间历史归档
        /// </summary>
        [Display(Name = "房间历史归档")]
        RoomArchive = 62,

        /// <summary>
        /// 部门历史归档
        /// </summary>
        [Display(Name = "部门历史归档")]
        DepartmentArchive = 63,

        /// <summary>
        /// 字典添加
        /// </summary>
        [Display(Name = "字典添加")]
        DictionaryCreate = 70,

        /// <summary>
        /// 字典编辑
        /// </summary>
        [Display(Name = "字典编辑")]
        DictionaryEdit = 71,

        /// <summary>
        /// 用户登录成功
        /// </summary>
        [Display(Name = "用户登录成功")]
        UserLoginSuccess = 100,

        /// <summary>
        /// 用户登录失败
        /// </summary>
        [Display(Name = "用户登录失败")]
        UserLoginFailed = 101,

        /// <summary>
        /// 编辑青教住户
        /// </summary>
        [Display(Name = "编辑青教住户")]
        InhabitantEdit = 151,

        /// <summary>
        /// 编辑居住记录
        /// </summary>
        [Display(Name = "编辑居住记录")]
        ResideRecordEdit = 156,

        /// <summary>
        /// 上传居住记录附件
        /// </summary>
        [Display(Name = "上传居住记录附件")]
        ResideRecordUploadFile = 157,

        /// <summary>
        /// 变更房租
        /// </summary>
        [Display(Name = "变更房租")]
        ChangeRent = 158,

        /// <summary>
        /// 入住业务办理
        /// </summary>
        [Display(Name = "入住业务办理")]
        ApartmentCheckIn = 160,

        /// <summary>
        /// 退房业务办理
        /// </summary>
        [Display(Name = "退房业务办理")]
        ApartmentCheckOut = 161,

        /// <summary>
        /// 延期业务办理
        /// </summary>
        [Display(Name = "延期业务办理")]
        ApartmentExtend = 162,

        /// <summary>
        /// 换房业务办理
        /// </summary>
        [Display(Name = "换房业务办理")]
        ApartmentExchange = 163,

        /// <summary>
        /// 特殊换房业务办理
        /// </summary>
        [Display(Name = "特殊换房业务办理")]
        ApartmentSpecialExchange = 164,

        /// <summary>
        /// 新教职工登记办理
        /// </summary>
        [Display(Name = "新教职工登记办理")]
        ApartmentRegister = 165,

        /// <summary>
        /// 其它入住业务办理
        /// </summary>
        [Display(Name = "其它入住业务办理")]
        ApartmentCheckIn2 = 166,

        /// <summary>
        /// 添加任务
        /// </summary>
        [Display(Name = "添加任务")]
        TaskCreate = 201
    }
}
