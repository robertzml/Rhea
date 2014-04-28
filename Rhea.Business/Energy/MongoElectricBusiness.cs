using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Rhea.Model.Energy;

namespace Rhea.Business.Energy
{
    /// <summary>
    /// 电系统业务类
    /// </summary>
    public class MongoElectricBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context;

        /// <summary>
        /// 日志接口
        /// </summary>
        private ILogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 部门业务类
        /// </summary>
        public MongoElectricBusiness()
        {
            this.context = new RheaMongoContext(RheaServer.EnergyDatabase);         
            this.logBusiness = new MongoLogBusiness();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取绑定房间信息列表
        /// </summary>
        /// <returns></returns>
        public List<RoomMap> GetBindList()
        {
            List<RoomMap> data = new List<RoomMap>();
            var docs = this.context.FindAll(EnergyCollection.RoomMap);

            foreach(BsonDocument doc in docs)
            {
                if (doc["status"].AsInt32 == 1)
                    continue;

                RoomMap map = new RoomMap();
                map.RoomId = doc["roomId"].AsInt32;
                map.NodeId = doc["nodeId"].AsInt64;
                map.szOPCNode = doc.GetValue("szOPCNode", "").AsString;
                map.Multiplying = doc["multiplying"].AsInt32;
                map.Status = doc["status"].AsInt32;
                data.Add(map);
            }

            return data;
        }

        /// <summary>
        /// 获取绑定房间信息
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <returns></returns>
        public RoomMap GetBind(int roomId)
        {
            BsonDocument doc = this.context.FindOne(EnergyCollection.RoomMap, "roomId", roomId);

            if (doc != null)
            {
                RoomMap map = new RoomMap();
                map.RoomId = roomId;
                map.NodeId = doc["nodeId"].AsInt64;
                map.szOPCNode = doc.GetValue("szOPCNode", "").AsString;
                map.Multiplying = doc["multiplying"].AsInt32;
                map.Status = doc["status"].AsInt32;

                return map;
            }
            else
                return null;
        }

        /// <summary>
        /// 绑定房间与电系统数据
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns></returns>
        public bool BindRoom(RoomMap data)
        {
            bool dup = this.context.CheckDuplicate(EnergyCollection.RoomMap, "roomId", data.RoomId);
            if (dup)
                return false;

            BsonDocument doc = new BsonDocument
            {
                { "roomId", data.RoomId },                
                { "nodeId", data.NodeId },
                { "szOPCNode", data.szOPCNode },
                { "multiplying", data.Multiplying },
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(EnergyCollection.RoomMap, doc);

            if (result.Ok)
                return true;
            else
                return false;
        }
        #endregion //Method
    }
}
