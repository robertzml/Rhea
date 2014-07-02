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
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public virtual Building Get(int id)
        {
            return this.repository.Where(r => r.BuildingId == id).First();
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
        #endregion //Method
    }
}
