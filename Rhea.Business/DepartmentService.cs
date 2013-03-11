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
    /// 部门服务
    /// </summary>
    public class DepartmentService
    {
        public IEnumerable<Department> GetDepartmentList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("department");

            if (response.IsSuccessStatusCode)
            {
                var departments = response.Content.ReadAsAsync<IEnumerable<Department>>().Result;
                return departments;
            }
            else
            {
                return null;
            }
        }
    }
}
