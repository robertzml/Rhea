using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Rhea.Business
{
    /// <summary>
    /// Api服务
    /// </summary>
    public class ApiService
    {
        #region Field
        /// <summary>
        /// API服务器
        /// </summary>
        private string host = "http://localhost:11500/";
        #endregion //Field

        #region Method
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string queryString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List all products.
            HttpResponseMessage response = client.GetAsync("api/" + queryString).Result;  // Blocking call!
            return response;
        }
        #endregion //Method
    }
}
