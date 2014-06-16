using Rhea.Data.Estate;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 独栋Repository
    /// </summary>
    public class MongoCottageRepository : MongoBuildingRepository
    {
        #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<Cottage> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 独栋Repository
        /// </summary>
        public MongoCottageRepository()
        {
            this.repository = new MongoRepository<Cottage>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public override Building Get(int id)
        {
            return this.repository.Where(r => r.BuildingId == id).First();
        }

        /// <summary>
        /// 更新建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        public override ErrorCode Update(Building data)
        {
            try
            {
                Cottage cottage = (Cottage)data;
                this.repository.Update(cottage);
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
