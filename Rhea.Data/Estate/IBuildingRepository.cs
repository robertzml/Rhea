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
        /// 添加建筑
        /// </summary>
        /// <param name="model">建筑对象</param>
        /// <returns></returns>
        ErrorCode Create(Building model);
    }
}
