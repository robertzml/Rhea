using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 楼宇业务接口
    /// </summary>
    public interface IBuildingService
    {
        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <returns></returns>
        List<Building> GetList();

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        List<Building> GetListByBuildingGroup(int buildingGroupId);

        /// <summary>
        /// 获取部门相关楼宇
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        List<Building> GetListByDepartment(int departmentId);

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        Building Get(int id);

        /// <summary>
        /// 获取楼层
        /// </summary>
        /// <param name="id">楼层ID</param>
        /// <returns></returns>
        Floor GetFloor(int id);

        /// <summary>
        /// 添加楼宇
        /// </summary>
        /// <param name="data">楼宇数据</param>
        /// <returns>楼宇ID,0:添加失败</returns>
        int Create(Building data);

        /// <summary>
        /// 编辑楼宇
        /// </summary>
        /// <param name="data">楼宇数据</param>
        /// <returns></returns>
        bool Edit(Building data);

        /// <summary>
        /// 删除楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns></returns>
        int CreateFloor(int buildingId, Floor floor);

        /// <summary>
        /// 编辑楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns></returns>
        bool EditFloor(int buildingId, Floor floor);

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        bool DeleteFloor(int buildingId, int floorId);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}
