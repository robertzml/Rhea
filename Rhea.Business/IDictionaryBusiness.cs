using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Business
{
    /// <summary>
    /// 字典业务
    /// </summary>
    public interface IDictionaryBusiness
    {
        /// <summary>
        /// 得到组合字典集
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <returns></returns>
        Dictionary<int, string> GetCombineDict(string name);

        /// <summary>
        /// 得到非组合字典集
        /// </summary>
        /// <param name="name">字典名称</param>
        /// <returns></returns>
        string[] GetDict(string name);

        /// <summary>
        /// 得到字典项值
        /// </summary>
        /// <param name="dictName">字典名称</param>
        /// <param name="id">字典项ID</param>
        /// <returns></returns>
        string GetItemValue(string dictName, int id);
    }
}
