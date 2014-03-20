using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;

namespace Rhea.Data.Estate
{
    public interface IBuildingGroupRepository
    {
        /// <summary>
        /// 获取所有楼群
        /// </summary>
        /// <returns></returns>
        IEnumerable<BuildingGroup> Get();

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        BuildingGroup Get(int id);
    }
}
