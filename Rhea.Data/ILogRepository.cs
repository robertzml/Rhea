using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;

namespace Rhea.Data
{
    /// <summary>
    /// 日志 Repository 接口
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        IEnumerable<Log> Get();

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="_id">日志ID</param>
        /// <returns></returns>
        Log Get(string _id);

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="data">日志对象</param>
        /// <returns></returns>
        ErrorCode Create(Log data);
    }
}
