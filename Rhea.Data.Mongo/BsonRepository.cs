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

        /// <summary>
        /// 获取最后一个楼层的ID
        /// </summary>
        /// <remarks>
        /// 楼层ID升序排列，取最大值。
        /// </remarks>
        /// <returns></returns>
        public int GetLastFloorId()
        {
            MongoRepository repository = new MongoRepository(RheaServer.EstateDatabase, EstateCollection.Building);

            AggregateArgs args = new AggregateArgs();
            args.Pipeline = new[]{
                new BsonDocument {
                    { "$project", new BsonDocument {
                        { "id", 1 },
                        { "name", 1 },
                        { "floors", 1 }
                    }}
                },
                new BsonDocument {
                    { "$unwind", "$floors" }
                },
                new BsonDocument {
                    { "$sort", new BsonDocument {
                        { "floors.id", -1 }
                    }}
                },
                new BsonDocument {
                    { "$limit", 1 }
                }
            };

            var result = repository.Collection.Aggregate(args);
            if (result.Count() == 0)
                return 0;

            BsonDocument doc = result.First();
            int maxId = doc["floors"].AsBsonDocument["id"].AsInt32;
            return maxId;
        }

        /// <summary>
        ///  获取最新房间ID
        /// </summary>
        /// <param name="type">房间类型, 1：公用房，3:宿舍</param>
        /// <returns></returns>
        public int GetLastRoomId(int type)
        {
            int maxId = (type + 1) * 100000;
            MongoRepository repository = new MongoRepository(RheaServer.EstateDatabase, EstateCollection.Room);

            AggregateArgs args = new AggregateArgs();
            args.Pipeline = new[]{
                new BsonDocument {
                    { "$project", new BsonDocument {
                        { "id", 1 }
                    }}
                },
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "id", new BsonDocument { 
                            { "$lt", maxId }
                        }}
                    }}
                },
                new BsonDocument {
                    { "$sort", new BsonDocument {
                        { "id", -1 }
                    }}
                },
                new BsonDocument {
                    { "$limit", 1 }
                }
            };

            var result = repository.Collection.Aggregate(args);
            if (result.Count() == 0)
                return 0;

            BsonDocument doc = result.First();
            return doc["id"].AsInt32;
        }
        #endregion //Method
    }
}
