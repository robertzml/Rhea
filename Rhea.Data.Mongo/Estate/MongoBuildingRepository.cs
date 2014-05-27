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
    /// MongoDB 建筑Repository
    /// </summary>
    public class MongoBuildingRepository : IBuildingRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<Building> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 建筑Repository
        /// </summary>
        public MongoBuildingRepository()
        {
            this.repository = new MongoRepository<Building>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有建筑
        /// </summary>
        /// <returns>所有建筑</returns>
        public IEnumerable<Building> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 添加建筑
        /// </summary>
        /// <param name="model">建筑对象</param>
        /// <returns></returns>
        public ErrorCode Create(Building model)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
