using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Server;
using Rhea.Model.Account;

namespace Rhea.Business.Account
{
    /// <summary>
    /// 用户组业务
    /// </summary>
    public class MongoUserGroupBusiness : IUserGroupBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);
        #endregion //Field

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private UserGroup ModelBind(BsonDocument doc)
        {
            UserGroup group = new UserGroup();
            group.Id = doc["id"].AsInt32;
            group.Name = doc["name"].AsString;
            group.Title = doc["title"].AsString;
            group.Rank = doc["rank"].AsInt32;
            group.Remark = doc.GetValue("remark", "").AsString;
            group.Status = doc.GetValue("status", 0).AsInt32;

            return group;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        public List<UserGroup> GetList()
        {
            List<UserGroup> data = new List<UserGroup>();
            List<BsonDocument> docs = this.context.Find(RheaCollection.UserGroup, "type", 2);

            foreach (BsonDocument doc in docs)
            {
                UserGroup userGroup = ModelBind(doc);
                data.Add(userGroup);
            }

            return data.OrderBy(r => r.Id).ToList();
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="id">用户组ID</param>
        /// <returns></returns>
        public UserGroup Get(int id)
        {
            BsonDocument doc = this.context.FindOne(RheaCollection.UserGroup, "id", id);

            if (doc != null)
            {
                UserGroup group = ModelBind(doc);
                return group;
            }
            else
                return null;
        }
        #endregion //Method
    }
}
