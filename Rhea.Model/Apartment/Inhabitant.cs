using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Apartment
{
    /// <summary>
    /// 住户类
    /// </summary>
    [CollectionName("inhabitant")]
    public class Inhabitant : MongoEntity
    {
        #region Property
        /// <summary>
        /// 工号、学号或其它
        /// </summary>
        [BsonElement("inhabitantId")]
        [Display(Name = "工号")]
        public string InhabitantId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [BsonElement("name")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 住户类型
        /// </summary>
        [BsonElement("type")]
        [Display(Name = "住户类型")]
        public int Type { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [BsonElement("departmentId")]
        [Display(Name = "所属部门")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [BsonElement("duty")]
        [Display(Name = "职务")]
        public string Duty { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Property
    }
}
