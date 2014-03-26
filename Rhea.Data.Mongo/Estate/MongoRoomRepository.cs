﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Model.Estate;
using MongoDB.Driver.Builders;
using Rhea.Data;

namespace Rhea.Data.Mongo.Estate
{
    /// <summary>
    /// MongoDB 房间Repository
    /// </summary>
    public class MongoRoomRepository : RheaContextBase<Room>, IRoomRepository
    {
        #region Constructor
        public MongoRoomRepository()
        {
            this.repository = new MongoRepository<Room>(RheaServer.EstateDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Room> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id">房间ID</param>
        /// <returns></returns>
        public Room Get(int id)
        {
            var query = Query.EQ("id", id);
            return this.repository.Collection.FindOne(query);
        }

        /// <summary>
        /// 根据楼宇获取房间
        /// </summary>
        /// <param name="buildingId">所属楼宇ID</param>
        /// <returns></returns>
        public IEnumerable<Room> GetByBuilding(int buildingId)
        {
            var query = Query.EQ("buildingId", buildingId);
            return this.repository.Collection.Find(query);
        }
        #endregion //Method
    }
}
