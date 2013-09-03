using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 楼宇业务接口
    /// </summary>
    public interface IBuildingBusiness
    {
        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="sort">是否按sort排序</param>
        /// <returns></returns>
        List<Building> GetList(bool sort = false);

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <param name="sort">是否按sort排序</param>
        /// <returns></returns>
        List<Building> GetListByBuildingGroup(int buildingGroupId, bool sort = false);

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
        /// 得到楼宇名称
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        string GetName(int id);

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
        /// 编辑楼层平面图
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <param name="svgUrl">平面图路径</param>
        /// <returns></returns>
        bool EditFloorSvg(int buildingId, int floorId, string svgUrl);

        /// <summary>
        /// 获取楼宇总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 获取楼层总数
        /// </summary>
        /// <returns></returns>
        int FloorCount();

        /// <summary>
        /// 获取楼宇使用面积
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        double GetUsableArea(int buildingId);

        /// <summary>
        /// 获取楼层使用面积
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        double GetFloorUsableArea(int buildingId, int floor);

        /// <summary>
        /// 备份楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>   
        /// <returns></returns>
        bool Backup(int id);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        bool Log(int id, Log log);

        /// <summary>
        /// 归档楼宇
        /// </summary>
        /// <param name="log">相关日志</param>
        /// <returns></returns>
        bool Archive(Log log);

        /// <summary>
        /// 导出楼宇
        /// </summary>
        /// <returns></returns>
        byte[] Export();
    }
}
