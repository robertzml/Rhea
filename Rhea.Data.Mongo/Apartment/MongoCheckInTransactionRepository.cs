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
    /// MongoDB 入住办理业务记录 Repository
    /// </summary>
    public class MongoCheckInTransactionRepository : ITransactionRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<CheckInTransaction> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 入住办理业务记录 Repository
        /// </summary>
        public MongoCheckInTransactionRepository()
        {
            this.repository = new MongoRepository<CheckInTransaction>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有入住办理业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApartmentTransaction> Get()
        {
            var data = this.repository.Where(r => r.Type == (int)LogType.ApartmentCheckIn);
            return data;
        }

        /// <summary>
        /// 添加业务记录
        /// </summary>
        /// <param name="data">业务记录对象</param>
        /// <returns></returns>
        public ErrorCode Create(ApartmentTransaction data)
        {
            try
            {
                CheckInTransaction tran = (CheckInTransaction)data;
                this.repository.Add(tran);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
