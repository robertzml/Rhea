using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using Rhea.Model;
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
        /// 获取房间列表
        /// </summary>
        /// <param name="firstCode">一级功能编码</param>
        /// <returns></returns>
        List<Room> GetListByFunction(int firstCode);

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="firstCode">一级功能编码</param>
        /// <param name="secondCode">二级功能编码</param>
        /// <returns></returns>
        List<Room> GetListByFunction(int firstCode, int secondCode);

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
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 获取房间使用面积
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        double GetUsableArea(int id);

        /// <summary>
        /// 楼群内房间数量
        /// </summary>
        /// <param name="buildingGroupId">楼群ID</param>
        /// <returns></returns>
        int CountByBuildingGroup(int buildingGroupId);

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
        /// 房间总面积
        /// </summary>
        /// <returns></returns>
        double TotalArea();

        /// <summary>
        /// 部门房间使用总面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        double DepartmentRoomArea(int departmentId);

        /// <summary>
        /// 分功能房间使用面积
        /// </summary>
        /// <param name="firstCode">一级编码</param>
        /// <param name="secondCode">二级编码</param>
        /// <returns></returns>
        double FunctionRoomArea(int firstCode, int secondCode);

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
        /// 记录日志
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        bool Log(int id, Log log);

        /// <summary>
        /// 归档房间
        /// </summary>
        /// <param name="log">相关日志</param>
        /// <returns></returns>
        bool Archive(Log log);

        /// <summary>
        /// 分配房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <param name="newDepartmentId">新部门ID</param>
        /// <returns></returns>
        bool Assign(int id, int newDepartmentId);

        /// <summary>
        /// 查找分配历史
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        List<Room> GetAssignHistory(int id);

        /// <summary>
        /// 按部门得到归档房间列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="logId">日志ID</param>
        /// <returns></returns>
        List<Room> GetArchiveListByDepartment(int departmentId, string logId);
    }
}
