using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <returns></returns>
        List<BuildingGroup> GetList();

        /// <summary>
        /// 得到楼群简单列表
        /// </summary>
        /// <returns></returns>
        List<BuildingGroup> GetSimpleList();

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
        /// <returns>楼群ID</returns>
        int Create(BuildingGroup data);

        /// <summary>
        /// 更新楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <returns></returns>
        bool Edit(BuildingGroup data);

        /// <summary>
        /// 删除楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        bool Delete(int id);

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
    }
}
