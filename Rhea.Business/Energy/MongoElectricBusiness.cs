using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model;
using Rhea.Model.Energy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
        /// 电能数据库连接
        /// </summary>
        private SqlServerContext sqlContext;

        /// <summary>
        /// 电能数据库连接字符串
        /// </summary>
        private string sqlConnectionString = "server=210.28.16.79;uid=robert;pwd=msconfig1209;database=CEMS";

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
            this.sqlContext = new SqlServerContext(sqlConnectionString);
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

            foreach (BsonDocument doc in docs)
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

        /// <summary>
        /// 获取电数据
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public List<HourElectric> GetHourValueByDay(int roomId, DateTime datetime)
        {
            RoomMap map = GetBind(roomId);
            if (map == null)
                return null;

            List<HourElectric> data = new List<HourElectric>();

            string sql = string.Format("SELECT * FROM T_BD_HourValue WHERE F_NodeID = {0} AND " +
                "CONVERT(VARCHAR(10),F_HourDate,120) = '{1}'", map.NodeId, datetime.ToString("yyyy-MM-dd"));
            DataTable dt = this.sqlContext.ExecuteQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                HourElectric elec = new HourElectric();
                elec.RoomId = roomId;
                elec.NodeId = map.NodeId;
                elec.HourDate = Convert.ToDateTime(row["F_HourDate"]);
                elec.HourDataValue = Convert.ToDecimal(row["F_HourDataValue"]);
                elec.StartHourDate = Convert.ToDateTime(row["F_StartHourDate"]);
                elec.StartHourData = Convert.ToDecimal(row["F_StartHourData"]);
                elec.EndHourDate = Convert.ToDateTime(row["F_EndHourDate"]);
                elec.EndHourData = Convert.ToDecimal(row["F_EndHourData"]);

                data.Add(elec);
            }

            return data;
        }

        /// <summary>
        /// 获取多日电数据
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public List<HourElectric> GetHourValueByDayLength(int roomId, DateTime startDate, DateTime endDate)
        {
            RoomMap map = GetBind(roomId);
            if (map == null)
                return null;

            List<HourElectric> data = new List<HourElectric>();

            string sql = string.Format("SELECT * FROM T_BD_HourValue WHERE F_NodeID = {0} AND " +
                "F_HourDate between '{1}' and '{2}'", map.NodeId, startDate.ToString("yyyy-MM-dd"), 
                endDate.ToString("yyyy-MM-dd"));
            DataTable dt = this.sqlContext.ExecuteQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                HourElectric elec = new HourElectric();
                elec.RoomId = roomId;
                elec.NodeId = map.NodeId;
                elec.HourDate = Convert.ToDateTime(row["F_HourDate"]);
                elec.HourDataValue = Convert.ToDecimal(row["F_HourDataValue"]);
                elec.StartHourDate = Convert.ToDateTime(row["F_StartHourDate"]);
                elec.StartHourData = Convert.ToDecimal(row["F_StartHourData"]);
                elec.EndHourDate = Convert.ToDateTime(row["F_EndHourDate"]);
                elec.EndHourData = Convert.ToDecimal(row["F_EndHourData"]);

                data.Add(elec);
            }

            return data;
        }
        #endregion //Method
    }
}
