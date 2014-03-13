using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Rhea.Data
{
    /// <summary>
    /// 通用实体接口
    /// </summary>
    /// <typeparam name="TKey">实体ID类型</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 实体ID
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
    }

    /// <summary>
    /// 实体接口
    /// </summary>
    /// <remarks>实体ID默认为字符串</remarks>
    public interface IEntity : IEntity<string>
    { }
}
