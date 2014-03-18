using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 校区业务类
    /// </summary>
    public class CampusBusiness
    {
        #region Field
        /// <summary>
        /// 校区Campus
        /// </summary>
        private ICampusRepository campusRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 校区业务类
        /// </summary>
        public CampusBusiness()
        {
            this.campusRepository = new MongoCampusRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <returns></returns>
        public IQueryable<Campus> Get()
        {
            var data = campusRepository.Get();
            return data;
        }

        public Campus Get(int id)
        {
            //IRepository<Campus> campusRepository = new MongoRepository<Campus>();

            //return campusRepository.GetById(id.ToString());

            return null;
        }
        #endregion //Method
    }
}
