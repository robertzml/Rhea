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
    /// 居住记录Repository
    /// </summary>
    public interface IResideRecordRepository
    {
        /// <summary>
        /// 添加居住记录
        /// </summary>
        /// <param name="data">居住记录对象</param>
        /// <returns></returns>
        ErrorCode Create(ResideRecord data);
    }
}
