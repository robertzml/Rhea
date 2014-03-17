using Rhea.Data;
using Rhea.Data.Estate;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Mongo;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB校区Repository类
    /// </summary>
    public class MongoCampusRepository : RheaContextBase<Campus>, ICampusRepository
    {
        #region Field
        /// <summary>
        /// Repository
        /// </summary>
        //private MongoRepository<Campus> repository;
        #endregion //Field

        #region Constructor
        public MongoCampusRepository()
        {
            this.repository = new MongoRepository<Campus>();
        }
        #endregion //Constructor

        public IQueryable<Campus> Get()
        {
            throw new NotImplementedException();
        }

        public Campus Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
