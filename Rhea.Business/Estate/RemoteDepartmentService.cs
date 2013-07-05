﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Model.Personnel;
using Rhea.Business.Personnel;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 部门业务类
    /// </summary>
    public class RemoteDepartmentService : IDepartmentService
    {
        #region Method
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public List<Department> GetList()
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("department");

            if (response.IsSuccessStatusCode)
            {
                var departments = response.Content.ReadAsAsync<IEnumerable<Department>>().Result;
                return departments.ToList();
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
        public Department Get(int id)
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

        public bool Edit(Department data)
        {
            throw new NotImplementedException();
        }
        #endregion //Method        
    }
}
