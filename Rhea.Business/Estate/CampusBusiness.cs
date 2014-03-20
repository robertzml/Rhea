using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 校区业务类
    /// </summary>
    public class CampusBusiness
    {
        #region Field
        /// <summary>
        /// 校区Repository
        /// </summary>
        private ICampusRepository campusRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 校区业务类
        /// </summary>
        public CampusBusiness()
        {
            this.campusRepository = new MongoCampusRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Campus> Get()
        {
            var data = this.campusRepository.Get();
            return data.Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public Campus Get(int id)
        {
            var data = this.campusRepository.Get(id);
            if (data.Status == 1)
                return null;
            return data;
        }

        /// <summary>
        /// 获取校区数量
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this.campusRepository.Count();
        }

        /// <summary>
        /// 编辑校区
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns></returns>
        public int Edit(Campus data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
