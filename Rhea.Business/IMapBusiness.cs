using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model;

namespace Rhea.Business
{
    public interface IMapBusiness
    {
        /// <summary>
        /// 得到所有标记点
        /// </summary>
        /// <returns></returns>
        List<MapPoint> GetPointList();

        /// <summary>
        /// 添加点
        /// </summary>
        /// <param name="point">点</param>
        /// <returns></returns>
        bool CreatePoint(MapPoint point);
    }
}
