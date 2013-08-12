using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 校区业务类
    /// </summary>
    public class MongoCampusBusiness : ICampusBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);
        #endregion //Field

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Campus ModelBind(BsonDocument doc)
        {
            Campus campus = new Campus();
            campus.Id = doc["id"].AsInt32;
            campus.Name = doc["name"].AsString;
            campus.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            campus.Remark = doc.GetValue("remark", "").AsString;
            campus.Status = doc.GetValue("status", 0).AsInt32;

            if (doc.Contains("editor"))
            {
                BsonDocument editor = doc["editor"].AsBsonDocument;
                campus.Editor.Id = editor["id"].AsObjectId.ToString();
                campus.Editor.Name = editor["name"].AsString;
                campus.Editor.Time = editor["time"].AsBsonDateTime.ToLocalTime();
                campus.Editor.Type = editor["type"].AsInt32;
            }

            return campus;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 得到校区列表
        /// </summary>
        /// <returns></returns>
        public List<Campus> GetList()
        {
            List<Campus> campusList = new List<Campus>();
            List<BsonDocument> docs = this.context.FindAll(EstateCollection.Campus);

            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Campus campus = ModelBind(doc);
                campusList.Add(campus);
            }

            return campusList;
        }

        /// <summary>
        /// 得到校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public Campus Get(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Campus, "id", id);

            if (doc != null)
            {
                Campus campus = ModelBind(doc);
                if (campus.Status == 1)
                    return null;

                return campus;
            }
            else
                return null;
        }

        /// <summary>
        /// 得到校区名称
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Campus, "id", id);

            if (doc != null)
                return doc["name"].AsString;
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取校区总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.context.Count(EstateCollection.Campus, query);
            return (int)count;
        }
        #endregion //Method
    }
}
