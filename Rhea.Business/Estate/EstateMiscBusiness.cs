using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Estate;
using Rhea.Data.Server;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房产杂项业务
    /// </summary>
    public class EstateMiscBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.EstateDatabase);
        #endregion //Field

        #region Method
        /// <summary>
        /// 得到图片展示数据
        /// </summary>
        /// <returns></returns>
        public List<GalleryModel> GetGallery()
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Misc, "name", "gallery");
            BsonArray array = doc["value"].AsBsonArray;

            List<GalleryModel> data = new List<GalleryModel>();
            foreach (var item in array)
            {
                GalleryModel model = new GalleryModel
                {
                    ImageUrl = RheaConstant.ImagesRoot + item["image"].AsString,
                    ThumbnailUrl = RheaConstant.ImagesRoot + item["thumb"].AsString,
                    Description = item["description"].AsString
                };
                data.Add(model);
            }

            return data;
        }

        /// <summary>
        /// 得到土地证
        /// </summary>
        /// <returns></returns>
        public string GetLandCertificate()
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Misc, "name", "landCertificate");
            string url = RheaConstant.ImagesRoot + doc["value"].AsString;
            return url;
        }

        /// <summary>
        /// 得到土地类型
        /// </summary>
        /// <returns></returns>
        public List<LandTypeModel> GetLandType()
        {
            BsonDocument doc = this.context.FindOne(EstateCollection.Misc, "name", "landType");
            BsonArray array = doc["value"].AsBsonArray;

            List<LandTypeModel> data = new List<LandTypeModel>();
            foreach (var item in array)
            {
                LandTypeModel model = new LandTypeModel
                {
                    Name = item["name"].AsString,
                    Title = item["title"].AsString,
                    Area = item["value"].AsDouble
                };
                data.Add(model);
            }

            return data;
        }
        #endregion //Method
    }
}
