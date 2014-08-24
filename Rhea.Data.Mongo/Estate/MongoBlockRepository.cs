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
        /// <remarks>
        /// 获取组团的子楼宇
        /// </remarks>
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

        /// <summary>
        /// 更新楼层
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        public override ErrorCode UpdateFloor(int buildingId, Floor data)
        {
            try
            {
                Block block = this.repository.Single(r => r.BuildingId == buildingId);
                Floor floor = block.Floors.Single(r => r.FloorId == data.FloorId);
                floor.Number = data.Number;
                floor.Name = data.Name;
                floor.UsableArea = data.UsableArea;
                floor.BuildArea = data.BuildArea;
                floor.ImageUrl = data.ImageUrl;
                floor.Remark = data.Remark;

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
