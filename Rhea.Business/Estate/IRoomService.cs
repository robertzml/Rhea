using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Estate;

namespace Rhea.Business
{
    /// <summary>
    /// 房间业务接口
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns></returns>
        List<Room> GetList();

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
        /// <returns>房间ID，0:添加失败</returns>
        int Create(Room data);

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="data">房间数据</param>
        /// <returns></returns>
        bool Edit(Room data);

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        bool Delete(int id);

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
    }
}
