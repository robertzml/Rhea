using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 房间Repository接口
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        IEnumerable<Room> Get();

        /// <summary>
        /// 根据建筑获取房间
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        IEnumerable<Room> GetByBuilding(int buildingId);

        /// <summary>
        /// 获取多个建筑下房间
        /// </summary>
        /// <param name="buildingsId">建筑ID数组</param>
        /// <returns></returns>
        IEnumerable<Room> GetByBuildings(int[] buildingsId);

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        Room Get(int id);

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        ErrorCode Create(Room data);
    }
}
