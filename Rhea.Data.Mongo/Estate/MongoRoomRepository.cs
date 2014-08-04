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
        #endregion //Method
    }
}
