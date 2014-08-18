using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 校区Repository接口
    /// </summary>
    public interface ICampusRepository
    {
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <returns>所有校区</returns>
        IEnumerable<Campus> Get();

        /// <summary>
        /// 获取校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns>校区对象</returns>
        Campus Get(int id);

        /// <summary>
        /// 更新校区
        /// </summary>
        /// <param name="data">校区对象</param>
        /// <returns></returns>
        ErrorCode Update(Campus data);

        /// <summary>
        /// 校区计数
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}
