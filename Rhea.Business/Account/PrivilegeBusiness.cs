using Rhea.Data;
using Rhea.Data.Account;
using Rhea.Data.Mongo;
using Rhea.Data.Mongo.Account;
using Rhea.Model;
using Rhea.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Account
{
    /// <summary>
    /// 权限业务类
    /// </summary>
    public class PrivilegeBusiness
    {
        #region Field
        /// <summary>
        /// 权限Repository
        /// </summary>
        private IPrivilegeRepository privilegeRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 权限业务
        /// </summary>
        public PrivilegeBusiness()
        {
            this.privilegeRepository = new MongoPrivilegeRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Privilege> Get()
        {
            return this.privilegeRepository.Get();
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public Privilege Get(string id)
        {
            return this.privilegeRepository.Get(id);
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns></returns>
        public Privilege GetByName(string name)
        {
            return this.privilegeRepository.GetByName(name);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="data">权限对象</param>
        /// <returns></returns>
        public ErrorCode Create(Privilege data)
        {
            data.Status = 0;
            return this.privilegeRepository.Create(data);
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="data">权限对象</param>
        /// <returns></returns>
        public ErrorCode Update(Privilege data)
        {
            return this.privilegeRepository.Update(data);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public ErrorCode Delete(string id)
        {
            return this.privilegeRepository.Delete(id);
        }
        #endregion //Method
    }
}
