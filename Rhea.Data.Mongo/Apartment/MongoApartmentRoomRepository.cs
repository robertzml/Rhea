using Rhea.Data.Mongo.Estate;
using Rhea.Model;
using Rhea.Model.Apartment;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Mongo.Apartment
{
    /// <summary>
    /// MongoDB 青教房间 Repository
    /// </summary>
    public class MongoApartmentRoomRepository : MongoRoomRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<ApartmentRoom> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 青教房间 Repository
        /// </summary>
        public MongoApartmentRoomRepository()
        {
            this.repository = new MongoRepository<ApartmentRoom>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取青教房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public override Room Get(int id)
        {
            var data = this.repository.Where(r => r.RoomId == id);
            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 更新青教房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        public override ErrorCode Update(Room data)
        {
            try
            {
                ApartmentRoom apartmentRoom = (ApartmentRoom)data;
                this.repository.Update(apartmentRoom);
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
