﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 校区类
    /// </summary>
    public class Campus
    {
        #region Constructor
        public Campus()
        {
            this.Log = new Log();
        }
        #endregion //Constructor

        #region Property
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 日志属性
        /// </summary>
        [Display(Name = "日志属性")]
        public Log Log { get; set; }
        #endregion //Property
    }
}
