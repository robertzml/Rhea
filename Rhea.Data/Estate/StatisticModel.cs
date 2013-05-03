using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 二级分类面积模型
    /// </summary>
    public class SecondClassifyAreaModel
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
    }

    /// <summary>
    /// 学院分类用房面积模型
    /// </summary>
    public class CollegeClassifyAreaModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学院名称
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// 办公用房面积
        /// </summary>
        public double OfficeArea { get; set; }

        /// <summary>
        /// 办公二级分类面积
        /// </summary>
        public List<SecondClassifyAreaModel> OfficeDetailArea { get; set; }

        /// <summary>
        /// 教学用房面积
        /// </summary>
        public double EducationArea { get; set; }

        /// <summary>
        /// 教学二级分类面积
        /// </summary>
        public List<SecondClassifyAreaModel> EducationDetailArea { get; set; }

        /// <summary>
        /// 实验用房面积
        /// </summary>
        public double ExperimentArea { get; set; }

        /// <summary>
        /// 实验二级用房面积
        /// </summary>
        public List<SecondClassifyAreaModel> ExperimentDetailArea { get; set; }

        /// <summary>
        /// 科研用房面积
        /// </summary>
        public double ResearchArea { get; set; }

        /// <summary>
        /// 科研二级用房面积
        /// </summary>
        public List<SecondClassifyAreaModel> ResearchDetailArea { get; set; }

        /// <summary>
        /// 总计
        /// </summary>
        public double TotalArea { get; set; }
    }

    /// <summary>
    /// 学院分大楼面积模型
    /// </summary>
    public class CollegeBuildingAreaModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 学院
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// 大楼面积
        /// </summary>
        public List<BuildingAreaModel> BuildingArea { get; set; }
    }

    /// <summary>
    /// 楼宇面积模型
    /// </summary>
    public class BuildingAreaModel
    {
        /// <summary>
        /// 楼宇编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public double Area { get; set; }
    }
}