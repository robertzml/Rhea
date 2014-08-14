using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;

namespace Rhea.Data.Mongo.Apartment
{
    /// <summary>
    /// MongoDB 住户 Repository
    /// </summary>
    public class MongoInhabitantRepository : IInhabitantRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<Inhabitant> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 住户 Repository
        /// </summary>
        public MongoInhabitantRepository()
        {
            this.repository = new MongoRepository<Inhabitant>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有住户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Inhabitant> Get()
        {
            return this.repository.AsEnumerable().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取住户
        /// </summary>
        /// <param name="_id">住户ID</param>
        /// <returns></returns>
        public Inhabitant Get(string _id)
        {
            var data = this.repository.GetById(_id);
            if (data == null || data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 添加住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        public ErrorCode Create(Inhabitant data)
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
        /// 编辑住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        public ErrorCode Update(Inhabitant data)
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
