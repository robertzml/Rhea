using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 校区类
    /// </summary>
    public class Campus : CampusBase
    {
        #region Constructor
        /// <summary>
        /// 校区类
        /// </summary>
        public Campus()
        {
            this.Log = new Log();
        }
        #endregion //Constructor

        #region Field
        /// <summary>
        /// 日志属性
        /// </summary>
        [Display(Name = "日志属性")]
        public Log Log { get; set; }
        #endregion //Field
    }
}
