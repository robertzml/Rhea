﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business
{
    /// <summary>
    /// 常量类
    /// </summary>
    public static class RheaConstant
    {
        #region Field
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
        /// 头像目录
        /// </summary>
        public static readonly string AvatarRoot = "/Attachment/avatar/";

        /// <summary>
        /// 青教记录附件目录
        /// </summary>
        public static readonly string ApartmentRecord = "/Attachment/apartment/record/";

        /// <summary>
        /// 顶级父建筑名称
        /// </summary>
        public static readonly string TopParentBuildingName = "江南大学";

        /// <summary>
        /// 面积保留小数点位数
        /// </summary>
        public static readonly int AreaDecimalDigits = 2;

        /// <summary>
        /// 青教公寓建筑ID
        /// </summary>
        public static readonly int ApartmentBuildingId = 200037;

        /// <summary>
        /// 青教户型
        /// </summary>
        public static readonly string[] ApartmentHouseTypes = { "两室一厅", "大一室一厅", "一室一厅", "大单间", "单间" };
        #endregion //Field
    }
}
