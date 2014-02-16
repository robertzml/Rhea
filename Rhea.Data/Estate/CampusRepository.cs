using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;
using MongoDB.Bson;

namespace Rhea.Data.Estate
{
    public class CampusRepository
    {
        #region Field
        private RheaMongoContext context;
        #endregion //Field

        #region Constructor
        public CampusRepository()
        {
            this.context = new RheaMongoContext("estate");
        }
        #endregion //Constructor

        #region Function
        private Campus ModelBind(BsonDocument doc)
        {
            Campus campus = new Campus();
            campus.Id = doc["id"].AsInt32;
            campus.Name = doc["name"].AsString;
            campus.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            campus.Remark = doc.GetValue("remark", "").AsString;
            campus.Status = doc.GetValue("status", 0).AsInt32;

            if (doc.Contains("log"))
            {
                BsonDocument log = doc["log"].AsBsonDocument;
                campus.Log._id = log["id"].AsObjectId;
                campus.Log.UserName = log["name"].AsString;
                campus.Log.Time = log["time"].AsBsonDateTime.ToLocalTime();
                campus.Log.Type = log["type"].AsInt32;
            }

            return campus;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 得到全部校区
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Campus> Get()
        {
            var data = context.FindAll("campus");

            List<Campus> campus = new List<Campus>();

            foreach(var r in data)
            {
                Campus c = ModelBind(r);
                campus.Add(c);
            }

            return campus;
        }

        public Campus Get(int id)
        {
            var data = context.FindOne("campus", "id", id);
            Campus campus = ModelBind(data);
            return campus;
        }
        #endregion //Method
    }
}
