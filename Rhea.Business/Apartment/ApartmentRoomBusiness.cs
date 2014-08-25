using Rhea.Business.Estate;
using Rhea.Data;
using Rhea.Data.Apartment;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 青教房间业务类
    /// </summary>
    public class ApartmentRoomBusiness
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
        /// 青教房间业务类
        /// </summary>
        public ApartmentRoomBusiness()
        {
            this.roomRepository = new MongoApartmentRoomRepository();
            this.backupBusiness = new BackupBusiness();
            this.logBusiness = new LogBusiness();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 根据建筑获取房间
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        public IEnumerable<ApartmentRoom> GetByBuilding(int buildingId)
        {
            BuildingBusiness buildingBusiness = new BuildingBusiness();
            var building = buildingBusiness.Get(buildingId);

            if (building.HasChild)
            {
                var children = buildingBusiness.GetChildBuildings(buildingId);
                return this.roomRepository.GetByBuildings(children.Select(r => r.BuildingId).ToArray()).Where(r => r.Status != 1) as IEnumerable<ApartmentRoom>;
            }
            else
            {
                var data = this.roomRepository.GetByBuilding(buildingId).Where(r => r.Status != 1).Cast<ApartmentRoom>();
                return data;
            }
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ApartmentRoom Get(int id)
        {
            var data = this.roomRepository.Get(id);
            if (data == null || data.Status == 1)
                return null;
            else
                return (ApartmentRoom)data;
        }

        /// <summary>
        /// 获取房间当前住户
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <remarks>
        /// 仅限正常居住和挂职居住
        /// </remarks>
        /// <returns></returns>
        public Inhabitant GetCurrentInhabitant(int id)
        {
            var data = this.roomRepository.Get(id);
            if (data == null || data.Status == 1)
                return null;

            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var records = recordBusiness.GetByRoom(id).Where(r => (EntityStatus)r.Status != EntityStatus.Deleted && (EntityStatus)r.Status != EntityStatus.MoveOut);
            if (records == null || records.Count() == 0)
                return null;

            var record = records.First();
            if ((ResideType)record.ResideType == ResideType.Normal || (ResideType)record.ResideType == ResideType.Guest)
            {
                InhabitantBusiness inhabitantBusiness = new InhabitantBusiness();
                return inhabitantBusiness.Get(record.InhabitantId);
            }
            else
                return null;
        }

        /// <summary>
        /// 获取房间当前居住记录
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ResideRecord GetCurrentRecord(int id)
        {
            var data = this.roomRepository.Get(id);
            if (data == null || data.Status == 1)
                return null;

            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var records = recordBusiness.GetByRoom(id).Where(r => (EntityStatus)r.Status != EntityStatus.Deleted && (EntityStatus)r.Status != EntityStatus.MoveOut);
            if (records == null || records.Count() == 0)
                return null;

            return records.First();
        }

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        public ErrorCode Update(ApartmentRoom data)
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

        /// <summary>
        /// 更新房间日志信息
        /// </summary>
        /// <param name="_id">房间系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        /// <remarks>
        /// 日志信息已存在
        /// </remarks>
        public ErrorCode LogItem(string _id, Log log)
        {
            return this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Room, _id, log);
        }
        #endregion //Method
    }
}
