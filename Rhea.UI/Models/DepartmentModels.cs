using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rhea.UI.Models
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public enum DepartmentType
    {
        [Display(Name = "教学院系")]
        Type1 = 1,

        [Display(Name = "科研机构")]
        Type2,

        [Display(Name = "公共服务")]
        Type3,

        [Display(Name = "党务部门")]
        Type4,

        [Display(Name = "行政单位")]
        Type5,

        [Display(Name = "附(直)属单位")]
        Type6,

        [Display(Name = "后勤部门")]
        Type7,

        [Display(Name = "校办产业")]
        Type8,

        [Display(Name = "其它")]
        Type9,
    }

    /// <summary>
    /// 部门分楼宇模型
    /// </summary>
    public class DepartmentBuildingModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int BuildingId { get; set; }

        public string BuildingName { get; set; }

        public int RoomCount { get; set; }

        public double TotalUsableArea { get; set; }
    }
}