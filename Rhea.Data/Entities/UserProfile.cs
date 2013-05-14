﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Entities
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 管理组ID
        /// </summary>
        public int ManagerGroupId { get; set; }

        /// <summary>
        /// 管理组名称
        /// </summary>
        public string ManagerGroupName { get; set; }

        /// <summary>
        /// 用户组ID
        /// </summary>
        public int UserGroupId { get; set; }

        /// <summary>
        /// 用户组名称
        /// </summary>
        public string UserGroupName { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 本次登录时间
        /// </summary>
        public DateTime CurrentLoginTime { get; set; }
    }
}
