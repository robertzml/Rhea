using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Rhea.Data.Mongo
{
    /// <summary>
    /// 通用Repository
    /// 返回BsonDocument
    /// </summary>
    public class BsonRepository
    {
        #region Method
        /// <summary>
        /// 获取房间功能编码
        /// </summary>
        /// <returns></returns>
        public BsonDocument GetRoomFunctionCodes()
        {
            MongoRepository repository = new MongoRepository(RheaServer.RheaDatabase);
            repository.SetCollection("dictionary");

            var query = Query.EQ("name", "RoomFunctionCode");
            var doc = repository.Collection.FindOne(query);

            return doc;
        }
        #endregion //Method
    }
}
