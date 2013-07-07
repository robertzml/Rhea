using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 房间功能编码
    /// </summary>
    public class RoomFunctionCode
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string CodeId { get; set; }

        /// <summary>
        /// 一级编码
        /// </summary>
        public int FirstCode { get; set; }

        /// <summary>
        /// 二级编码
        /// </summary>
        public int SecondCode { get; set; }

        /// <summary>
        /// 功能属性
        /// </summary>
        public string FunctionProperty { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
