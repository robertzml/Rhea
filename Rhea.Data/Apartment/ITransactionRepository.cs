using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;
using Rhea.Model.Apartment;

namespace Rhea.Data.Apartment
{
    /// <summary>
    /// 青教业务记录Repository
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// 获取所有业务记录
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApartmentTransaction> Get();

        /// <summary>
        /// 获取业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        ApartmentTransaction Get(string id);

        /// <summary>
        /// 添加业务记录
        /// </summary>
        /// <param name="data">业务记录对象</param>
        /// <returns></returns>
        ErrorCode Create(ApartmentTransaction data);
    }
}