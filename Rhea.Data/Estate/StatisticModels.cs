using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 统计主页模型
    /// </summary>
    public class StatisticHomeModel
    {
        /// <summary>
        /// 校控房面积
        /// </summary>
        public double UniversityControlArea { get; set; }

        /// <summary>
        /// 校控房比例
        /// </summary>
        public double UniversityControlPercentage { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        public double TotalArea { get; set; }
    }

    /// <summary>
    /// 建筑分类面积
    /// </summary>
    public class UseTypeAreaModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }
    }

    /// <summary>
    /// 房间二级分类面积模型
    /// </summary>
    /// <remarks>
    /// 部门统计，楼宇统计，共同使用
    /// </remarks>
    public class RoomSecondClassifyAreaModel
    {
        /// <summary>
        /// 一级编码
        /// </summary>
        public int FunctionFirstCode { get; set; }

        /// <summary>
        /// 二级编码
        /// </summary>
        public int FunctionSecondCode { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string FunctionProperty { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }
    }

    /// <summary>
    /// 房间一级分类面积模型
    /// </summary>
    public class RoomFirstClassifyAreaModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 一级编码
        /// </summary>
        public int FunctionFirstCode { get; set; }

        /// <summary>
        /// 下属二级分类
        /// </summary>
        public List<RoomSecondClassifyAreaModel> SecondClassify { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }
    }

    /// <summary>
    /// 部门分类用房面积模型
    /// </summary>
    public class DepartmentClassifyAreaModel
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public List<RoomFirstClassifyAreaModel> FirstClassify { get; set; }

        /// <summary>
        /// 使用总面积
        /// </summary>
        public double TotalArea { get; set; }
    }

    /// <summary>
    /// 部门总面积模型
    /// </summary>
    /// <remarks>
    /// 总建筑面积，总使用面积
    /// </remarks>
    public class DepartmentTotalAreaModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        /// <summary>
        /// 总建筑面积
        /// </summary>
        public double BuildArea { get; set; }

        /// <summary>
        /// 总使用面积
        /// </summary>
        public double UsableArea { get; set; }

        /// <summary>
        /// 部门分楼宇使用面积
        /// </summary>
        public List<DepartmentBuildingAreaModel> BuildingArea { get; set; }

        /// <summary>
        /// 房间总数
        /// </summary>
        public int TotalRoomCount { get; set; }
    }

    /// <summary>
    /// 楼宇分类用房面积模型
    /// </summary>
    public class BuildingClassifyAreaModel
    {
        /// <summary>
        /// 楼宇编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public List<RoomFirstClassifyAreaModel> FirstClassify { get; set; }

        /// <summary>
        /// 使用总面积
        /// </summary>
        public double TotalArea { get; set; }
    }

    /// <summary>
    /// 楼群总面积模型
    /// </summary>
    /// <remarks>
    /// 总建筑面积，总使用面积
    /// </remarks>
    public class BuildingGroupTotalAreaModel
    {
        /// <summary>
        /// 楼群ID
        /// </summary>
        public int BuildingGroupId { get; set; }

        /// <summary>
        /// 楼群名称
        /// </summary>
        public string BuildingGroupName { get; set; }

        /// <summary>
        /// 楼群使用类型
        /// </summary>
        public int BuildingGroupType { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public double UsableArea { get; set; }

        /// <summary>
        /// 得房率
        /// </summary>
        public double SpaceRate { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomCount { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public List<RoomFirstClassifyAreaModel> FirstClassify { get; set; }
    }

    /// <summary>
    /// 部门单个楼宇内用房面积
    /// </summary>
    public class DepartmentBuildingAreaModel
    {
        public int BuildingId { get; set; }

        public string BuildingName { get; set; }

        public int RoomCount { get; set; }

        public double UsableArea { get; set; }
    }

    /// <summary>
    /// 部门人均面积模型
    /// </summary>
    public class DepartmentAverageAreaModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        /// <summary>
        /// 平均面积
        /// </summary>
        /// <remarks>
        /// 如人均办公面积，人均科研面积
        /// </remarks>
        public double AverageArea { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// 总经费
        /// </summary>
        public double TotalFunds { get; set; }

        /// <summary>
        /// 面积总数
        /// </summary>
        public double TotalArea { get; set; }
    }
}