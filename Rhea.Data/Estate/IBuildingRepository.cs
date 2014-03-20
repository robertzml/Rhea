using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 楼宇Repository接口
    /// </summary>
    public interface IBuildingRepository
    {
        /// <summary>
        /// 获取所有楼宇
        /// </summary>
        /// <returns></returns>
        IEnumerable<Building> Get();

        /// <summary>
        /// 按楼群获取楼宇
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        IEnumerable<Building> GetByBuildingGroup(int buildingGroupId);

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        Building Get(int id);
    }
}
