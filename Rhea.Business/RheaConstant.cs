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
            RheaConstant.AuthUrl = ConfigurationManager.AppSettings["AuthUrl"];
        }
        #endregion //Constructor

        #region Field
        /// <summary>
        /// API服务器地址
        /// </summary>
        public static readonly string ApiHost;

        /// <summary>
        /// 统一身份认证地址
        /// </summary>
        public static readonly string AuthUrl;

        /// <summary>
        /// 图片根目录
        /// </summary>
        public static readonly string ImagesRoot = "/Attachment/";

        /// <summary>
        /// SVG根目录
        /// </summary>
        public static readonly string SvgRoot = "/Attachment/svg/";

        /// <summary>
        /// SVG备份目录
        /// </summary>
        public static readonly string SvgBackup = "/Attachment/svgbackup/";

        /// <summary>
        /// 面积小数位数
        /// </summary>
        public static readonly int AreaDecimalDigits = 2;
        #endregion //Field
    }
}
