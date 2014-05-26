using Rhea.Data;
using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Mongo
{
    /// <summary>
    /// MongoDB 字典Repository类
    /// </summary>
    public class MongoDictionaryRepository : IDictionaryRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<Dictionary> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 字典Repository类
        /// </summary>
        public MongoDictionaryRepository()
        {
            this.repository = new MongoRepository<Dictionary>(RheaServer.RheaDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns>返回简单型字典集</returns>
        public IEnumerable<Dictionary> Get()
        {
            return this.repository.Where(r => r.IsCombined == false);
        }

        /// <summary>
        /// 获取字典集
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        public Dictionary Get(string name)
        {
            return this.repository.Where(r => r.Name == name).First();
        }

        /// <summary>
        /// 添加字典集
        /// </summary>
        /// <param name="data">字典对象</param>
        /// <returns></returns>
        public bool Create(Dictionary data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.Name == data.Name);
                if (dup)
                    return false;

                this.repository.Add(data);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        #endregion //Method
    }
}
