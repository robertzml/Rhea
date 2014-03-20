using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Model.Estate;
using MongoDB.Driver.Builders;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 楼宇Repository
    /// </summary>
    public class MongoBuildingRepository : RheaContextBase<Building>, IBuildingRepository
    {
        #region Constructor
        /// <summary>
        /// MongoDB 楼宇Repository
        /// </summary>
        public MongoBuildingRepository()
        {
            this.repository = new MongoRepository<Building>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有楼宇
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Building> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 按楼群获取楼宇
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public IEnumerable<Building> GetByBuildingGroup(int buildingGroupId)
        {
            var query = Query.EQ("buildingGroupId", buildingGroupId);
            return this.repository.Collection.Find(query);
        }

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public Building Get(int id)
        {
            var query = Query.EQ("id", id);
            return this.repository.Collection.FindOne(query);
        }
        #endregion //Method
    }
}
