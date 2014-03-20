using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Model.Estate;
using Rhea.Data.Mongo.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 楼群业务
    /// </summary>
    public class BuildingGroupBusiness
    {
        #region Field
        /// <summary>
        /// 楼群Repository
        /// </summary>
        private IBuildingGroupRepository buildingGroupRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 楼群业务
        /// </summary>
        public BuildingGroupBusiness()
        {
            this.buildingGroupRepository = new MongoBuildingGroupRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有楼群
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BuildingGroup> Get()
        {
            var data = this.buildingGroupRepository.Get();
            return data.Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public BuildingGroup Get(int id)
        {
            var data = this.buildingGroupRepository.Get(id);
            if (data.Status == 1)
                return null;
            else
                return data;
        }
        #endregion //Method
    }
}
