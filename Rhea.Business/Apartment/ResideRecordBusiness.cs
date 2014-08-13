using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Business.Estate;
using Rhea.Data.Apartment;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model.Apartment;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 居住记录业务类
    /// </summary>
    public class ResideRecordBusiness
    {
        #region Field
        /// <summary>
        /// 居住记录Repository
        /// </summary>
        IResideRecordRepository recordRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 居住记录业务类
        /// </summary>
        public ResideRecordBusiness()
        {
            this.recordRepository = new MongoResideRecordRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 根据楼宇获取居住记录
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByBuilding(int buildingId)
        {
            RoomBusiness roomBusiness = new RoomBusiness();
            var rooms = roomBusiness.GetByBuilding(buildingId).Select(r => r.RoomId);

            var data = this.recordRepository.GetByRooms(rooms.ToArray());
            return data;
        }

        /// <summary>
        /// 获取房间居住记录
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByRoom(int roomId)
        {
            var data = this.recordRepository.GetByRoom(roomId);
            return data;
        }
        #endregion //Method
    }
}
