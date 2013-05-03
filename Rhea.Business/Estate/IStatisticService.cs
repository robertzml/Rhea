using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 统计业务接口
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// 获取统计面积数据
        /// </summary>
        /// <param name="type">统计类型</param>
        /// <remarks>type=1:学院分类用房面积</remarks>
        /// <returns></returns>
        T GetStatisticArea<T>(int type);

        /// <summary>
        /// 获取对象数量
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        int GetEntitySize(int type);
    }
}
