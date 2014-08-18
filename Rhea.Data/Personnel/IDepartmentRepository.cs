using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Personnel;
using Rhea.Model;

namespace Rhea.Data.Personnel
{
    /// <summary>
    /// 部门接口Repository
    /// </summary>
    public interface IDepartmentRepository
    {
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        IEnumerable<Department> Get();

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门代码</param>
        /// <returns></returns>
        Department Get(int id);

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="data">部门对象</param>
        /// <returns></returns>
        ErrorCode Create(Department data);

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="data">部门对象</param>
        /// <returns></returns>
        ErrorCode Update(Department data);
    }
}
