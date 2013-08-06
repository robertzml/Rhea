﻿using System;
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
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);
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
            var query = Query.EQ("_id", _id);
            var update = Update.Set("lastLoginTime", last)
                .Set("currentLoginTime", current);

            WriteConcernResult result = this.context.Update(RheaCollection.User, query, update);

            return;
        }

        /// <summary>
        /// 得到用户组信息
        /// </summary>
        /// <param name="user">用户</param>
        private void GetGroupInfo(ref UserProfile user)
        {
            IUserGroupBusiness userGroupBusiness = new MongoUserGroupBusiness();
            if (user.UserGroupId != 0)
            {
                UserGroup uGroup = userGroupBusiness.Get(user.UserGroupId);
                if (uGroup != null)
                    user.UserGroupName = uGroup.Name;
                else
                    user.UserGroupName = "Null";
            }
            else
                user.UserGroupName = "Null";
        }

        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private UserProfile ModelBind(BsonDocument doc)
        {
            UserProfile user = new UserProfile();
            user._id = doc["_id"].AsObjectId;
            user.Id = user._id.ToString();
            user.UserName = doc["userName"].AsString;
            user.UserId = doc.GetValue("userId", "").AsString;
            user.UserGroupId = doc["userGroupId"].AsInt32;          
            user.LastLoginTime = doc.GetValue("lastLoginTime", DateTime.Now).ToLocalTime();
            user.CurrentLoginTime = doc.GetValue("currentLoginTime", DateTime.Now).ToLocalTime();
            user.IsSystem = doc.GetValue("isSystem", false).AsBoolean;
            user.DepartmentId = doc.GetValue("departmentId", 0).AsInt32;
            user.Status = doc.GetValue("status", 0).AsInt32;

            GetGroupInfo(ref user);

            return user;
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
            BsonDocument doc = this.context.FindOne(RheaCollection.User, "userName", userName);
            if (doc != null)
            {
                if (doc.GetValue("status", 0) == 1)
                    return null;

                string pass = doc["password"].AsString;
                if (Hasher.SHA1Encrypt(password) != pass)
                    return null;

                UserProfile user = new UserProfile();
                user._id = doc["_id"].AsObjectId;
                user.UserName = userName;
                user.UserId = doc.GetValue("userId", "").AsString;
                user.UserGroupId = doc.GetValue("userGroupId", 0).AsInt32;           
                user.LastLoginTime = doc.GetValue("currentLoginTime", DateTime.Now).ToLocalTime();
                user.CurrentLoginTime = DateTime.Now;
                user.DepartmentId = doc.GetValue("departmentId", 0).AsInt32;
                user.Status = doc.GetValue("status", 0).AsInt32;

                GetGroupInfo(ref user);

                UpdateLoginTime(user._id, user.LastLoginTime, user.CurrentLoginTime);

                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        public UserProfile Get(string id)
        {
            try
            {
                ObjectId _id = new ObjectId(id);

                BsonDocument doc = this.context.FindByKey(RheaCollection.User, _id);
                if (doc != null)
                {
                    UserProfile user = ModelBind(doc);
                    return user;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public UserProfile GetByUserName(string userName)
        {
            BsonDocument doc = this.context.FindOne(RheaCollection.User, "userName", userName);
            if (doc != null)
            {
                UserProfile user = ModelBind(doc);
                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public List<UserProfile> GetList()
        {
            List<UserProfile> data = new List<UserProfile>();
            List<BsonDocument> docs = this.context.FindAll(RheaCollection.User);

            foreach (BsonDocument doc in docs)
            {
                UserProfile g = ModelBind(doc);
                data.Add(g);
            }

            return data.OrderBy(r => r.UserId).ToList();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userGroupId">用户组ID</param>
        /// <returns></returns>
        public List<UserProfile> GetList(int userGroupId)
        {
            List<UserProfile> data = new List<UserProfile>();
            List<BsonDocument> docs = this.context.Find(RheaCollection.User, "userGroupId", userGroupId);

            foreach (BsonDocument doc in docs)
            {
                UserProfile g = ModelBind(doc);
                data.Add(g);
            }

            return data.OrderBy(r => r.UserId).ToList();
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns></returns>
        public string Create(UserProfile data)
        {
            bool dup = this.context.CheckDuplicate(RheaCollection.User, "userId", data.UserId);
            if (dup)
                return string.Empty;

            dup = this.context.CheckDuplicate(RheaCollection.User, "userName", data.UserName);
            if (dup)
                return string.Empty;

            BsonDocument doc = new BsonDocument
            {
                { "userId", data.UserId },
                { "userName", data.UserName },
                { "password", Hasher.SHA1Encrypt(data.Password) },             
                { "userGroupId", data.UserGroupId },
                { "isSystem", true },           
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(RheaCollection.User, doc);

            if (result.HasLastErrorMessage)
                return string.Empty;
            else
                return data._id.ToString();
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns></returns>
        public bool Edit(UserProfile data)
        {
            try
            {
                data._id = new ObjectId(data.Id);
                var query = Query.EQ("_id", data._id);

                IMongoUpdate update;

                if (string.IsNullOrEmpty(data.Password))
                {
                    update = Update.Set("userGroupId", data.UserGroupId)
                        .Set("departmentId", (BsonValue)data.DepartmentId);
                }
                else
                {
                    update = Update.Set("userGroupId", data.UserGroupId)
                        .Set("password", Hasher.SHA1Encrypt(data.Password))
                        .Set("departmentId", (BsonValue)data.DepartmentId);
                }               

                WriteConcernResult result = this.context.Update(RheaCollection.User, query, update);

                if (result.HasLastErrorMessage)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool ValidatePassword(string userName, string password)
        {            
            BsonDocument doc = this.context.FindOne(RheaCollection.User, "userName", userName);
            if (doc != null)
            {
                string pass = doc["password"].AsString;
                if (Hasher.SHA1Encrypt(password) != pass)
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
            var query = Query.EQ("userName", userName);
            var update = Update.Set("password", Hasher.SHA1Encrypt(newPassword));

            WriteConcernResult result = this.context.Update(RheaCollection.User, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        public bool Disable(string id)
        {
            try
            {
                ObjectId oid = new ObjectId(id);

                var query = Query.EQ("_id", oid);
                var update = Update.Set("status", 2);

                WriteConcernResult result = this.context.Update(RheaCollection.User, query, update);

                if (result.HasLastErrorMessage)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        public bool Enable(string id)
        {
            try
            {
                ObjectId oid = new ObjectId(id);

                var query = Query.EQ("_id", oid);
                var update = Update.Set("status", 0);

                WriteConcernResult result = this.context.Update(RheaCollection.User, query, update);

                if (result.HasLastErrorMessage)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion //Method
    }
}
