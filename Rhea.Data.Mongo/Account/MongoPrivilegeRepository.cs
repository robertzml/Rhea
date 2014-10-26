using Rhea.Data.Account;
using Rhea.Model;
using Rhea.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Mongo.Account
{
    /// <summary>
    /// MongoDB 权限 Repository
    /// </summary>
    public class MongoPrivilegeRepository : IPrivilegeRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<Privilege> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 权限 Repository
        /// </summary>
        public MongoPrivilegeRepository()
        {
            this.repository = new MongoRepository<Privilege>(RheaServer.RheaDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Privilege> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="_id">权限ID</param>
        /// <returns></returns>
        public Privilege Get(string _id)
        {
            return this.repository.GetById(_id);
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns></returns>
        public Privilege GetByName(string name)
        {
            return this.repository.SingleOrDefault(r => r.Name == name);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="data">权限对象</param>
        /// <returns></returns>
        public ErrorCode Create(Privilege data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.Name == data.Name);
                if (dup)
                    return ErrorCode.DuplicateName;

                this.repository.Add(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="data">权限对象</param>
        /// <returns></returns>
        public ErrorCode Update(Privilege data)
        {
            try
            {
                this.repository.Update(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="_id">权限ID</param>
        /// <returns></returns>
        public ErrorCode Delete(string _id)
        {
            try
            {
                this.repository.Delete(_id);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}