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
    /// MongoDB 居住记录 Repository
    /// </summary>
    public class MongoResideRecordRepository : IResideRecordRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<ResideRecord> repository; 
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 居住记录 Repository
        /// </summary>
        public MongoResideRecordRepository()
        {
            this.repository = new MongoRepository<ResideRecord>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 添加居住记录
        /// </summary>
        /// <param name="data">居住记录对象</param>
        /// <returns></returns>
        public ErrorCode Create(ResideRecord data)
        {
            try
            {
                this.repository.Add(data);
            }
            catch(Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
