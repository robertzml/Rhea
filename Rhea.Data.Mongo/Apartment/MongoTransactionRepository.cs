using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;

namespace Rhea.Data.Mongo.Apartment
{
    /// <summary>
    /// MongoDB 业务记录 Repository
    /// </summary>
    public class MongoTransactionRepository : ITransactionRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<ApartmentTransaction> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 业务记录 Repository
        /// </summary>
        public MongoTransactionRepository()
        {
            this.repository = new MongoRepository<ApartmentTransaction>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApartmentTransaction> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        public ApartmentTransaction Get(string id)
        {
            return this.repository.GetById(id);
        }

        /// <summary>
        /// 添加业务记录
        /// </summary>
        /// <param name="data">业务记录对象</param>
        /// <returns></returns>
        public ErrorCode Create(ApartmentTransaction data)
        {
            return ErrorCode.NotImplement;
        }
        #endregion //Method        
    }
}
