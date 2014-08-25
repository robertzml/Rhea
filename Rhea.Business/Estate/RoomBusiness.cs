using Rhea.Data;
using Rhea.Data.Estate;
using Rhea.Data.Mongo;
using Rhea.Data.Mongo.Estate;
using Rhea.Model;
using Rhea.Model.Estate;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// 备份业务
        /// </summary>
        private BackupBusiness backupBusiness;

        /// <summary>
        /// 日志业务
        /// </summary>
        private LogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 房间业务类
        /// </summary>
        public RoomBusiness()
        {
            this.roomRepository = new MongoRoomRepository();
            this.backupBusiness = new BackupBusiness();
            this.logBusiness = new LogBusiness();
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
            if (data == null || data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        ///  获取最新房间ID
        /// </summary>
        /// <param name="type">房间类型, 1：公用房，3:宿舍</param>
        /// <returns></returns>
        public int GetLastRoomId(int type)
        {
            BsonRepository repository = new BsonRepository();
            return repository.GetLastRoomId(type);
        }

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        public ErrorCode Create(Room data)
        {
            return this.roomRepository.Create(data);
        }

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        public ErrorCode Update(Room data)
        {
            return this.roomRepository.Update(data);
        }

        /// <summary>
        /// 备份房间
        /// </summary>
        /// <param name="_id">房间系统ID</param>
        /// <returns></returns>
        public ErrorCode Backup(string _id)
        {
            ErrorCode result = this.backupBusiness.Backup(RheaServer.EstateDatabase, EstateCollection.Room, EstateCollection.RoomBackup, _id);
            return result;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_id">房间系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public ErrorCode Log(string _id, Log log)
        {
            ErrorCode result = this.logBusiness.Create(log);
            if (result != ErrorCode.Success)
                return result;

            result = this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Room, _id, log);
            return result;
        }
        #endregion //Method
    }
}
