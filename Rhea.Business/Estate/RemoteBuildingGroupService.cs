using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business.Estate
{
    public class RemoteBuildingGroupService : IBuildingGroupService
    {
        #region Method
        /// <summary>
        /// 获取楼群列表
        /// </summary>
        /// <returns></returns>
        public List<BuildingGroup> GetList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("buildinggroup");

            if (response.IsSuccessStatusCode)
            {
                var buildingGroups = response.Content.ReadAsAsync<IEnumerable<BuildingGroup>>().Result;
                return buildingGroups.OrderBy(r => r.Id).ToList();
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
        public BuildingGroup Get(int id)
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
        /// 添加楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <returns>楼群ID</returns>
        public int Create(BuildingGroup data)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("BuildingGroup", data);

            if (response.IsSuccessStatusCode)
            {
                int id = response.Content.ReadAsAsync<int>().Result;
                return id;
            }
            else
                return 0;
        }

        /// <summary>
        /// 更新楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <returns></returns>
        public bool Update(BuildingGroup data)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Put("BuildingGroup", data.Id, data);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 删除楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Delete("BuildingGroup", id);

            return response.IsSuccessStatusCode;
        }
        #endregion //Method
    }
}
