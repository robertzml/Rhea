using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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
        /// <param name="showRoot">显示Root</param>
        /// <returns></returns>
        public List<UserGroup> GetList(bool showRoot = true)
        {
            List<UserGroup> data = new List<UserGroup>();
            var docs = this.context.FindAll(RheaCollection.UserGroup);

            foreach (BsonDocument doc in docs)
            {
                UserGroup userGroup = ModelBind(doc);
                if (!showRoot && userGroup.Name == "Root")
                    continue;
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

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="name">用户组名称</param>
        /// <returns></returns>
        public UserGroup Get(string name)
        {
            BsonDocument doc = this.context.FindOne(RheaCollection.UserGroup, "name", name);

            if (doc != null)
            {
                UserGroup group = ModelBind(doc);
                return group;
            }
            else
                return null;
        }

        /// <summary>
        /// 用户组添加
        /// </summary>
        /// <param name="data">用户组数据</param>
        /// <returns></returns>
        public int Create(UserGroup data)
        {
            bool dup = this.context.CheckDuplicateId(RheaCollection.UserGroup, data.Id);
            if (dup)
                return 0;

            BsonDocument doc = new BsonDocument
            {
                { "id", data.Id },
                { "name", data.Name },
                { "title", data.Title },
                { "rank", data.Rank },              
                { "remark", data.Remark ?? "" },
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(RheaCollection.UserGroup, doc);

            if (result.HasLastErrorMessage)
                return 0;
            else
                return data.Id;
        }

        /// <summary>
        /// 用户组编辑
        /// </summary>
        /// <param name="data">用户组数据</param>
        /// <returns></returns>
        public bool Edit(UserGroup data)
        {
            var query = Query.EQ("id", data.Id);

            var update = Update.Set("name", data.Name)
                .Set("title", data.Title)
                .Set("rank", data.Rank)
                .Set("remark", data.Remark ?? "");

            WriteConcernResult result = this.context.Update(RheaCollection.UserGroup, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }
        #endregion //Method
    }
}
