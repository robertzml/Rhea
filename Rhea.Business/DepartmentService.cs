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
        #region Method
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public Department GetDepartment(int id)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("department?id=" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                var department = response.Content.ReadAsAsync<Department>().Result;
                return department;
            }
            else
                return null;
        }
        #endregion //Method
    }
}
