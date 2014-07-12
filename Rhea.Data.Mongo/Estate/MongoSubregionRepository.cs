using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;
using Rhea.Model;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 分区Repository
    /// </summary>
    public class MongoSubregionRepository : MongoBuildingRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<Subregion> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 分区Repository
        /// </summary>
        public MongoSubregionRepository()
        {
            this.repository = new MongoRepository<Subregion>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
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
                Subregion cottage = (Subregion)data;
                this.repository.Update(cottage);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
