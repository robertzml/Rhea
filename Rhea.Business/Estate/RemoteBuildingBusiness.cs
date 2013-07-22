using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 楼宇业务类
    /// </summary>
    public class RemoteBuildingBusiness : IBuildingBusiness
    {
        #region Method
        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <returns></returns>
        public List<Building> GetList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("building");

            if (response.IsSuccessStatusCode)
            {
                var buildings = response.Content.ReadAsAsync<IEnumerable<Building>>().Result;
                return buildings.OrderBy(r => r.Id).ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取楼宇列表
        /// </summary>
        /// <param name="buildingGroupId">所属楼群ID</param>
        /// <returns></returns>
        public List<Building> GetListByBuildingGroup(int buildingGroupId)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Building?BuildingGroupId=" + buildingGroupId.ToString());

            if (response.IsSuccessStatusCode)
            {
                var buildings = response.Content.ReadAsAsync<IEnumerable<Building>>().Result;
                return buildings.ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// 获取楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public Building Get(int id)
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
        /// 添加楼宇
        /// </summary>
        /// <param name="data">楼宇数据</param>
        /// <returns>楼宇ID</returns>
        public int Create(Building data)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("Building", data);

            if (response.IsSuccessStatusCode)
            {
                int id = response.Content.ReadAsAsync<int>().Result;
                return id;
            }
            else
                return 0;
        }

        /// <summary>
        /// 编辑楼宇
        /// </summary>
        /// <param name="data">楼宇数据</param>
        /// <returns></returns>
        public bool Edit(Building data)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Put("Building", data.Id, data);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 删除楼宇
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Delete("Building", id);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns>楼层ID</returns>
        public int CreateFloor(int buildingId, Floor floor)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("Building?id=" + buildingId.ToString(), floor);

            if (response.IsSuccessStatusCode)
            {
                int id = response.Content.ReadAsAsync<int>().Result;
                return id;
            }
            else
                return 0;
        }

        /// <summary>
        /// 编辑楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns></returns>
        public bool EditFloor(int buildingId, Floor floor)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Put("Building?id=" + buildingId.ToString() + "&floorId=" + floor.Id, floor);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        public bool DeleteFloor(int buildingId, int floorId)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Delete("Building?id=" + buildingId.ToString() + "&floorId=" + floorId);
            return response.IsSuccessStatusCode;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Floor GetFloor(int id)
        {
            throw new NotImplementedException();
        }

        public List<Building> GetListByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public string GetName(int id)
        {
            throw new NotImplementedException();
        }

        public int FloorCount()
        {
            throw new NotImplementedException();
        }

        public double GetUsableArea(int buildingId)
        {
            throw new NotImplementedException();
        }

        public double GetFloorUsableArea(int buildingId, int floor)
        {
            throw new NotImplementedException();
        }
        #endregion //Method       
    }
}
