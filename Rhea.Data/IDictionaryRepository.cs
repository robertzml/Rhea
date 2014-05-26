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
        IEnumerable<Dictionary> Get();
    }
}
