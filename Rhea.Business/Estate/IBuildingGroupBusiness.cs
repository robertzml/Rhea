using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Account;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 楼群业务接口
    /// </summary>
    public interface IBuildingGroupBusiness
    {
        /// <summary>
        /// 获取楼群列表
        /// </summary>
        /// <param name="sort">是否按sort排序</param>
        /// <returns></returns>
        List<BuildingGroup> GetList(bool sort = false);

        /// <summary>
        /// 得到楼群简单列表
        /// </summary>
        /// <param name="sort">是否按sort排序</param>
        /// <returns></returns>
        List<BuildingGroup> GetSimpleList(bool sort = false);

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        BuildingGroup Get(int id);

        /// <summary>
        /// 得到楼群名称
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        string GetName(int id);

        /// <summary>
        /// 添加楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <param name="user">相关用户</param>
        /// <returns>楼群ID</returns>
        int Create(BuildingGroup data, UserProfile user);

        /// <summary>
        /// 更新楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        bool Edit(BuildingGroup data, UserProfile user);

        /// <summary>
        /// 删除楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <param name="user">相关用户</param>
        /// <returns></returns>
        bool Delete(int id, UserProfile user);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 获取使用面积
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        double GetUsableArea(int id);

        /// <summary>
        /// 导出楼群
        /// </summary>
        /// <returns></returns>
        byte[] Export();

        /// <summary>
        /// 备份楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <param name="backupBusiness">备份功能接口</param>
        /// <returns></returns>
        bool Backup(int id, IBackupBusiness backupBusiness);
    }
}
