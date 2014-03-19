using MongoDB.Driver.Linq;
using Rhea.Data.Estate;
using Rhea.Data.Server;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 校区Repository类
    /// </summary>
    public class MongoCampusRepository : RheaContextBase<Campus>, ICampusRepository
    {
        #region Field
       
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
        /// <remarks>状态不为1的对象</remarks>
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
            var query = Query.EQ("id", id);
            return this.repository.Collection.FindOne(query);
        }

        /// <summary>
        /// 校区计数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.repository.Collection.Count(query);
            return (int)count;
        }
        #endregion //Method
    }
}
