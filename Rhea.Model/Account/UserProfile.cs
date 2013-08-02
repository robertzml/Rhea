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
    /// 用户模型
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// 用户系统ID
        /// </summary>
        [BsonId]
        [Display(Name = "用户系统ID")]
        public ObjectId _id { get; set; }

        /// <summary>
        /// 用户系统ID,字符串格式
        /// </summary>
        [Display(Name = "用户系统ID")]
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        /// <remarks>工号,学号,系统序号</remarks>
        [Display(Name = "用户ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }       

        /// <summary>
        /// 用户组ID
        /// </summary>
        [UIHint("UserGroupDropDownList")]
        [Display(Name = "用户组ID")]
        public int UserGroupId { get; set; }

        /// <summary>
        /// 用户组名称
        /// </summary>
        [Display(Name = "用户组名称")]
        public string UserGroupName { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        [Display(Name = "上次登录时间")]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 本次登录时间
        /// </summary>
        [Display(Name = "本次登录时间")]
        public DateTime CurrentLoginTime { get; set; }

        /// <summary>
        /// 是否系统分配帐号
        /// </summary>
        /// <remarks>False:来自学校统一认证</remarks>
        [Display(Name = "是否系统分配帐号")]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 关联部门ID
        /// </summary>
        [UIHint("DepartmentDropDownList")]
        [Display(Name = "关联部门")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [Display(Name = "头像")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 系统状态
        /// </summary>
        /// <remarks>
        /// 0:正常, 1:删除, 2:禁用
        /// </remarks>
        public int Status { get; set; }
    }
}
