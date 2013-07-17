using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Estate
{
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
    /// 部门二级分类面积模型
    /// </summary>
    public class DepartmentSecondClassifyAreaModel
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
    /// 部门一级分类面积模型
    /// </summary>
    public class DepartmentFirstClassifyAreaModel
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
        public List<DepartmentSecondClassifyAreaModel> SecondClassify { get; set; }

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
        public List<DepartmentFirstClassifyAreaModel> FirstClassify { get; set; }
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

        public double BuildArea { get; set; }

        public double UsableArea { get; set; }
    }
}