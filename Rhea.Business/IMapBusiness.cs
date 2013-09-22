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
        /// 得到所有标记点
        /// </summary>
        /// <param name="type">标记类型</param>
        /// <returns></returns>
        List<MapPoint> GetPointList(int type);

        /// <summary>
        /// 得到标记点
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        MapPoint GetPoint(string id);

        /// <summary>
        /// 添加点
        /// </summary>
        /// <param name="point">点</param>
        /// <returns></returns>
        bool CreatePoint(MapPoint point);

        /// <summary>
        /// 编辑点
        /// </summary>
        /// <param name="data">点</param>
        /// <returns></returns>
        bool EditPoint(MapPoint data);
    }
}
