using Rhea.Data;
using Rhea.Data.Estate;
using Rhea.Data.Mongo;
using Rhea.Data.Mongo.Estate;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// 备份业务
        /// </summary>
        private BackupBusiness backupBusiness;

        /// <summary>
        /// 日志业务
        /// </summary>
        private LogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 建筑业务类
        /// </summary>
        public BuildingBusiness()
        {
            this.buildingRepository = new MongoBuildingRepository();
            this.backupBusiness = new BackupBusiness();
            this.logBusiness = new LogBusiness();
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
            if (data == null || data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 获取一级建筑
        /// </summary>
        /// <remarks>
        /// 类型为1:楼群, 2:组团, 3:独栋, 6:操场 的建筑
        /// </remarks>
        /// <returns></returns>
        public IEnumerable<Building> GetTopBuildings()
        {
            var data = this.buildingRepository.Get().Where(r => r.OrganizeType != (int)BuildingOrganizeType.Subregion && r.OrganizeType != (int)BuildingOrganizeType.Block);
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
        /// 获取所有子建筑
        /// </summary>
        /// <remarks>
        /// 仅子建筑能包含房间
        /// </remarks>
        /// <returns></returns>
        public IEnumerable<Building> GetChildBuildings()
        {
            var data = this.buildingRepository.Get().Where(r => r.HasChild == false && r.Status != 1);
            return data;
        }

        /// <summary>
        /// 获取子建筑
        /// </summary>
        /// <param name="parentId">父级建筑ID</param>
        /// <returns></returns>
        public IEnumerable<Building> GetChildBuildings(int parentId)
        {
            var data = this.buildingRepository.Get().Where(r => r.ParentId == parentId && r.Status != 1);
            return data;
        }

        /// <summary>
        /// 添加建筑
        /// </summary>
        /// <param name="data">建筑对象</param>
        /// <returns></returns>
        public ErrorCode Create(Building data)
        {
            return this.buildingRepository.Create(data);
        }

        /// <summary>
        /// 获取楼层
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        public Floor GetFloor(int buildingId, int floorId)
        {
            var building = this.buildingRepository.Get(buildingId);
            if (building == null || building.Status == 1)
                return null;
            else
            {
                switch ((BuildingOrganizeType)building.OrganizeType)
                {
                    case BuildingOrganizeType.BuildingGroup:
                        return null;
                    case BuildingOrganizeType.Cluster:
                        return null;

                    case BuildingOrganizeType.Cottage:
                        IBuildingRepository cottageRepository = new MongoCottageRepository();
                        var cottage = (Cottage)cottageRepository.Get(buildingId);
                        return cottage.Floors.SingleOrDefault(r => r.FloorId == floorId);

                    case BuildingOrganizeType.Subregion:
                        IBuildingRepository subregionRepository = new MongoSubregionRepository();
                        var subregion = (Subregion)subregionRepository.Get(buildingId);
                        return subregion.Floors.SingleOrDefault(r => r.FloorId == floorId);

                    case BuildingOrganizeType.Block:
                        IBuildingRepository blockRepository = new MongoBlockRepository();
                        var block = (Block)blockRepository.Get(buildingId);
                        return block.Floors.SingleOrDefault(r => r.FloorId == floorId);

                    case BuildingOrganizeType.Playground:
                        return null;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取最后楼层ID
        /// </summary>
        /// <returns></returns>
        public int GetLastFloorId()
        {
            BsonRepository br = new BsonRepository();
            return br.GetLastFloorId();
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        public ErrorCode CreateFloor(int buildingId, Floor data)
        {
            var building = this.buildingRepository.Get(buildingId);
            if (building == null || building.Status == 1)
                return ErrorCode.ObjectDeleted;
            else
            {
                switch ((BuildingOrganizeType)building.OrganizeType)
                {
                    case BuildingOrganizeType.BuildingGroup:
                        return ErrorCode.NotImplement;

                    case BuildingOrganizeType.Cluster:
                        return ErrorCode.NotImplement;

                    case BuildingOrganizeType.Cottage:
                        IBuildingRepository cottageRepository = new MongoCottageRepository();
                        return cottageRepository.CreateFloor(buildingId, data);

                    case BuildingOrganizeType.Subregion:
                        IBuildingRepository subregionRepository = new MongoSubregionRepository();
                        return subregionRepository.CreateFloor(buildingId, data);

                    case BuildingOrganizeType.Block:
                        IBuildingRepository blockRepository = new MongoBlockRepository();
                        return blockRepository.CreateFloor(buildingId, data);

                    case BuildingOrganizeType.Playground:
                        return ErrorCode.NotImplement;
                }

                return ErrorCode.NotImplement;
            }
        }

        /// <summary>
        /// 编辑楼层
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        public ErrorCode UpdateFloor(int buildingId, Floor data)
        {
            var building = this.buildingRepository.Get(buildingId);
            if (building == null || building.Status == 1)
                return ErrorCode.ObjectDeleted;
            else
            {
                switch ((BuildingOrganizeType)building.OrganizeType)
                {
                    case BuildingOrganizeType.BuildingGroup:
                        return ErrorCode.NotImplement;

                    case BuildingOrganizeType.Cluster:
                        return ErrorCode.NotImplement;

                    case BuildingOrganizeType.Cottage:
                        IBuildingRepository cottageRepository = new MongoCottageRepository();
                        return cottageRepository.UpdateFloor(buildingId, data);

                    case BuildingOrganizeType.Subregion:
                        IBuildingRepository subregionRepository = new MongoSubregionRepository();
                        return subregionRepository.UpdateFloor(buildingId, data);

                    case BuildingOrganizeType.Block:
                        IBuildingRepository blockRepository = new MongoBlockRepository();
                        return blockRepository.UpdateFloor(buildingId, data);

                    case BuildingOrganizeType.Playground:
                        return ErrorCode.NotImplement;
                }

                return ErrorCode.NotImplement;
            }
        }

        /// <summary>
        /// 编辑平面图
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <param name="data">楼层对象</param>
        /// <returns></returns>
        public ErrorCode UpdateSvg(int buildingId, Floor data)
        {
            Floor floor = this.GetFloor(buildingId, data.FloorId);
            if (floor == null)
                return ErrorCode.ObjectNotFound;

            floor.ImageUrl = data.ImageUrl;

            return this.UpdateFloor(buildingId, floor);
        }

        /// <summary>
        /// 备份平面图
        /// </summary>
        /// <param name="baseFolder">网站根目录</param>
        /// <param name="svgFileName">原平面图名称</param>
        /// <returns>备份SVG文件名</returns>
        public string BackupFloorSvg(string baseFolder, string svgFileName)
        {
            string oldFilePath = baseFolder + RheaConstant.SvgRoot + svgFileName;

            if (!File.Exists(oldFilePath))
                return "";

            string name = Path.GetFileNameWithoutExtension(svgFileName);
            string ext = Path.GetExtension(svgFileName);
            DateTime now = DateTime.Now;
            string newFileName = string.Format("{0}-{1}{2}{3}{4}{5}{6}{7}", name, now.Year, now.Month, now.Day,
                now.Hour, now.Minute, now.Second, ext);

            try
            {
                File.Copy(oldFilePath, baseFolder + RheaConstant.SvgBackup + newFileName);
            }
            catch (Exception)
            {
                return "";
            }
            return newFileName;
        }

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        public ErrorCode DeleteFloor(int buildingId, int floorId)
        {
            var building = this.buildingRepository.Get(buildingId);
            if (building == null || building.Status == 1)
                return ErrorCode.ObjectDeleted;
            else
            {
                switch ((BuildingOrganizeType)building.OrganizeType)
                {
                    case BuildingOrganizeType.BuildingGroup:
                        return ErrorCode.NotImplement;

                    case BuildingOrganizeType.Cluster:
                        return ErrorCode.NotImplement;

                    case BuildingOrganizeType.Cottage:
                        IBuildingRepository cottageRepository = new MongoCottageRepository();
                        return cottageRepository.DeleteFloor(buildingId, floorId);

                    case BuildingOrganizeType.Subregion:
                        IBuildingRepository subregionRepository = new MongoSubregionRepository();
                        return subregionRepository.DeleteFloor(buildingId, floorId);

                    case BuildingOrganizeType.Block:
                        IBuildingRepository blockRepository = new MongoBlockRepository();
                        return blockRepository.DeleteFloor(buildingId, floorId);

                    case BuildingOrganizeType.Playground:
                        return ErrorCode.NotImplement;
                }

                return ErrorCode.NotImplement;
            }
        }

        /// <summary>
        /// 备份建筑
        /// </summary>
        /// <param name="_id">建筑系统ID</param>
        /// <returns></returns>
        public ErrorCode Backup(string _id)
        {
            ErrorCode result = this.backupBusiness.Backup(RheaServer.EstateDatabase, EstateCollection.Building, EstateCollection.BuildingBackup, _id);
            return result;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_id">建筑系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public ErrorCode Log(string _id, Log log)
        {
            ErrorCode result = this.logBusiness.Create(log);
            if (result != ErrorCode.Success)
                return result;

            result = this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Building, _id, log);
            return result;
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

            if (data == null || data.OrganizeType != (int)BuildingOrganizeType.BuildingGroup || data.Status == 1)
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

            if (data == null || data.Status == 1 || data.OrganizeType != (int)BuildingOrganizeType.Cluster)
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

            if (data == null || data.Status == 1 || data.OrganizeType != (int)BuildingOrganizeType.Cottage)
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

            if (data == null || data.Status == 1 || data.OrganizeType != (int)BuildingOrganizeType.Subregion)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 获取下属子分区
        /// </summary>
        /// <param name="parentId">楼群ID</param>
        /// <returns></returns>
        public IEnumerable<Subregion> GetChildSubregions(int parentId)
        {
            IBuildingRepository buildingRepository = new MongoSubregionRepository();
            var data = buildingRepository.GetChildren(parentId) as IEnumerable<Subregion>;
            return data.Where(r => r.Status != 1);
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

            if (data == null || data.Status == 1 || data.OrganizeType != (int)BuildingOrganizeType.Block)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 获取组团内楼宇
        /// </summary>
        /// <param name="parentId">组团ID</param>
        /// <returns></returns>
        public IEnumerable<Block> GetChildBlocks(int parentId)
        {
            IBuildingRepository buildingRepository = new MongoBlockRepository();
            var data = buildingRepository.GetChildren(parentId) as IEnumerable<Block>;
            return data.Where(r => r.Status != 1);
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

            if (data == null || data.Status == 1 || data.OrganizeType != (int)BuildingOrganizeType.Playground)
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
