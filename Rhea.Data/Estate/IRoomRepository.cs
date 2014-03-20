using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;

namespace Rhea.Data.Estate
{
    /// <summary>
    /// 房间Repository接口
    /// </summary>
    public interface IRoomRepository
    {
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        IEnumerable<Room> Get();
        
        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        Room Get(int id);
    }
}
