using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Rhea.Data;
using Rhea.Data.Estate;
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
        /// 校区计数
        /// </summary>
        /// <returns>状态不为1的校区数量</returns>
        public int Count()
        {
            long count = this.repository.Count(r => r.Status != 1);
            return (int)count;
        }

        /// <summary>
        /// 更新校区
        /// </summary>
        /// <param name="data">校区对象</param>
        /// <returns></returns>
        public bool Update(Campus data)
        {
            try
            {
                this.repository.Update(data);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion //Method
    }
}
