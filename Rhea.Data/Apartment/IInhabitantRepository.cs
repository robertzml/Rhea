using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Apartment;
using Rhea.Model;

namespace Rhea.Data.Apartment
{
    /// <summary>
    /// 住户Repository
    /// </summary>
    public interface IInhabitantRepository
    {
        /// <summary>
        /// 获取所有住户
        /// </summary>
        /// <returns></returns>
        IEnumerable<Inhabitant> Get();

        /// <summary>
        /// 获取住户
        /// </summary>
        /// <param name="_id">住户ID</param>
        /// <returns></returns>
        Inhabitant Get(string _id);

        /// <summary>
        /// 添加住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        ErrorCode Create(Inhabitant data);
        
    }
}
