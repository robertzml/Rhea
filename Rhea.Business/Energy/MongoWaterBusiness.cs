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
    /// 水系统业务类
    /// </summary>
    public class MongoWaterBusiness
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
        private string sqlConnectionString = "server=210.28.16.79;uid=robert;pwd=msconfig1209;database=CWMS";

        /// <summary>
        /// 日志接口
        /// </summary>
        private ILogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 部门业务类
        /// </summary>
        public MongoWaterBusiness()
        {
            this.context = new RheaMongoContext(RheaServer.EnergyDatabase);
            this.sqlContext = new SqlServerContext(sqlConnectionString);
            this.logBusiness = new MongoLogBusiness();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取绑定楼宇信息列表
        /// </summary>
        /// <returns></returns>
        public List<WaterMap> GetBindList()
        {
            List<WaterMap> data = new List<WaterMap>();
            var docs = this.context.FindAll(EnergyCollection.WaterMap);

            foreach (BsonDocument doc in docs)
            {
                if (doc["status"].AsInt32 == 1)
                    continue;

                WaterMap map = new WaterMap();
                map.BuildingId = doc["buildingId"].AsInt32;
                map.NodeId = doc["nodeId"].AsInt64;
                map.szOPCNode = doc.GetValue("szOPCNode", "").AsString;
                map.Status = doc["status"].AsInt32;
                data.Add(map);
            }

            return data;
        }

        /// <summary>
        /// 获取绑定楼宇信息
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <returns></returns>
        public WaterMap GetBind(int buildingId)
        {
            BsonDocument doc = this.context.FindOne(EnergyCollection.WaterMap, "buildingId", buildingId);

            if (doc != null)
            {
                WaterMap map = new WaterMap();
                map.BuildingId = doc["buildingId"].AsInt32;
                map.NodeId = doc["nodeId"].AsInt64;
                map.szOPCNode = doc.GetValue("szOPCNode", "").AsString;
                map.Status = doc["status"].AsInt32;

                return map;
            }
            else
                return null;
        }

        /// <summary>
        /// 绑定楼宇与水系统数据
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <returns></returns>
        public bool BindBuilding(WaterMap data)
        {
            bool dup = this.context.CheckDuplicate(EnergyCollection.WaterMap, "buildingId", data.BuildingId);
            if (dup)
                return false;

            BsonDocument doc = new BsonDocument
            {
                { "buildingId", data.BuildingId },                
                { "nodeId", data.NodeId },
                { "szOPCNode", data.szOPCNode },              
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(EnergyCollection.WaterMap, doc);

            if (result.Ok)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取水数据
        /// </summary>
        /// <param name="buildingId">楼宇ID</param>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public List<HourWater> GetHourValueByDay(int buildingId, DateTime datetime)
        {
            WaterMap map = GetBind(buildingId);
            if (map == null)
                return null;

            List<HourWater> tmp = new List<HourWater>();

            DateTime begin = datetime.Date;
            DateTime end = datetime.Date.AddDays(1);

            string sql = string.Format("SELECT * FROM DB_AmmeterDatas WHERE NodeID = {0} AND " +
                "ReadingDate between '{1}' AND '{2}'", map.NodeId, begin.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            DataTable dt = this.sqlContext.ExecuteQuery(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                HourWater water = new HourWater();
                water.ReadingDate = Convert.ToDateTime(row["ReadingDate"]);
                water.AmmeterData = Convert.ToDecimal(row["AmmeterData"]);

                if (i != 0 && water.ReadingDate.Hour == Convert.ToDateTime(dt.Rows[i - 1]["ReadingDate"]).Hour)
                    continue;
                else
                    tmp.Add(water);
            }

            List<HourWater> data = new List<HourWater>();
            for (int i = 0; i < tmp.Count - 1; i++)
            {
                HourWater water = new HourWater();
                water.BuildingId = buildingId;
                water.NodeId = map.NodeId;
                water.ReadingDate = tmp[i].ReadingDate;
                water.HourValue = tmp[i + 1].AmmeterData - tmp[i].AmmeterData;

                data.Add(water);
            }

            return data;
        }
        #endregion //Method
    }
}
