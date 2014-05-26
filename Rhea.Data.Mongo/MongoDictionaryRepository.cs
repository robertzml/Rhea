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
        /// <returns></returns>
        public IEnumerable<Dictionary> Get()
        {
            return this.repository.Where(r => r.IsCombined == false);
        }
        #endregion //Method
    }
}
