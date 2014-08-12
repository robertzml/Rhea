using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model
{
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
        public int FirstCode { get; set; }

        /// <summary>
        /// 二级编码
        /// </summary>
        public int SecondCode { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Property { get; set; }

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
        public string ClassifyName { get; set; }

        /// <summary>
        /// 一级编码
        /// </summary>
        public int FirstCode { get; set; }

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
}
