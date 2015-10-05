using Rhea.Data;
using Rhea.Data.Apartment;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 收费业务类
    /// </summary>
    public class FinanceBusiness
    {
        #region Field
        /// <summary>
        /// 房间Repository
        /// </summary>
        private IRoomRepository roomRepository;

        /// <summary>
        /// 居住记录Repository
        /// </summary>
        IResideRecordRepository recordRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 收费业务类
        /// </summary>
        public FinanceBusiness()
        {
            this.recordRepository = new MongoResideRecordRepository();
        }
        #endregion //Constructor

        #region Method
        
        #endregion //Method
    }
}
