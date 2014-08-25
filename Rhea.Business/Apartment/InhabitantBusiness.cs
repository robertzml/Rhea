using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data;
using Rhea.Data.Apartment;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 住户业务类
    /// </summary>
    public class InhabitantBusiness
    {
        #region Field
        /// <summary>
        /// 住户Repository
        /// </summary>
        private IInhabitantRepository inhabitantRepository;

        /// <summary>
        /// 日志业务
        /// </summary>
        private LogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 住户业务类
        /// </summary>
        public InhabitantBusiness()
        {
            this.inhabitantRepository = new MongoInhabitantRepository();
            this.logBusiness = new LogBusiness();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有住户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Inhabitant> Get()
        {
            return this.inhabitantRepository.Get();
        }

        /// <summary>
        /// 获取住户
        /// </summary>
        /// <param name="_id">住户ID</param>
        /// <returns></returns>
        public Inhabitant Get(string _id)
        {
            return this.inhabitantRepository.Get(_id);
        }

        /// <summary>
        /// 添加住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        public ErrorCode Create(Inhabitant data)
        {
            return this.inhabitantRepository.Create(data);
        }

        /// <summary>
        /// 编辑住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        public ErrorCode Update(Inhabitant data)
        {
            return this.inhabitantRepository.Update(data);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_id">住户系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public ErrorCode Log(string _id, Log log)
        {
            ErrorCode result = this.logBusiness.Create(log);
            if (result != ErrorCode.Success)
                return result;

            result = this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Inhabitant, _id, log);
            return result;
        }

        /// <summary>
        /// 更新住户日志信息
        /// </summary>
        /// <param name="_id">住户系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        /// <remarks>
        /// 日志信息已存在
        /// </remarks>
        public ErrorCode LogItem(string _id, Log log)
        {
            return this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Inhabitant, _id, log);
        }
        #endregion //Method
    }
}
