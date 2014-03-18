using Rhea.Data;
using Rhea.Data.Estate;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Mongo;
using Rhea.Data.Server;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 校区Repository类
    /// </summary>
    public class MongoCampusRepository : RheaContextBase<Campus>, ICampusRepository
    {
        #region Field
       
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 校区Repository类
        /// </summary>
        public MongoCampusRepository()
        {
            this.repository = new MongoRepository<Campus>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        public IQueryable<Campus> Get()
        {
            return this.repository.Get(r => r.Status == 0).AsQueryable<Campus>();
        
        }

        public Campus Get(int id)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
