using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rhea.UI.Areas.Estate.Models;

namespace Rhea.UI.Models
{
    /// <summary>
    /// 地图点详情模型
    /// </summary>
    public class MapPointDetailModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public int Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        public int Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Title { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [Display(Name = "缩略图")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        [Display(Name = "建筑面积")]
        public double BuildArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        [Display(Name = "使用面积")]
        public double UsableArea { get; set; }

        /// <summary>
        /// 建成日期
        /// </summary>
        [Display(Name = "建成日期")]
        public DateTime BuildDate { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        [Display(Name = "房间数量")]
        public int RoomCount { get; set; }

        public List<BuildingGroupDepartmentModel> Departments { get; set; }
    }
}