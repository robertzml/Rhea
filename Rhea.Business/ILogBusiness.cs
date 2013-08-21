using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model;

namespace Rhea.Business
{
    public interface ILogBusiness
    {
        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        Log Insert(Log log);

        /// <summary>
        /// 显示所有日志
        /// </summary>
        /// <returns></returns>
        List<Log> GetList();

        /// <summary>
        /// 显示所有日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <returns></returns>
        List<Log> GetList(int type);
    }
}
