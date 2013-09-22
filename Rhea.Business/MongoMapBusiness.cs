using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model;

namespace Rhea.Business
{
    public class MongoMapBusiness : IMapBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.RheaDatabase);
        #endregion //Field

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private MapPoint ModelBind(BsonDocument doc)
        {
            MapPoint point = new MapPoint();
            point._id = doc["_id"].AsObjectId.ToString();
            point.TargetId = doc["targetId"].AsInt32;
            point.TargetType = doc["targetType"].AsInt32;
            point.Name = doc["name"].AsString;
            point.Content = doc["content"].AsString;
            point.PointX = doc["pointX"].AsDouble;
            point.PointY = doc["pointY"].AsDouble;
            point.Zoom = doc["zoom"].AsInt32;
            point.Pin = doc["pin"].AsString;

            return point;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 得到所有标记点
        /// </summary>
        /// <returns></returns>
        public List<MapPoint> GetPointList()
        {
            List<MapPoint> data = new List<MapPoint>();

            var docs = this.context.FindAll(RheaCollection.MapPoint);

            foreach (BsonDocument doc in docs)
            {
                MapPoint point = ModelBind(doc);
                data.Add(point);
            }

            return data;
        }

        /// <summary>
        /// 得到所有标记点
        /// </summary>
        /// <param name="type">标记类型</param>
        /// <returns></returns>
        public List<MapPoint> GetPointList(int type)
        {
            List<MapPoint> data = new List<MapPoint>();

            var docs = this.context.Find(RheaCollection.MapPoint, "targetType", type);

            foreach (BsonDocument doc in docs)
            {
                MapPoint point = ModelBind(doc);
                data.Add(point);
            }

            return data;
        }

        /// <summary>
        /// 得到标记点
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public MapPoint GetPoint(string id)
        {
            ObjectId _id = new ObjectId(id);
            var doc = this.context.FindOne(RheaCollection.MapPoint, "_id", _id);

            if (doc != null)
            {
                MapPoint point = ModelBind(doc);
                return point;
            }
            else
                return null;
        }

        /// <summary>
        /// 添加点
        /// </summary>
        /// <param name="point">点</param>
        /// <returns></returns>
        public bool CreatePoint(MapPoint point)
        {
            BsonDocument doc = new BsonDocument
            {
                { "targetId", point.TargetId },
                { "targetType", point.TargetType },
                { "name", point.Name },
                { "content", point.Content },
                { "pointX", point.PointX },
                { "pointY", point.PointY },
                { "zoom", point.Zoom },
                { "pin", point.Pin }
            };

            WriteConcernResult result = this.context.Insert(RheaCollection.MapPoint, doc);
            if (result.Ok)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 编辑点
        /// </summary>
        /// <param name="data">点</param>
        /// <returns></returns>
        public bool EditPoint(MapPoint data)
        {
            ObjectId _id = new ObjectId(data._id);
            var query = Query.EQ("_id", _id);

            var update = Update.Set("targetId", data.TargetId)
                .Set("targetType", data.TargetType)
                .Set("name", data.Name)
                .Set("content", data.Content)
                .Set("pointX", data.PointX)
                .Set("pointY", data.PointY)
                .Set("zoom", data.Zoom)
                .Set("pin", data.Pin);

            WriteConcernResult result = this.context.Update(RheaCollection.MapPoint, query, update);
            if (result.Ok)
                return true;
            else
                return false;
        }
        #endregion //Method
    }
}
