using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business
{
    /// <summary>
    /// 系统管理接口
    /// </summary>
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


        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        List<UserGroup> GetUserGroupList(); 
    }
}
