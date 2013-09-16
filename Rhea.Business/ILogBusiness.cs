using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
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
        /// 得到日志
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns></returns>
        Log Get(string id);

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

        /// <summary>
        /// 显示所有日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <returns></returns>
        List<Log> GetList(int[] type);

        /// <summary>
        /// 得到日志
        /// </summary>
        /// <param name="relateTime">相关时间</param>
        /// <param name="type">日志类型</param> 
        /// <returns></returns>
        /// <remarks>通过归档日期找到相关日志</remarks>
        Log Get(DateTime relateTime, int type);
    }
}
