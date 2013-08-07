using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 校区业务接口
    /// </summary>
    public interface ICampusBusiness
    {
        /// <summary>
        /// 得到校区列表
        /// </summary>
        /// <returns></returns>
        List<Campus> GetList();

        /// <summary>
        /// 得到校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        Campus Get(int id);

        /// <summary>
        /// 得到校区名称
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        string GetName(int id);

        /// <summary>
        /// 获取校区总数
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}
