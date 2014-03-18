using MongoDB.Driver.Linq;
using Rhea.Data.Estate;
using Rhea.Data.Server;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<Campus> Get()
        {
            //var query = this.repository.Collection.AsQueryable<Campus>().Where(r => r.Status == 0);
            //return query.ToList();

            return this.repository.Get(r => r.Status == 0);
        }

        public Campus Get(int id)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
