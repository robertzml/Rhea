using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 房间Repository接口
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="data">房间对象</param>
        /// <returns></returns>
        ErrorCode Create(Room data);
    }
}
