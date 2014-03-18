using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;

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
    }
}
