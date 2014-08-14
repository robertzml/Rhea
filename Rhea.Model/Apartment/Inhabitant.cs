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
        [BsonElement("jobNumber")]
        [Display(Name = "工号")]
        public string JobNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [BsonElement("name")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 住户类型
        /// 1:教职工；2:外聘人员；3:挂职；4:学生；5:其他
        /// </summary>
        [BsonElement("type")]
        [Display(Name = "住户类型")]
        public int Type { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [BsonElement("departmentName")]
        [Display(Name = "所属部门")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [BsonElement("duty")]
        [Display(Name = "职务")]
        public string Duty { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [BsonElement("telephone")]
        [Display(Name = "电话")]
        public string Telephone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [BsonElement("identityCard")]
        [Display(Name = "身份证")]
        public string IdentityCard { get; set; }

        /// <summary>
        /// 公积金领取情况
        /// </summary>
        [BsonElement("extractAccumulatedFunds")]
        [Display(Name = "公积金提取情况")]
        public bool? ExtractAccumulatedFunds { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        [BsonElement("marriage")]
        [Display(Name = "婚姻状况")]
        public string Marriage { get; set; }

        /// <summary>
        /// 蠡湖家园入住情况
        /// </summary>
        [BsonElement("liHuStatus")]
        [Display(Name = "蠡湖家园入住情况")]
        public string LiHuStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 0:正常居住；1:删除；55:离开；56:延期居住
        /// </remarks>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Property
    }
}
