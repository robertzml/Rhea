using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Rhea.UI
{
    public static class AppInitializer
    {
        #region Function
        static void LoadConnectionString()
        {
            string connectionStringName = "mongoConnection";
            ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
        #endregion //Function

        public static void Init()
        {

        }

        public static string ConnectionString;
    }
}