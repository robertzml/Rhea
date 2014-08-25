using Rhea.Common;
using Rhea.Data;
using Rhea.Data.Account;
using Rhea.Data.Mongo.Account;
using Rhea.Model;
using Rhea.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private IUserGroupRepository userGroupRepository;

        /// <summary>
        /// Root用户组ID
        /// </summary>
        private int rootGroupId = 100001;

        /// <summary>
        /// Root用户组名称
        /// </summary>
        private string rootGroupName = "Root";
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

            if (user.Status == (int)EntityStatus.Deleted)
                return ErrorCode.ObjectDeleted;

            if (user.Status == (int)EntityStatus.UserDisable)
                return ErrorCode.UserDisabled;

            UpdateLoginTime(user, user.CurrentLoginTime, DateTime.Now);

            return ErrorCode.Success;
        }

        #region User
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return this.userRepository.Get().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="isRoot"></param>
        /// <returns></returns>
        public IEnumerable<User> Get(bool isRoot)
        {
            if (isRoot)
                return this.Get();
            else
                return this.userRepository.Get().Where(r => r.Status != 1 && r.UserGroupId != this.rootGroupId);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="_id">用户系统ID</param>
        /// <returns></returns>
        public User Get(string _id)
        {
            var data = this.userRepository.Get(_id);
            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <returns></returns>
        public User GetByUserName(string userName)
        {
            var data = this.userRepository.GetByUserName(userName);
            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 根据用户组获取用户
        /// </summary>
        /// <param name="groupId">用户组ID</param>
        /// <param name="isRoot">是否Root</param>
        /// <returns></returns>
        public IEnumerable<User> GetByGroup(int groupId, bool isRoot)
        {
            var group = this.userGroupRepository.Get(groupId);
            if (!isRoot && group.Name == "Root")
                return null;

            var data = this.userRepository.Get().Where(r => r.UserGroupId == groupId).Where(r => r.Status != 1);
            return data;
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
            data.AvatarUrl = "default.png";
            data.AvatarLarge = "default_128.png";
            data.AvatarMedium = "default_64.png";
            data.AvatarSmall = "default_32.png";
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
        /// 修改头像
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <param name="avatarUrl">新头像地址</param>
        /// <returns></returns>
        public ErrorCode ChangeAvatar(User data, string avatarUrl)
        {
            var user = this.userRepository.Get(data._id);
            user.AvatarUrl = avatarUrl;

            return this.userRepository.Update(user);
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <param name="avatarUrl">新头像地址</param>
        /// <param name="largeUrl">大头像</param>
        /// <param name="mediumUrl">中头像</param>
        /// <param name="smallUrl">小头像</param>
        /// <returns></returns>
        public ErrorCode ChangeAvatar(User data, string avatarUrl, string largeUrl, string mediumUrl, string smallUrl)
        {
            var user = this.userRepository.Get(data._id);
            user.AvatarUrl = avatarUrl;
            user.AvatarLarge = largeUrl;
            user.AvatarMedium = mediumUrl;
            user.AvatarSmall = smallUrl;

            return this.userRepository.Update(user);
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="_id">用户ID</param>
        public void Enable(string _id)
        {
            var user = this.userRepository.Get(_id);
            user.Status = (int)EntityStatus.Normal;
            this.userRepository.Update(user);
            return;
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="_id">用户ID</param>
        public void Disable(string _id)
        {
            var user = this.userRepository.Get(_id);
            if (user.UserGroupId == this.rootGroupId) //Root 不能禁用
                return;

            user.Status = (int)EntityStatus.UserDisable;
            this.userRepository.Update(user);
            return;
        }
        #endregion //User

        #region UserGroup
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <remarks>不包括Root</remarks>
        /// <returns></returns>
        public IEnumerable<UserGroup> GetUserGroup()
        {
            return this.userGroupRepository.Get().Where(r => r.Name != this.rootGroupName).OrderByDescending(r => r.Rank);
        }

        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <param name="isRoot">是否Root</param>
        /// <returns></returns>
        public IEnumerable<UserGroup> GetUserGroup(bool isRoot)
        {
            if (isRoot)
                return this.userGroupRepository.Get().OrderByDescending(r => r.Rank);
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

        /// <summary>
        /// 编辑用户组
        /// </summary>
        /// <param name="data">用户组对象</param>
        /// <returns></returns>
        public ErrorCode UpdateUserGroup(UserGroup data)
        {
            data.Status = 0;
            return this.userGroupRepository.Update(data);
        }
        #endregion //UserGroup
        #endregion //Method
    }
}
