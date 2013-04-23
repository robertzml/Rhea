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
        public int CreateBuildingGroup(BuildingGroup buildingGroup)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("BuildingGroup", buildingGroup);

            if (response.IsSuccessStatusCode)
            {
                int id = response.Content.ReadAsAsync<int>().Result;
                return id;
            }
            else
                return 0;
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
        /// <returns>0:添加失败</returns>
        public int CreateBuilding(Building building)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("Building", building);

            if (response.IsSuccessStatusCode)
            {
                int id = response.Content.ReadAsAsync<int>().Result;
                return id;
            }
            else
                return 0;
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

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns></returns>
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
        /// 更新楼层
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层数据</param>
        /// <returns></returns>
        public bool UpdateFloor(int buildingId, Floor floor)
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
        #endregion //BuildingService

        #region RoomService
        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public Room GetRoom(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room?Id=" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                var room = response.Content.ReadAsAsync<Room>().Result;
                return room;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
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

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public IEnumerable<Room> GetRoomByBuilding(int buildingId, int floor)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room?buildingId=" + buildingId.ToString() + "&floor=" + floor.ToString());

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="room">房间数据</param>
        /// <returns>房间ID，0:添加失败</returns>
        public int CreateRoom(Room room)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("Room", room);

            if (response.IsSuccessStatusCode)
            {
                int id = response.Content.ReadAsAsync<int>().Result;
                return id;
            }
            else
                return 0;
        }

        /// <summary>
        /// 编辑房间
        /// </summary>
        /// <param name="room">房间数据</param>
        /// <returns></returns>
        public bool UpdateRoom(Room room)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Put("Room", room.Id, room);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public bool DeleteRoom(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Delete("Room", id);

            return response.IsSuccessStatusCode;
        }
        #endregion //RoomService

        #region Statistic Service
        /// <summary>
        /// 得到统计面积数据
        /// </summary>
        /// <param name="type">统计类型</param>
        /// <remarks>type=1:学院分类用房面积</remarks>
        /// <returns></returns>
        public T GetStatisticArea<T>(int type)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Statistic?type=" + type.ToString());

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<T>().Result;
                return data;
            }
            else
                return default(T);
        }
        #endregion //Statistic Service

        #region General Service
        /// <summary>
        /// 获取房间属性列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoomFunctionCode> GetFunctionCodeList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("GeneralProperty?name=RoomFunctionCode");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<IEnumerable<RoomFunctionCode>>().Result;
                return data;
            }
            else
                return null;
        }
        #endregion //General Service
    }
}
