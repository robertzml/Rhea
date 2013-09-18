using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 图片展示模型
    /// </summary>
    public class GalleryModel
    {
        public string ImageUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }
    }

    /// <summary>
    /// 土地类型模型
    /// </summary>
    public class LandTypeModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 面积值
        /// </summary>
        public double Area { get; set; }
    }    
}
