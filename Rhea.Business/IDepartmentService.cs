using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Entities;

namespace Rhea.Business
{
    /// <summary>
    /// 部门业务接口
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        List<Department> GetList();

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        Department Get(int id);
    }
}
