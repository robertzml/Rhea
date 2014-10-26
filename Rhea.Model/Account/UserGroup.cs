using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rhea.Model.Account
{
    /// <summary>
    /// 用户组类
    /// </summary>
    [CollectionName("userGroup")]
    public class UserGroup : MongoEntity
    {
        #region Constructor
        public UserGroup()
        {
            this.UserGroupPrivilege = new List<UserGroupPrivilege>();
        }
        #endregion //Constructor

        #region Property
        /// <summary>
        /// 用户组ID
        /// </summary>
        [BsonElement("id")]
        [Display(Name = "用户组ID")]
        public int UserGroupId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [BsonElement("name")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [BsonElement("title")]
        [Display(Name = "显示名称")]
        public string Title { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [BsonElement("rank")]
        [Display(Name = "级别")]
        public int Rank { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [BsonElement("remark")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 用户组权限
        /// </summary>
        [BsonElement("userGroupPrivilege")]
        [Display(Name = "用户组权限")]
        public List<UserGroupPrivilege> UserGroupPrivilege { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <remarks>
        /// 0:正常，1:删除
        /// </remarks>
        [BsonElement("status")]
        [Display(Name = "状态")]
        public int Status { get; set; }
        #endregion //Property
    }

    /// <summary>
    /// 用户组相关权限
    /// </summary>
    public class UserGroupPrivilege
    {
        #region Method
        /// <summary>
        /// ID
        /// </summary>
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Display(Name = "ID")]
        public string _id { get; set; }

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
        #endregion //Method
    }
}
