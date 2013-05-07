using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Rhea.Business
{
    /// <summary>
    /// Rhea常量类
    /// </summary>
    public static class RheaConstant
    {
        #region Constructor
        static RheaConstant()
        {           
            RheaConstant.ApiHost = ConfigurationManager.AppSettings["ApiHost"];            
        }
        #endregion //Constructor

        #region Field
        /// <summary>
        /// Cronus数据库
        /// </summary>
        public static readonly string CronusDatabase = "cronus";

        /// <summary>
        /// API服务器地址
        /// </summary>
        public static readonly string ApiHost;

        /// <summary>
        /// 图片根目录
        /// </summary>
        public static readonly string ImagesRoot = "/Images/";

        /// <summary>
        /// SVG根目录
        /// </summary>
        public static readonly string SvgRoot = "/Images/svg/";
        #endregion //Field
    }
}
