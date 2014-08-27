using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Business.Estate;
using Rhea.Data;
using Rhea.Data.Apartment;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model;
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

        /// <summary>
        /// 日志业务
        /// </summary>
        private LogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 居住记录业务类
        /// </summary>
        public ResideRecordBusiness()
        {
            this.recordRepository = new MongoResideRecordRepository();
            this.logBusiness = new LogBusiness();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有居住记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResideRecord> Get()
        {
            return this.recordRepository.Get().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取居住记录
        /// </summary>
        /// <param name="_id">居住记录ID</param>
        /// <returns></returns>
        public ResideRecord Get(string _id)
        {
            var data = this.recordRepository.Get(_id);
            if (data == null || data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 根据楼宇获取居住记录
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByBuilding(int buildingId)
        {
            RoomBusiness roomBusiness = new RoomBusiness();
            var rooms = roomBusiness.GetByBuilding(buildingId).Select(r => r.RoomId);

            var data = this.recordRepository.GetByRooms(rooms.ToArray()).Where(r => r.Status != 1);
            return data;
        }

        /// <summary>
        /// 获取房间居住记录
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByRoom(int roomId)
        {
            var data = this.recordRepository.GetByRoom(roomId).Where(r => r.Status != 1);
            return data;
        }

        /// <summary>
        /// 获取住户居住记录
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByInhabitant(string inhabitantId)
        {
            var data = this.recordRepository.GetByInhabitant(inhabitantId).Where(r => r.Status != 1);
            return data;
        }

        /// <summary>
        /// 获取快要到期的居住记录
        /// </summary>
        /// <param name="day">到期天数</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetExpireInDays(int day)
        {
            List<ResideRecord> data = new List<ResideRecord>();
            DateTime now = DateTime.Now;
            var records = this.recordRepository.Get().Where(r => r.Status == (int)EntityStatus.Normal || r.Status == (int)EntityStatus.ExtendTime);

            foreach(var record in records)
            {
                if (record.ExpireDate == null)
                    continue;

                DateTime expireDate = record.ExpireDate.Value;
                if (expireDate.AddDays(day) <= now)
                {
                    data.Add(record);
                }
            }

            data = data.OrderBy(r => r.ExpireDate).ToList();
            return data;
        }

        /// <summary>
        /// 获取超期居住记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetExpired()
        {
            var records = this.recordRepository.Get().Where(r => r.Status == (int)EntityStatus.OverTime).OrderBy(r => r.ExpireDate);
            return records;
        }

        /// <summary>
        /// 添加住户居住记录
        /// </summary>
        /// <param name="data">居住记录对象</param>
        /// <returns></returns>
        public ErrorCode Create(ResideRecord data)
        {
            return this.recordRepository.Create(data);
        }

        /// <summary>
        /// 更新居住记录
        /// </summary>
        /// <param name="data">居住记录对象</param>
        /// <returns></returns>
        public ErrorCode Update(ResideRecord data)
        {
            return this.recordRepository.Update(data);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_id">居住记录系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public ErrorCode Log(string _id, Log log)
        {
            ErrorCode result = this.logBusiness.Create(log);
            if (result != ErrorCode.Success)
                return result;

            result = this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.ResideRecord, _id, log);
            return result;
        }

        /// <summary>
        /// 更新居住记录日志信息
        /// </summary>
        /// <param name="_id">居住记录系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        /// <remarks>
        /// 日志信息已存在
        /// </remarks>
        public ErrorCode LogItem(string _id, Log log)
        {
            return this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.ResideRecord, _id, log);
        }
        #endregion //Method
    }
}
