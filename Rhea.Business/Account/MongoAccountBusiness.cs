using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Common;
using Rhea.Data.Server;
using Rhea.Model.Account;

namespace Rhea.Business.Account
{
    /// <summary>
    /// 账户业务
    /// </summary>
    public class MongoAccountBusiness : IAccountBusiness
    {
        #region Field
        #endregion //Field

        #region Function
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="_id">用户系统ID</param>
        /// <param name="last">上次登录时间</param>
        /// <param name="current">本次登录时间</param>
        private void UpdateLoginTime(ObjectId _id, DateTime last, DateTime current)
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);

            var query = Query.EQ("_id", _id);
            var update = Update.Set("lastLoginTime", last)
                .Set("currentLoginTime", current);

            WriteConcernResult result = context.Update(RheaCollection.User, query, update);

            return;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserProfile Login(string userName, string password)
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);

            BsonDocument doc = context.FindOne(RheaCollection.User, "userName", userName);
            if (doc != null)
            {
                string pass = doc["password"].AsString;
                if (Hasher.MD5Encrypt(password) != pass)
                    return null;

                UserProfile user = new UserProfile();
                user._id = doc["_id"].AsObjectId;
                user.UserName = userName;
                user.UserId = doc.GetValue("userId", "").AsString;
                //user.UserGroupId = doc["userGroupId"].AsInt32;
                //user.ManagerGroupId = doc["managerGroupId"].AsInt32;
                user.LastLoginTime = doc.GetValue("currentLoginTime", DateTime.Now).ToLocalTime();
                user.CurrentLoginTime = DateTime.Now;

                /*IAdminService adminService = new MongoAdminService();
                if (user.ManagerGroupId != 0)
                {
                    ManagerGroup mGroup = adminService.GetManagerGroup(user.ManagerGroupId);
                    user.ManagerGroupName = mGroup.Name;
                }
                else
                    user.ManagerGroupName = "Null";
                if (user.UserGroupId != 0)
                {
                    UserGroup uGroup = adminService.GetUserGroup(user.UserGroupId);
                    user.UserGroupName = uGroup.Name;
                }
                else
                    user.UserGroupName = "Null";*/

                UpdateLoginTime(user._id, user.LastLoginTime, user.CurrentLoginTime);

                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public UserProfile Get(string userName)
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);

            BsonDocument doc = context.FindOne(RheaCollection.User, "userName", userName);
            if (doc != null)
            {
                UserProfile user = new UserProfile();
                user._id = doc["_id"].AsObjectId;
                user.UserName = userName;
                user.UserId = doc.GetValue("userId", "").AsString;
                //user.UserGroupId = doc["userGroupId"].AsInt32;
                //user.ManagerGroupId = doc["managerGroupId"].AsInt32;
                user.LastLoginTime = doc.GetValue("lastLoginTime", DateTime.Now).ToLocalTime();
                user.CurrentLoginTime = doc.GetValue("currentLoginTime", DateTime.Now).ToLocalTime();
                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool ValidatePassword(string userName, string password)
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);

            BsonDocument doc = context.FindOne(RheaCollection.User, "userName", userName);
            if (doc != null)
            {
                string pass = doc["password"].AsString;
                if (Hasher.MD5Encrypt(password) != pass)
                    return false;
                else
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);

            var query = Query.EQ("userName", userName);
            var update = Update.Set("password", Hasher.MD5Encrypt(newPassword));

            WriteConcernResult result = context.Update(RheaCollection.User, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }
        #endregion //Method
    }
}
