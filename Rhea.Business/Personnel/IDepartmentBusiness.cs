using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Model.Personnel;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 部门业务接口
    /// </summary>
    public interface IDepartmentBusiness
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

        /// <summary>
        /// 得到部门名称
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        string GetName(int id);

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        bool Edit(Department data);
    }
}
