﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business
{
    /// <summary>
    /// 房产服务
    /// </summary>
    public class EstateService
    {
        #region BuildingGroupService
        /// <summary>
        /// 获取楼群列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BuildingGroup> GetBuildingGroupList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("buildinggroup");
           
            if (response.IsSuccessStatusCode)
            {                           
                var buildingGroups = response.Content.ReadAsAsync<IEnumerable<BuildingGroup>>().Result;           
                return buildingGroups.OrderBy(r => r.Id);
            }
            else
            {
                  return null;
            }
        }

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public BuildingGroup GetBuildingGroup(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("BuildingGroup?id=" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                var buildingGroup = response.Content.ReadAsAsync<BuildingGroup>().Result;                
                return buildingGroup;
            }
            else
                return null;
        }

        /// <summary>
        /// 更新楼群
        /// </summary>
        /// <param name="buildingGroup">楼群模型</param>
        /// <returns></returns>
        public bool UpdateBuildingGroup(BuildingGroup buildingGroup)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Put("BuildingGroup", buildingGroup.Id, buildingGroup);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 添加楼群
        /// </summary>
        /// <param name="buildingGroup">楼群模型</param>
        /// <returns></returns>
        public bool CreateBuildingGroup(BuildingGroup buildingGroup)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("BuildingGroup", buildingGroup);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 删除楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public bool DeleteBuildingGroup(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Delete("BuildingGroup", id);

            return response.IsSuccessStatusCode;
        }
        #endregion //BuildingGroupService

        #region BuildingService
        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Building> GetBuildingList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("building");

            if (response.IsSuccessStatusCode)
            {
                var buildings = response.Content.ReadAsAsync<IEnumerable<Building>>().Result;
                return buildings.OrderBy(r => r.Id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public Building GetBuilding(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Building?id=" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                var building = response.Content.ReadAsAsync<Building>().Result;
                return building;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public IEnumerable<Building> GetBuildingByGroup(int buildingGroupId)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Building?BuildingGroupId=" + buildingGroupId.ToString());

            if (response.IsSuccessStatusCode)
            {
                var buildings = response.Content.ReadAsAsync<IEnumerable<Building>>().Result;
                return buildings;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加楼宇
        /// </summary>
        /// <param name="building">楼宇模型</param>
        /// <returns></returns>
        public bool CreateBuilding(Building building)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("Building", building);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 更新楼宇
        /// </summary>
        /// <param name="building">楼宇模型</param>
        /// <returns></returns>
        public bool UpdateBuilding(Building building)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Put("Building", building.Id, building);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 删除楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public bool DeleteBuilding(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Delete("Building", id);

            return response.IsSuccessStatusCode;
        }
        #endregion //BuildingService

        #region RoomService
        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public IEnumerable<Room> GetRoomByBuilding(int buildingId)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room?BuildingId=" + buildingId.ToString());

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms;
            }
            else
                return null;
        }
        #endregion //RoomService
    }
}
