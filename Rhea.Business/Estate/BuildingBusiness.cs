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
        #region Building
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
        /// 获取子建筑
        /// </summary>
        /// <param name="parentId">父级建筑ID</param>
        /// <returns></returns>
        public IEnumerable<Building> GetChildBuildings(int parentId)
        {
            var data = this.buildingRepository.Get().Where(r => r.ParentId == parentId);
            return data;
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
        #endregion //Building

        #region BuildingGroup
        /// <summary>
        /// 获取楼群建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public BuildingGroup GetBuildingGroup(int id)
        {
            IBuildingRepository buildingRepository = new MongoBuildingGroupRepository();
            BuildingGroup data = (BuildingGroup)buildingRepository.Get(id);

            if (data.OrganizeType != (int)BuildingOrganizeType.BuildingGroup || data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 更新楼群
        /// </summary>
        /// <param name="data">楼群对象</param>
        /// <returns></returns>
        public ErrorCode UpdateBuildingGroup(BuildingGroup data)
        {
            IBuildingRepository buildingRepository = new MongoBuildingGroupRepository();
            return buildingRepository.Update(data);
        }
        #endregion //BuildingGroup

        #region Cluster
        /// <summary>
        /// 获取组团
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public Cluster GetCluster(int id)
        {
            IBuildingRepository buildingRepository = new MongoClusterRepository();
            Cluster data = (Cluster)buildingRepository.Get(id);

            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 更新组团
        /// </summary>
        /// <param name="data">组团对象</param>
        /// <returns></returns>
        public ErrorCode UpdateCluster(Cluster data)
        {
            IBuildingRepository buildingRepository = new MongoClusterRepository();
            return buildingRepository.Update(data);
        }
        #endregion //Cluster

        #region Cottage
        /// <summary>
        /// 获取独栋建筑
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public Cottage GetCottage(int id)
        {
            IBuildingRepository buildingRepository = new MongoCottageRepository();
            Cottage data = (Cottage)buildingRepository.Get(id);
            data.Floors = data.Floors.OrderBy(r => r.Number).ToList();

            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 更新独栋
        /// </summary>
        /// <param name="data">独栋对象</param>
        /// <returns></returns>
        public ErrorCode UpdateCottage(Cottage data)
        {
            IBuildingRepository buildingRepository = new MongoCottageRepository();
            Cottage old = (Cottage)buildingRepository.Get(data.BuildingId);
            data.Floors = old.Floors;

            return buildingRepository.Update(data);
        }
        #endregion //Cottage

        #region Subregion
        /// <summary>
        /// 获取分区
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public Subregion GetSubregion(int id)
        {
            IBuildingRepository buildingRepository = new MongoSubregionRepository();
            Subregion data = (Subregion)buildingRepository.Get(id);
            data.Floors = data.Floors.OrderBy(r => r.Number).ToList();

            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 更新分区
        /// </summary>
        /// <param name="data">分区对象</param>
        /// <returns></returns>
        public ErrorCode UpdateSubregion(Subregion data)
        {
            IBuildingRepository buildingRepository = new MongoSubregionRepository();
            Subregion old = (Subregion)buildingRepository.Get(data.BuildingId);
            data.Floors = old.Floors;

            return buildingRepository.Update(data);
        }
        #endregion //Subregion

        #region Block
        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public Block GetBlock(int id)
        {
            IBuildingRepository buildingRepository = new MongoBlockRepository();
            Block data = (Block)buildingRepository.Get(id);
            data.Floors = data.Floors.OrderBy(r => r.Number).ToList();

            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 更新楼宇
        /// </summary>
        /// <param name="data">楼宇对象</param>
        /// <returns></returns>
        public ErrorCode UpdateBlock(Block data)
        {
            IBuildingRepository buildingRepository = new MongoBlockRepository();
            Block old = (Block)buildingRepository.Get(data.BuildingId);
            data.Floors = old.Floors;

            return buildingRepository.Update(data);
        }
        #endregion //Block

        #region Playground
        /// <summary>
        /// 获取操场
        /// </summary>
        /// <param name="id">建筑ID</param>
        /// <returns></returns>
        public Playground GetPlayground(int id)
        {
            IBuildingRepository buildingRepository = new MongoPlaygroundRepository();
            Playground data = (Playground)buildingRepository.Get(id);

            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 更新操场
        /// </summary>
        /// <param name="data">操场对象</param>
        /// <returns></returns>
        public ErrorCode UpdatePlayground(Playground data)
        {
            IBuildingRepository buildingRepository = new MongoPlaygroundRepository();
            return buildingRepository.Update(data);
        }
        #endregion //Playground
        #endregion //Method
    }
}
