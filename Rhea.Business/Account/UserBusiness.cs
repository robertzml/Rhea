﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Account;
using Rhea.Data.Mongo.Account;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Common;

namespace Rhea.Business.Account
{
    /// <summary>
    /// 用户业务类
    /// </summary>
    public class UserBusiness
    {
        #region Field
        /// <summary>
        /// 用户Repository
        /// </summary>
        private IUserRepository userRepository;

        /// <summary>
        /// 用户组Repository
        /// </summary>
        public IUserGroupRepository userGroupRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 用户业务类
        /// </summary>
        public UserBusiness()
        {
            this.userRepository = new MongoUserRepository();
            this.userGroupRepository = new MongoUserGroupRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public ErrorCode Login(string userName, string password)
        {
            User user = this.userRepository.GetByUserName(userName);

            if (user == null)
                return ErrorCode.UserNotExist;

            if (user.Password != Hasher.SHA1Encrypt(password))
                return ErrorCode.WrongPassword;

            return ErrorCode.Success;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <returns></returns>
        public User GetByUserName(string userName)
        {
            return this.userRepository.GetByUserName(userName);
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="id">用户组ID</param>
        /// <returns></returns>
        public UserGroup GetUserGroup(int id)
        {
            return this.userGroupRepository.Get(id);
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="name">用户组名称</param>
        /// <returns></returns>
        public UserGroup GetUserGroup(string name)
        {
            return this.userGroupRepository.Get(name);
        }
        #endregion //Method
    }
}
