using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房间业务类
    /// </summary>
    public class RoomBusiness
    {
        #region Field
        /// <summary>
        /// 房间Repository
        /// </summary>
        private IRoomRepository roomRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 房间业务类
        /// </summary>
        public RoomBusiness()
        {
            this.roomRepository = new MongoRoomRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Room> Get()
        {
            var data = this.roomRepository.Get();
            return data.Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public Room Get(int id)
        {
            var data = this.roomRepository.Get(id);
            if (data.Status != 1)
                return data;
            else
                return null;
        }

        /// <summary>
        /// 根据楼宇获取房间
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public IEnumerable<Room> GetByBuilding(int buildingId)
        {
            var data = this.roomRepository.GetByBuilding(buildingId);
            return data.Where(r => r.Status != 1).OrderBy(r => r.RoomId);
        }
        #endregion //Method
    }
}
