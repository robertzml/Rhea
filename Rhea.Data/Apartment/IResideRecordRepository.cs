using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;
using Rhea.Model.Apartment;

namespace Rhea.Data.Apartment
{
    /// <summary>
    /// 居住记录Repository
    /// </summary>
    public interface IResideRecordRepository
    {
        /// <summary>
        /// 获取所有居住记录
        /// </summary>
        /// <returns></returns>
        IEnumerable<ResideRecord> Get();

        /// <summary>
        /// 获取居住记录
        /// </summary>
        /// <param name="_id">系统ID</param>
        /// <returns></returns>
        ResideRecord Get(string _id);       

        /// <summary>
        /// 获取多个房间的居住记录
        /// </summary>
        /// <param name="roomsId">房间ID数组</param>
        /// <returns></returns>
        IEnumerable<ResideRecord> GetByRooms(int[] roomsId);

        /// <summary>
        /// 根据房间获取居住记录
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        IEnumerable<ResideRecord> GetByRoom(int roomId);

        /// <summary>
        /// 获取住户居住记录
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <returns></returns>
        IEnumerable<ResideRecord> GetByInhabitant(string inhabitantId);

        /// <summary>
        /// 添加居住记录
        /// </summary>
        /// <param name="data">居住记录对象</param>
        /// <returns></returns>
        ErrorCode Create(ResideRecord data);
    }
}
