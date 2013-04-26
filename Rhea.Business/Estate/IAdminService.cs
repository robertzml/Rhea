using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Estate;

namespace Rhea.Business.Estate
{
    public interface IAdminService
    {
        List<ManagerGroup> GetManagerGroupList();
    }
}
