using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business
{
    /// <summary>
    /// 房产服务
    /// </summary>
    public class EstateService
    {
        #region Statistic Service
        /// <summary>
        /// 得到统计面积数据
        /// </summary>
        /// <param name="type">统计类型</param>
        /// <remarks>type=1:学院分类用房面积</remarks>
        /// <returns></returns>
        public T GetStatisticArea<T>(int type)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Statistic?type=" + type.ToString());

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<T>().Result;
                return data;
            }
            else
                return default(T);
        }

        /// <summary>
        /// 获取对象数量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetEntitySize(int type)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Statistic?type=" + type.ToString());

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<int>().Result;
                return data;
            }
            else
                return 0;
        }
        #endregion //Statistic Service       
    }
}
