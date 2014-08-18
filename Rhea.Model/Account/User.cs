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
    /// 用户类
    /// </summary>
    [CollectionName("user")]
    public class User : MongoEntity
    {
        #region Property
        /// <summary>
        /// 用户ID
        /// </summary>
        /// <remarks>工号,学号,系统序号</remarks>
        [BsonElement("userId")]
        [Display(Name = "用户ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [BsonElement("userName")]
        [Display(Name = "登录名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [BsonElement("password")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [BsonElement("name")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 用户组ID
        /// </summary>
        [BsonElement("userGroupId")]
        [UIHint("UserGroupDropDownList")]
        [Display(Name = "用户组")]
        public int UserGroupId { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("lastLoginTime")]
        [Display(Name = "上次登录时间")]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 本次登录时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("currentLoginTime")]
        [Display(Name = "本次登录时间")]
        public DateTime CurrentLoginTime { get; set; }

        /// <summary>
        /// 是否系统分配帐号
        /// </summary>
        /// <remarks>False:来自学校统一认证</remarks>
        [BsonElement("isSystem")]
        [Display(Name = "是否系统分配帐号")]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 关联部门ID
        /// </summary>
        [BsonElement("departmentId")]
        [UIHint("DepartmentDropDownList")]
        [Display(Name = "关联部门")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 头像地址
        /// 256x256
        /// </summary>
        [BsonElement("avatarUrl")]
        [Display(Name = "头像")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 大头像
        /// 128x128
        /// </summary>
        [BsonElement("avatarLarge")]
        [Display(Name = "大头像")]
        public string AvatarLarge { get; set; }

        /// <summary>
        /// 中头像
        /// 64x64
        /// </summary>
        [BsonElement("avatarMedium")]
        [Display(Name = "中头像")]
        public string AvatarMedium { get; set; }

        /// <summary>
        /// 小头像
        /// 32x32
        /// </summary>
        [BsonElement("avatarSmall")]
        [Display(Name = "小头像")]
        public string AvatarSmall { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataType(DataType.MultilineText)]
        [BsonElement("remark")]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 系统状态
        /// </summary>
        /// <remarks>
        /// 0:正常, 1:删除, 2:禁用
        /// </remarks>
        [BsonElement("status")]
        [Display(Name = "用户状态")]
        public int Status { get; set; }
        #endregion //Property
    }
}
