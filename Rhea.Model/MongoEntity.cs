using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace Rhea.Model
{
    /// <summary>
    /// MongoDB实体抽象类
    /// </summary>
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class MongoEntity : IEntity<string>
    {
        /// <summary>
        /// 实体主键ID
        /// </summary>
        [DataMember]        
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public virtual string _id { get; set; }
    }
}
