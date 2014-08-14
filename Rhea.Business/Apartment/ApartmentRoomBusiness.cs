﻿using Rhea.Business.Estate;
using Rhea.Data.Apartment;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model.Apartment;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 青教房间业务类
    /// </summary>
    public class ApartmentRoomBusiness
    {
        #region Field
        /// <summary>
        /// 房间Repository
        /// </summary>
        private IRoomRepository roomRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 青教房间业务类
        /// </summary>
        public ApartmentRoomBusiness()
        {
            this.roomRepository = new MongoApartmentRoomRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 根据建筑获取房间
        /// </summary>
        /// <param name="buildingId">所属建筑ID</param>
        /// <returns></returns>
        public IEnumerable<ApartmentRoom> GetByBuilding(int buildingId)
        {
            BuildingBusiness buildingBusiness = new BuildingBusiness();
            var building = buildingBusiness.Get(buildingId);

            if (building.HasChild)
            {
                var children = buildingBusiness.GetChildBuildings(buildingId);
                return this.roomRepository.GetByBuildings(children.Select(r => r.BuildingId).ToArray()).Where(r => r.Status != 1) as IEnumerable<ApartmentRoom>;
            }
            else
            {
                var data = this.roomRepository.GetByBuilding(buildingId).Where(r => r.Status != 1).Cast<ApartmentRoom>();
                return data;
            }
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public ApartmentRoom Get(int id)
        {
            var data = this.roomRepository.Get(id);
            if (data == null || data.Status == 1)
                return null;
            else
                return (ApartmentRoom)data;
        }

        /// <summary>
        /// 获取房间当前住户
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <remarks>
        /// 仅限正常居住和挂职居住
        /// </remarks>
        /// <returns></returns>
        public Inhabitant GetCurrentInhabitant(int id)
        {
            var data = this.roomRepository.Get(id);
            if (data == null || data.Status == 1)
                return null;

            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var records = recordBusiness.GetByRoom(id).Where(r => r.Status == 0);
            if (records == null || records.Count() == 0)
                return null;

            var record = records.First();
            if ((ResideType)record.ResideType == ResideType.Normal || (ResideType)record.ResideType == ResideType.Guest)
            {
                InhabitantBusiness inhabitantBusiness = new InhabitantBusiness();
                return inhabitantBusiness.Get(record.InhabitantId);
            }
            else
                return null;
        }
        #endregion //Method
    }
}