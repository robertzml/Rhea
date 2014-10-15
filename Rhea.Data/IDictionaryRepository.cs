using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 获取文本属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        List<String> GetTextProperty(string name);

        /// <summary>
        /// 获取键值属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <returns></returns>
        Dictionary<int, string> GetPairProperty(string name);

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

        /// <summary>
        /// 更新文本属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <param name="property">属性</param>
        /// <returns></returns>
        ErrorCode UpdateTextProperty(string name, List<string> property);

        /// <summary>
        /// 更新键值属性
        /// </summary>
        /// <param name="name">字典集名称</param>
        /// <param name="property">属性</param>
        /// <returns></returns>
        ErrorCode UpdatePairProperty(string name, Dictionary<int, string> property);
    }
}
