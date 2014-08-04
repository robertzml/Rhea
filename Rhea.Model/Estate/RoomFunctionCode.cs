using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Estate
{
    /// <summary>
    /// 房间功能编码类
    /// </summary>
    public class RoomFunctionCode
    {
        /// <summary>
        /// 编码
        /// </summary>
        [BsonElement("codeId")]
        [Display(Name = "编码")]
        public string CodeId { get; set; }

        /// <summary>
        /// 一级编码
        /// </summary>
        [BsonElement("firstCode")]
        [Display(Name = "一级编码")]
        public int FirstCode { get; set; }

        /// <summary>
        /// 二级编码
        /// </summary>
        [BsonElement("secondCode")]
        [Display(Name = "二级编码")]
        public int SecondCode { get; set; }

        /// <summary>
        /// 一级分类名称
        /// </summary>
        [BsonElement("classifyName")]
        [Display(Name = "一级分类名称")]
        public string ClassifyName { get; set; }

        /// <summary>
        /// 功能属性
        /// </summary>
        [BsonElement("functionProperty")]
        [Display(Name = "功能属性")]
        public string FunctionProperty { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}
