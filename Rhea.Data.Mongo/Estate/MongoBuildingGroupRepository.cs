using Rhea.Data.Estate;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 楼群Repsoitory
    /// </summary>
    public class MongoBuildingGroupRepository : MongoBuildingRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<BuildingGroup> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 建筑Repository
        /// </summary>
        public MongoBuildingGroupRepository()
        {
            this.repository = new MongoRepository<BuildingGroup>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns>楼群</returns>
        public new BuildingGroup Get(int id)
        {
            return this.repository.Where(r => r.BuildingId == id).First();
        }
        #endregion //Method
    }
}
