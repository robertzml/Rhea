using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Server;

namespace Rhea.Business.Estate
{
    public class EstateArchiveBusiness : IArchiveBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);
        #endregion //Field

        #region Method
        /// <summary>
        /// 归档数据
        /// </summary>
        /// <param name="collectionName">collection名称</param>
        /// <param name="docs">数据</param>
        /// <returns></returns>
        public bool Archive(string collectionName, IEnumerable<BsonDocument> docs)
        {
            IEnumerable<WriteConcernResult> result = this.context.InsertBatch(collectionName, docs);

            if (result.All(r => r.Ok == true))
                return true;
            else
                return false;
        }

        public void GetGroup(string collectionName)
        {
            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "$editor.time" }
                    }}
                }
            };


        }
        #endregion //Method
    }
}
