using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;

namespace Rhea.Data
{
    /// <summary>
    /// 字典Repository接口
    /// </summary>
    public interface IDictionaryRepository
    {
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns>返回简单型字典集</returns>
        IEnumerable<Dictionary> Get();

        /// <summary>
        /// 获取字典集
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        Dictionary Get(string name);

        /// <summary>
        /// 添加字典集
        /// </summary>
        /// <param name="data">字典对象</param>
        /// <returns></returns>
        ErrorCode Create(Dictionary data);

        /// <summary>
        /// 保存字典集
        /// </summary>
        /// <param name="data">字典对象</param>
        /// <returns></returns>
        ErrorCode Edit(Dictionary data);
    }
}
