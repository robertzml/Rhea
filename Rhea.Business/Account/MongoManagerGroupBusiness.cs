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
    /// 管理组业务
    /// </summary>
    public class MongoManagerGroupBusiness : IManagerGroupBusiness
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
        private ManagerGroup ModelBind(BsonDocument doc)
        {
            ManagerGroup group = new ManagerGroup();
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
        /// 获取管理组列表
        /// </summary>
        /// <returns></returns>
        public List<ManagerGroup> GetList()
        {
            List<ManagerGroup> data = new List<ManagerGroup>();
            List<BsonDocument> docs = this.context.Find(RheaCollection.UserGroup, "type", 1);

            foreach (BsonDocument doc in docs)
            {
                ManagerGroup g = ModelBind(doc);
                data.Add(g);
            }

            return data.OrderBy(r => r.Id).ToList();
        }

        /// <summary>
        /// 获取管理组
        /// </summary>
        /// <param name="id">管理组ID</param>
        /// <returns></returns>
        public ManagerGroup Get(int id)
        { 
            BsonDocument doc = this.context.FindOne(RheaCollection.UserGroup, "id", id);

            if (doc != null)
            {
                ManagerGroup group = ModelBind(doc);
                return group;
            }
            else
                return null;
        }

        /// <summary>
        /// 管理组编辑
        /// </summary>
        /// <param name="data">管理组数据</param>
        /// <returns></returns>
        public bool Edit(ManagerGroup data)
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
