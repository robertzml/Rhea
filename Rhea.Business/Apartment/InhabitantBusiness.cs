using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data;
using Rhea.Data.Apartment;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 住户业务类
    /// </summary>
    public class InhabitantBusiness
    {
        #region Field
        /// <summary>
        /// 住户Repository
        /// </summary>
        private IInhabitantRepository inhabitantRepository;

        /// <summary>
        /// 日志业务
        /// </summary>
        private LogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 住户业务类
        /// </summary>
        public InhabitantBusiness()
        {
            this.inhabitantRepository = new MongoInhabitantRepository();
            this.logBusiness = new LogBusiness();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有住户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Inhabitant> Get()
        {
            return this.inhabitantRepository.Get().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取住户
        /// </summary>
        /// <param name="_id">住户ID</param>
        /// <returns></returns>
        public Inhabitant Get(string _id)
        {
            var data = this.inhabitantRepository.Get(_id);
            if (data == null || data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 添加住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        public ErrorCode Create(Inhabitant data)
        {
            return this.inhabitantRepository.Create(data);
        }

        /// <summary>
        /// 编辑住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        public ErrorCode Update(Inhabitant data)
        {
            return this.inhabitantRepository.Update(data);
        }

        /// <summary>
        /// 获取住户当前居住房间
        /// </summary>
        /// <param name="id">住户ID</param>
        /// <returns>
        /// 仅包括记录居住中，超期，延期房间
        /// </returns>
        public IEnumerable<ApartmentRoom> GetCurrentRooms(string id)
        {
            List<ApartmentRoom> rooms = new List<ApartmentRoom>();
            var data = this.inhabitantRepository.Get(id);
            if (data == null || data.Status == 1)
                return rooms;

            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var records = recordBusiness.GetByInhabitant(id)
                .Where(r => r.Status == (int)EntityStatus.Normal || r.Status == (int)EntityStatus.OverTime || r.Status == (int)EntityStatus.ExtendTime);
            if (records == null || records.Count() == 0)
                return rooms;

            ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
            foreach (var item in records)
            {
                var room = roomBusiness.Get(item.RoomId);
                if (room != null && room.Status != 1)
                    rooms.Add(room);
            }

            return rooms;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_id">住户系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public ErrorCode Log(string _id, Log log)
        {
            ErrorCode result = this.logBusiness.Create(log);
            if (result != ErrorCode.Success)
                return result;

            result = this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Inhabitant, _id, log);
            return result;
        }

        /// <summary>
        /// 更新住户日志信息
        /// </summary>
        /// <param name="_id">住户系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        /// <remarks>
        /// 日志信息已存在
        /// </remarks>
        public ErrorCode LogItem(string _id, Log log)
        {
            return this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Inhabitant, _id, log);
        }
        #endregion //Method
    }
}
