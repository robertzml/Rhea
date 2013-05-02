using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Estate;

namespace Rhea.Business
{
    public interface IAdminService
    {
        /// <summary>
        /// 获取管理组列表
        /// </summary>
        /// <returns></returns>
        List<ManagerGroup> GetManagerGroupList();

        /// <summary>
        /// 添加管理组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool CreateManagerGroup(ManagerGroup data);
    }
}
