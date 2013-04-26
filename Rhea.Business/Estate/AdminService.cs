using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房产系统管理事务类
    /// </summary>
    public class AdminService : IAdminService
    {
        public List<ManagerGroup> GetManagerGroupList()
        {
            List<ManagerGroup> data = new List<ManagerGroup>();

            return data;
        }
    }
}
