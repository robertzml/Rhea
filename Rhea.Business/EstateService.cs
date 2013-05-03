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
        /// 获取房间
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public IEnumerable<Room> GetRoomByDepartment(int departmentId)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room?DepartmentId=" + departmentId.ToString());

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

        /// <summary>
        /// 获取对象数量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetEntitySize(int type)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Statistic?type=" + type.ToString());

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<int>().Result;
                return data;
            }
            else
                return 0;
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
