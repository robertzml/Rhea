using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 住户业务类
        /// </summary>
        public InhabitantBusiness()
        {
            this.inhabitantRepository = new MongoInhabitantRepository();
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
        /// 编辑住户
        /// </summary>
        /// <param name="data">住户对象</param>
        /// <returns></returns>
        public ErrorCode Update(Inhabitant data)
        {
            return this.inhabitantRepository.Update(data);
        }            
        #endregion //Method
    }
}
