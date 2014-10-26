using Rhea.Model;
using Rhea.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Account
{
    /// <summary>
    /// 权限 Repository 接口
    /// </summary>
    public interface IPrivilegeRepository
    {
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        IEnumerable<Privilege> Get();

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="_id">权限ID</param>
        /// <returns></returns>
        Privilege Get(string _id);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns></returns>
        Privilege GetByName(string name);

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="data">权限对象</param>
        /// <returns></returns>
        ErrorCode Create(Privilege data);

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="data">权限对象</param>
        /// <returns></returns>
        ErrorCode Update(Privilege data);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="_id">权限ID</param>
        /// <returns></returns>
        ErrorCode Delete(string _id);
    }
}
