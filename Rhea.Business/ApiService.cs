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
        /// GET数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string queryString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        
            HttpResponseMessage response = client.GetAsync("api/" + queryString).Result;  // Blocking call!
            return response;
        }

        /// <summary>
        /// PUT数据
        /// </summary>
        /// <param name="queryString">Uri路径</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public HttpResponseMessage Put(string queryString, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PutAsJsonAsync("api/" + queryString, data).Result;
            return response;
        }
        #endregion //Method
    }
}
