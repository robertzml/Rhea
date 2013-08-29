using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Server;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 人事字典
    /// </summary>
    public class PersonnelDictionaryBusiness : DictionaryBusiness
    {
        #region Field
        
        #endregion //Field

         #region Constructor
        /// <summary>
        /// 人事字典
        /// </summary>
        public PersonnelDictionaryBusiness()
            :base(RheaServer.PersonnelDatabase, PersonnelCollection.Dictionary)
        {
        }
        #endregion //Constructor

        #region Method
        ///// <summary>
        ///// 得到字典集
        ///// </summary>
        ///// <param name="name">字典名称</param>
        ///// <returns></returns>
        //public Dictionary<int, string> GetCombineDict(string name)
        //{
        //    BsonDocument doc = this.context.FindOne(PersonnelCollection.Dictionary, "name", name);

        //    if (doc == null)
        //        return null;

        //    BsonArray array = doc["property"].AsBsonArray;
        //    Dictionary<int, string> data = new Dictionary<int, string>();

        //    foreach (var item in array)
        //    {
        //        data.Add(item["id"].AsInt32, item["value"].AsString);
        //    }

        //    return data;
        //}

        ///// <summary>
        ///// 得到字典项值
        ///// </summary>
        ///// <param name="dictName">字典名称</param>
        ///// <param name="id">字典项ID</param>
        ///// <returns></returns>
        //public string GetItemValue(string dictName, int id)
        //{
        //    BsonDocument doc = this.context.FindOne(PersonnelCollection.Dictionary, "name", dictName);

        //    if (doc == null)
        //        return null;

        //    BsonArray array = doc["property"].AsBsonArray;
        //    BsonValue item = array.First(r => r["id"].AsInt32 == id);
        //    string value = item["value"].AsString;
        //    return value;
        //}

        //public string[] GetDict(string name)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion //Method
    }
}
