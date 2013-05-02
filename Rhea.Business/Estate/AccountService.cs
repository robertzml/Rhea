using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Server;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Entities;
using Rhea.Common;

namespace Rhea.Business.Estate
{
    public class AccountService : IAccountService
    {
        public User ValidateUser(string userName, string password)
        {
            RheaContext context = new RheaContext();

            BsonDocument doc = context.FindOne("user", "name", userName);
            if (doc != null)
            {
                string pass = doc["password"].AsString;
                if (Hasher.MD5Encrypt(password) != pass)
                    return null;

                User user = new User();
                user.Id = doc["id"].AsInt32;
                user.Name = userName;
                user.UserType = doc["userType"].AsInt32;
                user.ManagerType = doc["managerType"].AsInt32;

                return user;
            }
            return null;
        }
    }
}
