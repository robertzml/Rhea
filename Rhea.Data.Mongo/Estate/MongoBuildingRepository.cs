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
    /// MongoDB 建筑 Repository
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

        /// <summary>
        /// MongoDB 建筑Repository
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="database">数据库</param>
        public MongoBuildingRepository(string connectionString, string database)
        {
            this.repository = new MongoRepository<Building>(connectionString, database);
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
        /// 获取子建筑
        /// </summary>
        /// <param name="parentId">父级建筑ID</param>
        /// <returns></returns>
        public virtual IEnumerable<Building> GetChildren(int parentId)
        {
            var data = this.repository.Where(r => r.ParentId == parentId);
            return data;
        }

        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public virtual Building Get(int id)
        {
            var data = this.repository.Where(r => r.BuildingId == id);
            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 按组织类型获取建筑
        /// </summary>
        /// <param name="organizeType">组织类型</param>
        /// <returns></returns>
        public IEnumerable<Building> GetByOrganizeType(int organizeType)
        {
            return this.repository.Where(r => r.OrganizeType == organizeType);
        }

        /// <summary>
        /// 添加建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        public ErrorCode Create(Building data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.BuildingId == data.BuildingId);
                if (dup)
                    return ErrorCode.DuplicateId;

                this.repository.Add(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 更新建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        public virtual ErrorCode Update(Building data)
        {
            return ErrorCode.NotImplement;
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        public virtual ErrorCode CreateFloor(int buildingId, Floor data)
        {
            return ErrorCode.NotImplement;
        }

        /// <summary>
        /// 更新楼层
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        public virtual ErrorCode UpdateFloor(int buildingId, Floor data)
        {
            return ErrorCode.NotImplement;
        }
        #endregion //Method
    }
}
