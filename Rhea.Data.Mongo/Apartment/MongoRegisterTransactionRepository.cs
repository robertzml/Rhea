﻿using System;
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
    /// MongoDB 新教职工登记业务记录 Repository
    /// </summary>
    public class MongoRegisterTransactionRepository : ITransactionRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<RegisterTransaction> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 新教职工登记业务记录 Repository
        /// </summary>
        public MongoRegisterTransactionRepository()
        {
            this.repository = new MongoRepository<RegisterTransaction>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有新教职工登记业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApartmentTransaction> Get()
        {
            var data = this.repository.Where(r => r.Type == (int)LogType.ApartmentRegister);
            return data;
        }

        /// <summary>
        /// 获取新教职工登记业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        public ApartmentTransaction Get(string id)
        {
            var data = this.repository.GetById(id);
            return data;
        }

        /// <summary>
        /// 添加业务记录
        /// </summary>
        /// <param name="data">业务记录对象</param>
        /// <returns></returns>
        public ErrorCode Create(ApartmentTransaction data)
        {
            try
            {
                RegisterTransaction tran = (RegisterTransaction)data;
                this.repository.Add(tran);
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
