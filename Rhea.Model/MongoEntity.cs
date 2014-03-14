using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace Rhea.Model
{
    /// <summary>
    /// 实体抽象类
    /// </summary>
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class MongoEntity : IEntity<ObjectId>
    {
        /// <summary>
        /// 实体主键ID
        /// </summary>
        [DataMember]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual ObjectId _id { get; set; }
    }
}
