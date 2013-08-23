using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// MongoDB楼群业务
    /// </summary>
    public class MongoBuildingGroupBusiness : IBuildingGroupBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context;

        /// <summary>
        /// 备份接口
        /// </summary>
        private IBackupBusiness backupBusiness;

        /// <summary>
        /// 日志接口
        /// </summary>
        private ILogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 楼群业务类
        /// </summary>
        public MongoBuildingGroupBusiness()
        {
            this.context = new RheaMongoContext(RheaServer.EstateDatabase);
            this.backupBusiness = new EstateBackupBusiness();
            this.logBusiness = new MongoLogBusiness();
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private BuildingGroup ModelBind(BsonDocument doc)
        {
            BuildingGroup buildingGroup = new BuildingGroup();
            buildingGroup.Id = doc["id"].AsInt32;
            buildingGroup.Name = doc["name"].AsString;
            buildingGroup.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            buildingGroup.PartMapUrl = doc.GetValue("partMapUrl", "").AsString;
            buildingGroup.CampusId = doc["campusId"].AsInt32;
            buildingGroup.BuildingCount = (int?)doc.GetValue("buildingCount", null);
            buildingGroup.AreaCoeffcient = doc.GetValue("areaCoeffcient", "").AsString;
            buildingGroup.BuildArea = (double?)doc.GetValue("buildArea", null);
            buildingGroup.UsableArea = (double?)doc.GetValue("usableArea", null);
            buildingGroup.Floorage = (double?)doc.GetValue("floorage", null);
            buildingGroup.BuildType = doc.GetValue("buildType", "").AsString;
            buildingGroup.BuildStructure = doc.GetValue("buildStructure", "").AsString;
            buildingGroup.BuildCost = (double?)doc.GetValue("buildCost", null);
            buildingGroup.CurrentValue = (double?)doc.GetValue("currentValue", null);
            buildingGroup.Classified = doc.GetValue("classified", "").AsString;
            buildingGroup.FundsSubject = doc.GetValue("fundsSubject", "").AsString;
            buildingGroup.EquityNumber = doc.GetValue("equityNumber", "").AsString;
            buildingGroup.BuildDate = (DateTime?)doc.GetValue("buildDate", null);
            buildingGroup.FixedYear = (int?)doc.GetValue("fixedYear", null);
            buildingGroup.DesignCompany = doc.GetValue("designCompany", "").AsString;
            buildingGroup.ConstructCompany = doc.GetValue("constructCompany", "").AsString;
            buildingGroup.ManageType = doc.GetValue("manageType", "").AsString;
            buildingGroup.UseType = doc.GetValue("useType").AsInt32;
            buildingGroup.Sort = doc.GetValue("sort", 0).AsInt32;
            buildingGroup.Remark = doc.GetValue("remark", "").AsString;
            buildingGroup.Status = doc.GetValue("status", 0).AsInt32;
            //buildingGroup.UsableArea = GetUsableArea(buildingGroup.Id);

            if (doc.Contains("gallery"))
            {
                BsonArray array = doc["gallery"].AsBsonArray;
                buildingGroup.Gallery = new string[array.Count];
                for (int i = 0; i < array.Count; i++)
                {
                    buildingGroup.Gallery[i] = array[i].AsString;
                }
            }

            if (doc.Contains("log"))
            {
                BsonDocument log = doc["log"].AsBsonDocument;
                buildingGroup.Log._id = log["id"].AsObjectId;
                buildingGroup.Log.UserName = log["name"].AsString;
                buildingGroup.Log.Time = log["time"].AsBsonDateTime.ToLocalTime();
                buildingGroup.Log.Type = log["type"].AsInt32;
            }

            if (buildingGroup.BuildDate != null)
                buildingGroup.BuildDate = ((DateTime)buildingGroup.BuildDate).ToLocalTime();

            return buildingGroup;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取楼群列表
        /// </summary>
        /// <param name="sort">是否按sort排序</param>
        /// <returns></returns>
        public List<BuildingGroup> GetList(bool sort = false)
        {
            List<BuildingGroup> buildingGroups = new List<BuildingGroup>();
            var docs = this.context.FindAll(EstateCollection.BuildingGroup);

            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                BuildingGroup buildingGroup = ModelBind(doc);
                buildingGroups.Add(buildingGroup);
            }

            if (sort)
                buildingGroups = buildingGroups.OrderBy(r => r.Sort).ToList();
            else
                buildingGroups = buildingGroups.OrderBy(r => r.Id).ToList();

            return buildingGroups;
        }

        /// <summary>
        /// 得到楼群完整列表
        /// </summary>
        /// <param name="sort">是否按sort排序</param>
        /// <remarks>包括已删除</remarks>
        /// <returns></returns>
        public List<BuildingGroup> GetFullList(bool sort)
        {
            List<BuildingGroup> buildingGroups = new List<BuildingGroup>();
            var docs = this.context.FindAll(EstateCollection.BuildingGroup);

            foreach (var doc in docs)
            {
                BuildingGroup buildingGroup = ModelBind(doc);
                buildingGroups.Add(buildingGroup);
            }

            if (sort)
                buildingGroups = buildingGroups.OrderBy(r => r.Sort).ToList();
            else
                buildingGroups = buildingGroups.OrderBy(r => r.Id).ToList();

            return buildingGroups;
        }

        /// <summary>
        /// 得到楼群简单列表
        /// </summary>
        /// <param name="sort">是否按sort排序</param>
        /// <returns></returns>
        public List<BuildingGroup> GetSimpleList(bool sort = false)
        {
            List<BuildingGroup> buildingGroups = new List<BuildingGroup>();
            var docs = this.context.FindAll(EstateCollection.BuildingGroup);

            foreach (var doc in docs)
            {
                if (doc.GetValue("status", 0).AsInt32 == 1)
                    continue;
                BuildingGroup buildingGroup = new BuildingGroup
                {
                    Id = doc["id"].AsInt32,
                    Name = doc["name"].AsString,
                    UseType = doc["useType"].AsInt32,
                    Sort = doc["sort"].AsInt32
                };
                buildingGroups.Add(buildingGroup);
            }

            if (sort)
                buildingGroups = buildingGroups.OrderBy(r => r.Sort).ToList();
            else
                buildingGroups = buildingGroups.OrderBy(r => r.Id).ToList();

            return buildingGroups;
        }

        /// <summary>
        /// 获取楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public BuildingGroup Get(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.BuildingGroup, "id", id);
            if (doc != null)
            {
                BuildingGroup buildingGroup = ModelBind(doc);
                if (buildingGroup.Status == 1)
                    return null;

                return buildingGroup;
            }
            else
                return null;
        }

        /// <summary>
        /// 得到楼群名称
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.BuildingGroup, "id", id);

            if (doc != null)
                return doc["name"].AsString;
            else
                return string.Empty;
        }

        /// <summary>
        /// 添加楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <returns>楼群ID</returns>
        public int Create(BuildingGroup data)
        {
            data.Id = this.context.FindSequenceIndex(EstateCollection.BuildingGroup) + 1;

            BsonDocument doc = new BsonDocument //26
            {
                { "id", data.Id },
                { "name", data.Name },
                { "imageUrl", data.ImageUrl },
                { "partMapUrl", data.PartMapUrl },
                { "campusId", data.CampusId },
                { "buildingCount", data.BuildingCount },
                { "areaCoeffcient", data.AreaCoeffcient },
                { "buildArea", data.BuildArea },
                { "usableArea", data.UsableArea },
                { "floorage", data.Floorage },
                { "buildType", data.BuildType },
                { "buildStructure", data.BuildStructure },
                { "buildCost", data.BuildCost },
                { "currentValue", data.CurrentValue },
                { "classified", data.Classified },
                { "fundsSubject", data.FundsSubject },
                { "equityNumber", data.EquityNumber },
                { "buildDate", data.BuildDate },
                { "fixedYear", data.FixedYear },
                { "designCompany", data.DesignCompany },
                { "constructCompany", data.ConstructCompany },
                { "manageType", data.ManageType },     
                { "useType", data.UseType },
                { "sort", data.Sort },
                { "remark", data.Remark },                           
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(EstateCollection.BuildingGroup, doc);

            if (result.HasLastErrorMessage)
                return 0;
            else
                return data.Id;
        }

        /// <summary>
        /// 更新楼群
        /// </summary>
        /// <param name="data">楼群数据</param>
        /// <param name="log">日志信息</param>
        /// <returns></returns>
        public bool Edit(BuildingGroup data)
        {
            var query = Query.EQ("id", data.Id);
            var update = Update.Set("name", data.Name)  //24
                .Set("imageUrl", data.ImageUrl ?? "")
                .Set("partMapUrl", data.PartMapUrl ?? "")
                .Set("campusId", data.CampusId)
                .Set("buildingCount", (BsonValue)data.BuildingCount)
                .Set("areaCoeffcient", data.AreaCoeffcient ?? "")
                .Set("buildArea", (BsonValue)data.BuildArea)
                .Set("usableArea", (BsonValue)data.UsableArea)
                .Set("floorage", (BsonValue)data.Floorage)
                .Set("buildType", data.BuildType ?? "")
                .Set("buildStructure", data.BuildStructure ?? "")
                .Set("buildCost", (BsonValue)data.BuildCost)
                .Set("currentValue", (BsonValue)data.CurrentValue)
                .Set("classified", data.Classified ?? "")
                .Set("fundsSubject", data.FundsSubject ?? "")
                .Set("equityNumber", data.EquityNumber ?? "")
                .Set("buildDate", (BsonValue)data.BuildDate)
                .Set("fixedYear", (BsonValue)data.FixedYear)
                .Set("designCompany", data.DesignCompany ?? "")
                .Set("constructCompany", data.ConstructCompany ?? "")
                .Set("manageType", data.ManageType ?? "")
                .Set("sort", data.Sort)
                .Set("useType", data.UseType)
                .Set("remark", data.Remark ?? "");

            WriteConcernResult result = this.context.Update(EstateCollection.BuildingGroup, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <param name="log">日志信息</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("status", 1);

            WriteConcernResult result = this.context.Update(EstateCollection.BuildingGroup, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 备份楼群
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public bool Backup(int id)
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.BuildingGroup, "id", id);
            doc.Remove("_id");

            bool result = this.backupBusiness.Backup(EstateCollection.BuildingGroupBackup, doc);
            return result;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <param name="log">日志内容</param>
        /// <returns></returns>
        public bool Log(int id, Log log)
        {
            log = this.logBusiness.Insert(log);
            if (log == null)
                return false;

            var query = Query.EQ("id", id);
            var update = Update.Set("log.id", log._id)
                .Set("log.name", log.UserName)
                .Set("log.time", log.Time)
                .Set("log.type", log.Type);

            WriteConcernResult result = this.context.Update(EstateCollection.BuildingGroup, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            var query = Query.NE("status", 1);
            long count = this.context.Count(EstateCollection.BuildingGroup, query);
            return (int)count;
        }

        /// <summary>
        /// 获取使用面积
        /// </summary>
        /// <param name="id">楼群ID</param>
        /// <returns></returns>
        public double GetUsableArea(int id)
        {
            var query = Query.EQ("buildingGroupId", id);
            var buildings = this.context.Find(EstateCollection.Building, query).SetFields("id");

            BsonArray bids = new BsonArray();
            foreach (var building in buildings)
            {
                bids.Add(building["id"]);
            }

            BsonDocument[] pipeline = {
                new BsonDocument {
                    { "$match", new BsonDocument {
                        { "buildingId", new BsonDocument { 
                            { "$in", bids }
                        }}
                    }}
                },
                new BsonDocument {
                    { "$group", new BsonDocument {
                        { "_id", "id" },
                        { "area", new BsonDocument {
                            { "$sum", "$usableArea" }
                        }}
                    }}
                }
            };

            AggregateResult result = this.context.Aggregate(EstateCollection.Room, pipeline);

            var doc = result.ResultDocuments.SingleOrDefault();

            if (doc != null)
                return Math.Round(doc["area"].AsDouble, 2);
            else
                return 0;
        }

        /// <summary>
        /// 归档楼群
        /// </summary>
        /// <param name="log">相关日志</param>
        /// <returns></returns>
        public bool Archive(Log log)
        {
            log = this.logBusiness.Insert(log);
            if (log == null)
                return false;

            var docs = this.context.FindAll(EstateCollection.BuildingGroup);
            List<BsonDocument> newDocs = new List<BsonDocument>();

            foreach (BsonDocument doc in docs)
            {
                doc.Remove("_id");
                doc.Remove("log");
                doc.Add("log", new BsonDocument
                {
                    { "id", log._id },
                    { "name", log.UserName },
                    { "time", log.RelateTime },
                    { "type", log.Type }
                });
                newDocs.Add(doc);
            }

            bool result = this.backupBusiness.Archive(EstateCollection.BuildingGroupBackup, newDocs);
            return result;
        }

        /// <summary>
        /// 导出楼群
        /// </summary>
        /// <returns></returns>
        public byte[] Export()
        {
            StringBuilder sb = new StringBuilder();
            var docs = this.context.FindAll(EstateCollection.BuildingGroup);

            sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}," +
                "{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28}",
                "Id", "名称", "图片", "局部导航", "所属校区", "楼宇栋数", "面积系数", "建筑面积", "使用面积", "占地面积",
                "建造方式", "建筑结构", "建筑物造价", "折旧后现值", "建筑物产别", "建筑物经费科目", "建筑物产权证号",
                "建成日期", "使用年限", "建筑设计单位", "建筑物施工单位", "建筑物房管形式", "使用类型", "排序", "备注",
                "编辑人", "编辑时间", "编辑类型", "状态"));

            foreach (var doc in docs)
            {
                BsonDocument editor = doc["editor"].AsBsonDocument;

                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}," +
                    "{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28}",
                    doc["id"],
                    doc["name"],
                    doc.GetValue("imageUrl", ""),
                    doc.GetValue("partMapUrl", ""),
                    doc["campusId"],
                    doc.GetValue("buildingCount", null),
                    doc.GetValue("areaCoeffcient", ""),
                    doc.GetValue("buildArea", null),
                    doc.GetValue("usableArea", null),
                    doc.GetValue("floorage", null),
                    doc.GetValue("buildType", ""),
                    doc.GetValue("buildStructure", ""),
                    doc.GetValue("buildCost", null),
                    doc.GetValue("currentValue", null),
                    doc.GetValue("classified", ""),
                    doc.GetValue("fundsSubject", ""),
                    doc.GetValue("equityNumber", ""),
                    doc.GetValue("buildDate", null),
                    doc.GetValue("fixedYear", null),
                    doc.GetValue("designCompany", ""),
                    doc.GetValue("constructCompany", ""),
                    doc.GetValue("manageType", ""),
                    doc.GetValue("useType"),
                    doc.GetValue("sort"),
                    doc.GetValue("remark", ""),
                    editor.GetValue("name", ""),
                    editor.GetValue("time", null),
                    editor.GetValue("type", null),
                    doc.GetValue("status", 0)
                ));
            }

            byte[] fileContents = Encoding.Default.GetBytes(sb.ToString());
            return fileContents;
        }
        #endregion //Method
    }
}
