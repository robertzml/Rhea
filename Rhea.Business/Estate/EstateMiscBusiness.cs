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
        #endregion //Method
    }
}
