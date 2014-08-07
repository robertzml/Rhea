using System;
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

        #region Function
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="_id">用户系统ID</param>
        /// <param name="last">上次登录时间</param>
        /// <param name="current">本次登录时间</param>
        private void UpdateLoginTime(User user, DateTime last, DateTime current)
        {
            user.LastLoginTime = last;
            user.CurrentLoginTime = current;

            this.userRepository.Update(user);

            return;
        }
        #endregion //Function

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

            UpdateLoginTime(user, user.CurrentLoginTime, DateTime.Now);

            return ErrorCode.Success;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return this.userRepository.Get().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="_id">用户系统ID</param>
        /// <returns></returns>
        public User Get(string _id)
        {
            return this.userRepository.Get(_id);
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
        /// 添加用户
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <returns></returns>
        public ErrorCode Create(User data)
        {
            data.Password = Hasher.SHA1Encrypt(data.Password);
            data.IsSystem = true;
            data.LastLoginTime = DateTime.Now;
            data.CurrentLoginTime = DateTime.Now;
            data.DepartmentId = 0;
            return this.userRepository.Create(data);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <returns></returns>
        public ErrorCode Update(User data)
        {
            var user = this.userRepository.Get(data._id);

            user.Name = data.Name;
            user.UserGroupId = data.UserGroupId;
            user.DepartmentId = data.DepartmentId;
            user.Remark = data.Remark;

            if (!string.IsNullOrEmpty(data.Password))
            {
                user.Password = Hasher.SHA1Encrypt(data.Password);
            }

            return this.userRepository.Update(user);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public ErrorCode ChangePassword(User data, string oldPassword, string newPassword)
        {
            var user = this.userRepository.Get(data._id);

            if (user.Password != Hasher.SHA1Encrypt(oldPassword))
                return ErrorCode.WrongPassword;

            user.Password = Hasher.SHA1Encrypt(newPassword);

            return this.userRepository.Update(user);
        }

        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <remarks>不包括Root</remarks>
        /// <returns></returns>
        public IEnumerable<UserGroup> GetUserGroup()
        {
            return this.userGroupRepository.Get().Where(r => r.Name != "Root");
        }

        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <param name="isRoot">是否Root</param>
        /// <returns></returns>
        public IEnumerable<UserGroup> GetUserGroup(bool isRoot)
        {
            if (isRoot)
                return this.userGroupRepository.Get();
            else
                return GetUserGroup();
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

        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="data">用户组对象</param>
        /// <returns></returns>
        public ErrorCode CreateUserGroup(UserGroup data)
        {
            data.Status = 0;
            return this.userGroupRepository.Create(data);
        }
        #endregion //Method
    }
}
