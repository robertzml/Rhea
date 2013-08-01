using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    public class MongoCampusBusiness : ICampusBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);
        #endregion //Field

        #region Method
        /// <summary>
        /// 得到校区列表
        /// </summary>
        /// <returns></returns>
        public List<Campus> GetList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 得到校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public Campus Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取校区总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.context.Count(EstateCollection.Campus, query);
            return (int)count;
        }
        #endregion //Method
    }
}
