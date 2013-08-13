using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Account;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房间业务接口
    /// </summary>
    public interface IRoomBusiness
    {
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns></returns>
        List<Room> GetList();

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        List<Room> GetListByBuildingGroup(int buildingGroupId);

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        List<Room> GetListByBuilding(int buildingId);

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        List<Room> GetListByBuilding(int buildingId, int floor);

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        List<Room> GetListByFloor(int floorId);

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        List<Room> GetListByDepartment(int departmentId);

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        List<Room> GetListByDepartment(int departmentId, int buildingId);

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        Room Get(int id);

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="data">房间数据</param>
        /// <param name="user">相关用户</param>
        /// <returns>房间ID，0:添加失败</returns>
        int Create(Room data, UserProfile user);

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="data">房间数据</param>
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        bool Edit(Room data, UserProfile user);

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        bool Delete(int id, UserProfile user);

        /// <summary>
        /// 获取房间属性列表
        /// </summary>
        /// <returns></returns>
        List<RoomFunctionCode> GetFunctionCodeList();

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 楼宇内房间数量
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        int CountByBuilding(int buildingId);

        /// <summary>
        /// 楼层内房间数量
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        int CountByFloor(int buildingId, int floor);

        /// <summary>
        /// 部门房间数量
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        int CountByDepartment(int departmentId);

        /// <summary>
        /// 导出房间
        /// </summary>
        /// <returns></returns>
        byte[] Export();

        /// <summary>
        /// 备份房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        bool Backup(int id);

        /// <summary>
        /// 分配房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="newDepartmentId">新部门ID</param>
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        bool Assign(int id, int newDepartmentId, UserProfile user);

        /// <summary>
        /// 查找分配历史
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        List<Room> GetAssignHistory(int id);
    }
}
