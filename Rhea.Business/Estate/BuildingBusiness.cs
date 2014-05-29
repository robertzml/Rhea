using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model;
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
        
        /// <summary>
        /// 获取建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public Building Get(int id)
        {
            var data = this.buildingRepository.Get(id);
            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 获取楼群建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public BuildingGroup GetBuildingGroup(int id)
        {
            IBuildingRepository buildingRepository = new MongoBuildingGroupRepository();
            BuildingGroup data = (BuildingGroup)buildingRepository.Get(id);

            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 获取所有父级建筑
        /// </summary>
        /// <remarks>
        /// 类型为，1:楼群, 2:组团，的建筑
        /// </remarks>
        /// <returns></returns>
        public IEnumerable<Building> GetParentBuildings()
        {
            var data1 = this.buildingRepository.GetByOrganizeType((int)BuildingOrganizeType.BuildingGroup);
            var data2 = this.buildingRepository.GetByOrganizeType((int)BuildingOrganizeType.Cluster);

            data1 = data1.Union(data2).Where(r => r.Status != 1);

            return data1;
        }

        /// <summary>
        /// 添加建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        public ErrorCode Create(Building data)
        {
            data.Status = 0;
            return this.buildingRepository.Create(data);
        }
        #endregion //Method
    }
}
