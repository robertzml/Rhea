using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Entities;
using Rhea.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Rhea.Business
{
    /// <summary>
    /// 系统管理事务类
    /// </summary>
    public class MongoAdminService : IAdminService
    {/*
        #region Method
        /// <summary>
        /// 获取管理组列表
        /// </summary>
        /// <returns></returns>
        public List<ManagerGroup> GetManagerGroupList()
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);

            List<ManagerGroup> data = new List<ManagerGroup>();           
            List<BsonDocument> docs = context.Find(EstateCollection.UserGroup, "type", 1);

            foreach (BsonDocument doc in docs)
            {
                ManagerGroup g = new ManagerGroup
                {
                    Id = doc["id"].AsInt32,
                    Name = doc["name"].AsString,
                    Rank = doc["rank"].AsInt32
                };
                data.Add(g);
            }

            return data.OrderBy(r => r.Id).ToList();
        }

        /// <summary>
        /// 添加管理组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CreateManagerGroup(ManagerGroup data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="managerGroupId">管理组ID</param>
        /// <returns></returns>
        public ManagerGroup GetManagerGroup(int managerGroupId)
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);

            BsonDocument doc = context.FindOne(EstateCollection.UserGroup, "id", managerGroupId);

            if (doc != null)
            {
                ManagerGroup group = new ManagerGroup
                {
                    Id = doc["id"].AsInt32,
                    Name = doc["name"].AsString,
                    Rank = doc["rank"].AsInt32
                };

                return group;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        public List<UserGroup> GetUserGroupList()
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);

            List<UserGroup> data = new List<UserGroup>();
            List<BsonDocument> docs = context.Find(EstateCollection.UserGroup, "type", 2);

            foreach (BsonDocument doc in docs)
            {
                UserGroup userGroup = new UserGroup
                {
                    Id = doc["id"].AsInt32,
                    Name = doc["name"].AsString,
                    Rank = doc["rank"].AsInt32
                };
                data.Add(userGroup);
            }

            return data.OrderBy(r => r.Id).ToList();
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="userGroupId">用户组ID</param>
        /// <returns></returns>
        public UserGroup GetUserGroup(int userGroupId)
        {
            RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);

            BsonDocument doc = context.FindOne(EstateCollection.UserGroup, "id", userGroupId);

            if (doc != null)
            {
                UserGroup group = new UserGroup
                {
                    Id = doc["id"].AsInt32,
                    Name = doc["name"].AsString,
                    Rank = doc["rank"].AsInt32
                };

                return group;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public List<UserProfile> GetUserList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="groupId">组ID</param>
        /// <param name="type">1:管理组，2:用户组</param>
        /// <returns></returns>
        public List<UserProfile> GetUserList(int groupId, int type)
        {
            throw new NotImplementedException();
        }
        #endregion //Method        
      */
    }
}
