using System;
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
                return buildingGroups;
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
                return buildings;
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
    }
}
