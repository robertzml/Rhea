using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Estate;
using Rhea.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房产系统管理事务类
    /// </summary>
    public class AdminService : IAdminService
    {
        public List<ManagerGroup> GetManagerGroupList()
        {
            List<ManagerGroup> data = new List<ManagerGroup>();

            RheaMongoContext context = new RheaMongoContext();
            List<BsonDocument> docs = context.Find("userGroup", "type", 1);

            foreach (BsonDocument doc in docs)
            {
                ManagerGroup g = new ManagerGroup
                {
                    Id = doc["id"].AsInt32,
                    Name = doc["name"].AsString
                };
                data.Add(g);
            }

            return data;
        }

        public bool CreateManagerGroup(ManagerGroup data)
        {
            throw new NotImplementedException();
        }
    }
}
