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
    /// MongoDB 用户 Repository
    /// </summary>
    public class MongoUserRepository : IUserRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<User> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 用户 Repository
        /// </summary>
        public MongoUserRepository()
        {
            this.repository = new MongoRepository<User>(RheaServer.RheaDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="_id">用户系统ID</param>
        /// <returns></returns>
        public User Get(string _id)
        {
            return this.repository.GetById(_id);
        }

        /// <summary>
        /// 根据登录名获取用户
        /// </summary>
        /// <param name="userName">登录名</param>
        /// <returns></returns>
        public User GetByUserName(string userName)
        {
            var data = this.repository.Where(r => r.UserName == userName);
            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <returns></returns>
        public ErrorCode Create(User data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.UserName == data.UserName);
                if (dup)
                    return ErrorCode.DuplicateUserName;

                dup = this.repository.Exists(r => r.UserId == data.UserId);
                if (dup)
                    return ErrorCode.DuplicateUserId;

                this.repository.Add(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="data">用户对象</param>
        /// <returns></returns>
        public ErrorCode Update(User data)
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
        #endregion //Method
    }
}
