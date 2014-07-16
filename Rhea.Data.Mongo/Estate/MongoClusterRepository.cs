using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 组团Repsoitory
    /// </summary>
    public class MongoClusterRepository : MongoBuildingRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<Cluster> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 建筑Repository
        /// </summary>
        public MongoClusterRepository()
        {
            this.repository = new MongoRepository<Cluster>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns>组团</returns>
        public override Building Get(int id)
        {
            return this.repository.Where(r => r.BuildingId == id).First();
        }

        /// <summary>
        /// 更新建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        public override ErrorCode Update(Building data)
        {
            try
            {
                Cluster cluster = (Cluster)data;
                this.repository.Update(cluster);
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
