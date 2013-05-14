using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Entities;
using Rhea.Common;

namespace Rhea.Business
{
    /// <summary>
    /// 账户业务
    /// </summary>
    public class MongoAccountService : IAccountService
    {
        #region Field
        #endregion //Field

        #region Function
        /// <summary>
        /// 更新登录时间
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="last">上次登录时间</param>
        /// <param name="current">本次登录时间</param>
        private void UpdateLoginTime(int userId, DateTime last, DateTime current)
        {
            RheaMongoContext context = new RheaMongoContext(RheaConstant.EstateDatabase);

            var query = Query.EQ("id", userId);
            var update = Update.Set("lastLoginTime", last)
                .Set("currentLoginTime", current);

            WriteConcernResult result = context.Update(EstateCollection.User, query, update);

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
            RheaMongoContext context = new RheaMongoContext(RheaConstant.EstateDatabase);

            BsonDocument doc = context.FindOne(EstateCollection.User, "userName", userName);
            if (doc != null)
            {
                string pass = doc["password"].AsString;
                if (Hasher.MD5Encrypt(password) != pass)
                    return null;

                UserProfile user = new UserProfile();
                user.Id = doc["id"].AsInt32;
                user.UserName = userName;
                user.UserGroupId = doc["userGroupId"].AsInt32;
                user.ManagerGroupId = doc["managerGroupId"].AsInt32;
                user.LastLoginTime = doc.GetValue("currentLoginTime", DateTime.Now).ToLocalTime();
                user.CurrentLoginTime = DateTime.Now;

                IAdminService adminService = new MongoAdminService();
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
                    user.UserGroupName = "Null";

                UpdateLoginTime(user.Id, user.LastLoginTime, user.CurrentLoginTime);

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
            RheaMongoContext context = new RheaMongoContext(RheaConstant.EstateDatabase);

            BsonDocument doc = context.FindOne(EstateCollection.User, "userName", userName);
            if (doc != null)
            { 
                UserProfile user = new UserProfile();
                user.Id = doc["id"].AsInt32;
                user.UserName = userName;
                user.UserGroupId = doc["userGroupId"].AsInt32;
                user.ManagerGroupId = doc["managerGroupId"].AsInt32;
                user.LastLoginTime = doc.GetValue("lastLoginTime", DateTime.Now).ToLocalTime();
                user.CurrentLoginTime = doc.GetValue("currentLoginTime", DateTime.Now).ToLocalTime();
                return user;
            }
            else
                return null;
        }
        #endregion //Method        
    }
}
