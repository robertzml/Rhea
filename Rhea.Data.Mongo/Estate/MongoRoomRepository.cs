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
    /// MongoDB 房间 Repository
    /// </summary>
    public class MongoRoomRepository : IRoomRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<Room> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 房间 Repository
        /// </summary>
        public MongoRoomRepository()
        {
            this.repository = new MongoRepository<Room>(RheaServer.EstateDatabase);
        }

        /// <summary>
        /// MongoDB 房间 Repository
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="database">数据库</param>
        public MongoRoomRepository(string connectionString, string database)
        {
            this.repository = new MongoRepository<Room>(connectionString, database);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Room> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 根据建筑获取房间
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        public virtual IEnumerable<Room> GetByBuilding(int buildingId)
        {
            return this.repository.Where(r => r.BuildingId == buildingId);
        }

        /// <summary>
        /// 获取多个建筑下房间
        /// </summary>
        /// <param name="buildingsId">建筑ID数组</param>
        /// <returns></returns>
        public virtual IEnumerable<Room> GetByBuildings(int[] buildingsId)
        {
            var data = this.repository.Where(r => buildingsId.Contains(r.BuildingId));
            return data;
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public virtual Room Get(int id)
        {
            var data = this.repository.Where(r => r.RoomId == id);
            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        public ErrorCode Create(Room data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.RoomId == data.RoomId);
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
        /// 更新房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        public virtual ErrorCode Update(Room data)
        {
            try
            {
                this.repository.Update(data);
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
