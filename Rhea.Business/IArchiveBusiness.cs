using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Rhea.Business
{
    /// <summary>
    /// 归档业务类
    /// </summary>
    public interface IArchiveBusiness
    {
        /// <summary>
        /// 归档数据
        /// </summary>
        /// <param name="collectionName">collection名称</param>
        /// <param name="docs">数据</param>
        /// <returns></returns>
        bool Archive(string collectionName, IEnumerable<BsonDocument> docs);


    }
}
