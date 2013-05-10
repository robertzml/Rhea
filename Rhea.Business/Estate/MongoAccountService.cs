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

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 账户业务
    /// </summary>
    public class MongoAccountService : IAccountService
    {
        #region Field
        /// <summary>
        /// Collection名称
        /// </summary>
        private string collectionName = "user";
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

            WriteConcernResult result = context.Update(this.collectionName, query, update);

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

            BsonDocument doc = context.FindOne(this.collectionName, "userName", userName);
            if (doc != null)
            {
                string pass = doc["password"].AsString;
                if (Hasher.MD5Encrypt(password) != pass)
                    return null;

                UserProfile user = new UserProfile();
                user.Id = doc["id"].AsInt32;
                user.UserName = userName;
                user.UserType = doc["userType"].AsInt32;
                user.ManagerType = doc["managerType"].AsInt32;
                user.LastLoginTime = doc.GetValue("currentLoginTime", DateTime.Now).ToLocalTime();
                user.CurrentLoginTime = DateTime.Now;

                UpdateLoginTime(user.Id, user.LastLoginTime, user.CurrentLoginTime);

                return user;
            }
            else
                return null;
        }

        public UserProfile Get(string userName)
        {
            RheaMongoContext context = new RheaMongoContext(RheaConstant.EstateDatabase);

            BsonDocument doc = context.FindOne(this.collectionName, "userName", userName);
            if (doc != null)
            { 
                UserProfile user = new UserProfile();
                user.Id = doc["id"].AsInt32;
                user.UserName = userName;
                user.UserType = doc["userType"].AsInt32;
                user.ManagerType = doc["managerType"].AsInt32;
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
