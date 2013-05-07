using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房间业务类
    /// </summary>
    public class RemoteRoomService : IRoomService
    {
        #region Method
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <returns></returns>
        public List<Room> GetList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room");

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms.OrderBy(r => r.Id).ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public List<Room> GetListByBuilding(int buildingId)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room?BuildingId=" + buildingId.ToString());

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms.ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <param name="floor">楼层</param>
        /// <returns></returns>
        public List<Room> GetListByBuilding(int buildingId, int floor)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room?buildingId=" + buildingId.ToString() + "&floor=" + floor.ToString());

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms.ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="floorId">楼层ID</param>
        /// <returns></returns>
        public List<Room> GetListByFloor(int floorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public List<Room> GetListByDepartment(int departmentId)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Room?DepartmentId=" + departmentId.ToString());

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms.ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public Room Get(int id)
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
        /// 添加房间
        /// </summary>
        /// <param name="data">房间数据</param>
        /// <returns>房间ID，0:添加失败</returns>
        public int Create(Room data)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Post("Room", data);

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
        /// <param name="data">房间数据</param>
        /// <returns></returns>
        public bool Edit(Room data)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Put("Room", data.Id, data);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 删除房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Delete("Room", id);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// 获取房间属性列表
        /// </summary>
        /// <returns></returns>
        public List<RoomFunctionCode> GetFunctionCodeList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("GeneralProperty?name=RoomFunctionCode");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<IEnumerable<RoomFunctionCode>>().Result;
                return data.ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            throw new NotImplementedException();
        }
        #endregion //Method        
    }
}
