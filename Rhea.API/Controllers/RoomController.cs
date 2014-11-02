using Rhea.API.Models;
using Rhea.Business.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Rhea.API.Controllers
{
    /// <summary>
    /// 房间控制器
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoomController : ApiController
    {
        #region Field
        /// <summary>
        /// 房间业务对象
        /// </summary>
        private RoomBusiness roomBusiness;
        #endregion //Field

        #region Constructor
        public RoomController()
        {
            this.roomBusiness = new RoomBusiness();
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 模型映射
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Room BindRoom(Rhea.Model.Estate.Room model)
        {
            if (model == null)
                return null;

            Room room = new Room();
            room.Id = model.RoomId;
            room.Name = model.Name;
            room.Number = model.Number;
            room.Floor = model.Floor;
            room.Span = model.Span;
            room.Orientation = model.Orientation;
            room.UsableArea = model.UsableArea;
            room.BuildingId = model.BuildingId;
            room.DepartmentId = model.DepartmentId;
            room.FirstCode = model.Function.FirstCode;
            room.SecondCode = model.Function.SecondCode;
            room.ClassifyName = model.Function.ClassifyName;
            room.FunctionProperty = model.Function.FunctionProperty;
            room.LastUpdateTime = model.Log.Time;

            return room;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 获取所有房间数据
        /// </summary>
        /// <returns>所有房间数据</returns>
        public IEnumerable<Room> Get()
        {
            var model = this.roomBusiness.Get().ToList();
            List<Room> data = new List<Room>();
            foreach (var item in model)
            {
                data.Add(BindRoom(item));
            }

            return data;
        }

        /// <summary>
        /// 获取房间数据
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns>房间ID</returns>
        public Room Get(int id)
        {
            var model = this.roomBusiness.Get(id);
            Room data = BindRoom(model);
            return data;
        }
        #endregion //Action
    }
}
