using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model;
using Rhea.Model.Estate;

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
        /// <remarks>状态不为1的对象</remarks>
        /// <returns></returns>
        public IEnumerable<Campus> Get()
        {
            return this.campusRepository.Get().Where(r => r.Status != 1);
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
            else
                return data;
        }

        /// <summary>
        /// 更新校区
        /// </summary>
        /// <param name="data">校区对象</param>
        /// <returns></returns>
        public ErrorCode Update(Campus data)
        {
            return this.campusRepository.Update(data);
        }
        #endregion //Method
    }
}
