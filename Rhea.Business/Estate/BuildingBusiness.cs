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
    /// 建筑业务类
    /// </summary>
    public class BuildingBusiness
    {
        #region Field
        /// <summary>
        /// 建筑Repository
        /// </summary>
        private IBuildingRepository buildingRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 建筑业务类
        /// </summary>
        public BuildingBusiness()
        {
            this.buildingRepository = new MongoBuildingRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有建筑
        /// </summary>
        /// <returns>状态不为1的对象</returns>
        public IEnumerable<Building> Get()
        {
            return this.buildingRepository.Get().Where(r => r.Status != 1);
        }
        #endregion //Method
    }
}
