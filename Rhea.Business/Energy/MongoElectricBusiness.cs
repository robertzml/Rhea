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
