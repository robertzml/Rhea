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
                        
            HttpResponseMessage response = client.GetAsync("api/" + queryString).Result;
            return response;
        }

        /// <summary>
        /// PUT数据
        /// </summary>
        /// <param name="queryString">Uri路径</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public HttpResponseMessage Put(string queryString, int id, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);           
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PutAsJsonAsync("api/" + queryString + "/" + id.ToString(), data).Result;
            return response;
        }

        /// <summary>
        /// PUT数据
        /// </summary>
        /// <param name="queryString">Uri路径</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        /// <remarks>适用于复杂参数</remarks>
        public HttpResponseMessage Put(string queryString, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PutAsJsonAsync("api/" + queryString, data).Result;
            return response;
        }

        /// <summary>
        /// POST数据
        /// </summary>
        /// <param name="queryString">Uri路径</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public HttpResponseMessage Post(string queryString, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/" + queryString, data).Result;
            return response;
        }

        /// <summary>
        /// Delete数据
        /// </summary>
        /// <param name="queryString">Uri路径</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public HttpResponseMessage Delete(string queryString, int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.DeleteAsync("api/" + queryString + "/" + id.ToString()).Result;
            return response;
        }

        /// <summary>
        /// Delete数据
        /// </summary>
        /// <param name="queryString">Uri路径</param>        
        /// <returns></returns>
        /// <remarks>适用于复杂参数</remarks>
        public HttpResponseMessage Delete(string queryString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(host);         
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.DeleteAsync("api/" + queryString).Result;
            return response;
        }
        #endregion //Method
    }
}
