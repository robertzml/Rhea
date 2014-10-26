using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Rhea.Model.Account
{
    /// <summary>
    /// 权限类
    /// </summary>
    [CollectionName("privilege")]
    public class Privilege : MongoEntity
    {
        #region Method
        /// <summary>
        /// 标题
        /// </summary>
        [BsonElement("title")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 所需级别
        /// </summary>
        /// <remarks>
        /// 该权限项要求最低级别
        /// </remarks>
        [BsonElement("rank")]
        [Display(Name = "所需级别")]
        public int Rank { get; set; }

        /// <summary>
        /// 是否写入
        /// </summary>
        /// <remarks>
        /// 表示对应操作是否要修改数据库
        /// </remarks>
        [UIHint("Boolean2")]
        [BsonElement("isWrite")]
        [Display(Name = "是否写入")]
        public bool IsWrite { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Method
    }
}
