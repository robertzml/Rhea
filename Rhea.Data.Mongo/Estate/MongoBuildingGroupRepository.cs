using Rhea.Data.Estate;
using Rhea.Data.Server;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 楼群Repository
    /// </summary>
    public class MongoBuildingGroupRepository : RheaContextBase<BuildingGroup>, IBuildingGroupRepository
    {
        #region Constructor
        /// <summary>
        /// MongoDB 楼群Repository类
        /// </summary>
        public MongoBuildingGroupRepository()
        {
            this.repository = new MongoRepository<BuildingGroup>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有楼群
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BuildingGroup> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public BuildingGroup Get(int id)
        {
            var query = Query.EQ("id", id);
            return this.repository.Collection.FindOne(query);
        }
        #endregion //Method
    }
}
