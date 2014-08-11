using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data;
using Rhea.Data.Mongo;
using Rhea.Model;
using Rhea.Model.Estate;
using MongoDB.Bson;

namespace Rhea.Business
{
    /// <summary>
    /// 字典业务
    /// </summary>
    public class DictionaryBusiness
    {
        #region Field
        /// <summary>
        /// 字典Repository
        /// </summary>
        private IDictionaryRepository dictionaryRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 字典业务
        /// </summary>
        public DictionaryBusiness()
        {
            this.dictionaryRepository = new MongoDictionaryRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns>返回简单字典集</returns>
        public IEnumerable<Dictionary> Get()
        {
            return this.dictionaryRepository.Get();
        }

        /// <summary>
        /// 获取字典集
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        public Dictionary Get(string name)
        {
            return this.dictionaryRepository.Get(name);
        }

        /// <summary>
        /// 添加字典集
        /// </summary>
        /// <param name="data">字典对象</param>
        /// <returns></returns>
        public ErrorCode Create(Dictionary data)
        {
            return this.dictionaryRepository.Create(data);
        }

        /// <summary>
        /// 编辑字典集
        /// </summary>
        /// <param name="data">字典对象</param>
        /// <returns></returns>
        public ErrorCode Edit(Dictionary data)
        {
            return this.dictionaryRepository.Edit(data);
        }

        /// <summary>
        /// 获取房间功能编码列表
        /// </summary>
        /// <returns></returns>
        public List<RoomFunctionCode> GetRoomFunctionCodes()
        {
            List<RoomFunctionCode> data = new List<RoomFunctionCode>();
            BsonRepository repository = new BsonRepository();
            BsonDocument doc = repository.GetRoomFunctionCodes();

            BsonArray array = doc["property"].AsBsonArray;
           
            foreach(BsonDocument row in array)
            {
                RoomFunctionCode code = new RoomFunctionCode();
                code.CodeId = row["codeId"].AsString;
                code.FirstCode = row["firstCode"].AsInt32;
                code.SecondCode = row["secondCode"].AsInt32;
                code.ClassifyName = row["classifyName"].AsString;
                code.FunctionProperty = row["functionProperty"].AsString;
                code.Remark = row["remark"].AsString;

                data.Add(code);
            }

            return data;
        }
        #endregion //Method
    }
}
