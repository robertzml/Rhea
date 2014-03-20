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
    /// 楼宇业务类
    /// </summary>
    public class BuildingBusiness
    {
        #region Field
        /// <summary>
        /// 楼宇Reposiotry
        /// </summary>
        private IBuildingRepository buildingRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 楼宇业务类
        /// </summary>
        public BuildingBusiness()
        {
            this.buildingRepository = new MongoBuildingRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有楼宇
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Building> Get()
        {
            var data = this.buildingRepository.Get();
            return data.Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public Building Get(int id)
        {
            var data = this.buildingRepository.Get(id);
            if (data.Status != 1)
                return data;
            else
                return null;
        }
        #endregion //Method
    }
}
