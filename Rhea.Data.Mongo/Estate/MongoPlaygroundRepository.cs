﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 操场 Repository
    /// </summary>
    public class MongoPlaygroundRepository : MongoBuildingRepository
    {
         #region Field
        /// <summary>
        /// repository对象
        /// </summary>
        private IMongoRepository<Playground> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 操场 Repository
        /// </summary>
        public MongoPlaygroundRepository()
        {
            this.repository = new MongoRepository<Playground>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns>操场</returns>
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
                Playground playground = (Playground)data;
                this.repository.Update(playground);
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
