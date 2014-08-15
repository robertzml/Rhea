using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;

namespace Rhea.Data.Mongo.Apartment
{
    /// <summary>
    /// MongoDB 居住记录 Repository
    /// </summary>
    public class MongoResideRecordRepository : IResideRecordRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<ResideRecord> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 居住记录 Repository
        /// </summary>
        public MongoResideRecordRepository()
        {
            this.repository = new MongoRepository<ResideRecord>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有居住记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResideRecord> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取居住记录
        /// </summary>
        /// <param name="_id">系统ID</param>
        /// <returns></returns>
        public ResideRecord Get(string _id)
        {
            return this.repository.GetById(_id);
        }

        /// <summary>
        /// 获取多个房间的居住记录
        /// </summary>
        /// <param name="roomsId">房间ID数组</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByRooms(int[] roomsId)
        {
            return this.repository.Where(r => roomsId.Contains(r.RoomId));
        }

        /// <summary>
        /// 根据房间获取居住记录
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByRoom(int roomId)
        {
            return this.repository.Where(r => r.RoomId == roomId);
        }

        /// <summary>
        /// 获取住户居住记录
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <returns></returns>
        public IEnumerable<ResideRecord> GetByInhabitant(string inhabitantId)
        {
            return this.repository.Where(r => r.InhabitantId == inhabitantId);
        }

        /// <summary>
        /// 添加居住记录
        /// </summary>
        /// <param name="data">居住记录对象</param>
        /// <returns></returns>
        public ErrorCode Create(ResideRecord data)
        {
            try
            {
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
