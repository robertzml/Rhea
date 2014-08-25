using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 建筑Repository接口
    /// </summary>
    public interface IBuildingRepository
    {
        /// <summary>
        /// 获取所有建筑
        /// </summary>
        /// <returns>所有建筑</returns>
        IEnumerable<Building> Get();

        /// <summary>
        /// 获取子建筑
        /// </summary>
        /// <param name="parentId">父级建筑ID</param>
        /// <returns></returns>
        IEnumerable<Building> GetChildren(int parentId);

        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        Building Get(int id);

        /// <summary>
        /// 按组织类型获取建筑
        /// </summary>
        /// <param name="organizeType">组织类型</param>
        /// <returns></returns>
        IEnumerable<Building> GetByOrganizeType(int organizeType);

        /// <summary>
        /// 添加建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        ErrorCode Create(Building data);

        /// <summary>
        /// 更新建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        ErrorCode Update(Building data);

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        ErrorCode CreateFloor(int buildingId, Floor data);

        /// <summary>
        /// 更新楼层
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        ErrorCode UpdateFloor(int buildingId, Floor data);

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        ErrorCode DeleteFloor(int buildingId, int floorId);
    }
}
