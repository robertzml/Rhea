using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Account;
using Rhea.Model;
using Rhea.Model.Account;

namespace Rhea.Data.Mongo.Account
{
    /// <summary>
    /// MongoDB 用户组 Repository
    /// </summary>
    public class MongoUserGroupRepository : IUserGroupRepository
    {
         #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<UserGroup> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 用户组 Repository
        /// </summary>
        public MongoUserGroupRepository()
        {
            this.repository = new MongoRepository<UserGroup>(RheaServer.RheaDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserGroup> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="id">用户组ID</param>
        /// <returns></returns>
        public UserGroup Get(int id)
        {
            var data = this.repository.Where(r => r.UserGroupId == id);
            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="name">用户组名称</param>
        /// <returns></returns>
        public UserGroup Get(string name)
        {
            var data = this.repository.Where(r => r.Name == name);
            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="data">用户组对象</param>
        /// <returns></returns>
        public ErrorCode Create(UserGroup data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.UserGroupId == data.UserGroupId);
                if (dup)
                    return ErrorCode.DuplicateId;

                this.repository.Add(data);
            }
            catch(Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
