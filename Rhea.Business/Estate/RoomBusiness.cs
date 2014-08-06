using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model;
using Rhea.Model.Estate;

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
            return this.roomRepository.Get().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 根据建筑获取房间
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        public IEnumerable<Room> GetByBuilding(int buildingId)
        {
            BuildingBusiness buildingBusiness = new BuildingBusiness();
            var building = buildingBusiness.Get(buildingId);

            if (building.HasChild)
            {
                var children = buildingBusiness.GetChildBuildings(buildingId);
                return this.roomRepository.GetByBuildings(children.Select(r => r.BuildingId).ToArray()).Where(r => r.Status != 1);
            }
            else
            {
                return this.roomRepository.GetByBuilding(buildingId).Where(r => r.Status != 1);
            }
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public Room Get(int id)
        {
            var data = this.roomRepository.Get(id);
            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        public ErrorCode Create(Room data)
        {
            data.Status = 0;
            return this.roomRepository.Create(data);
        }
        #endregion //Method
    }
}
