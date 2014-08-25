using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Rhea.Data;
using Rhea.Data.Estate;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 校区Repository类
    /// </summary>
    public class MongoCampusRepository : ICampusRepository
    {
        #region Field
        private IMongoRepository<Campus> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 校区Repository类
        /// </summary>
        public MongoCampusRepository()
        {
            this.repository = new MongoRepository<Campus>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <returns>所有校区</returns>
        public IEnumerable<Campus> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns>校区对象</returns>
        public Campus Get(int id)
        {
            return this.repository.Where(r => r.CampusId == id).Single();
        }

        /// <summary>
        /// 添加校区
        /// </summary>
        /// <param name="data">校区对象</param>
        /// <returns></returns>
        public ErrorCode Create(Campus data)
        {
            try
            {
                this.repository.Add(data);
            }
            catch (Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 更新校区
        /// </summary>
        /// <param name="data">校区对象</param>
        /// <returns></returns>
        public ErrorCode Update(Campus data)
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
