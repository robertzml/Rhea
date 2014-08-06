using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 楼宇 Repository
    /// </summary>
    public class MongoBlockRepository : MongoBuildingRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<Block> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 楼宇 Repository
        /// </summary>
        public MongoBlockRepository()
        {
            this.repository = new MongoRepository<Block>(RheaServer.EstateDatabase);
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
        /// 获取子建筑
        /// </summary>
        /// <param name="parentId">父级建筑ID</param>
        /// <returns></returns>
        public override IEnumerable<Building> GetChildren(int parentId)
        {
            return this.repository.Where(r => r.ParentId == parentId);
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
                Block block = (Block)data;
                this.repository.Update(block);
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
