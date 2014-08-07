using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data;
using Rhea.Data.Personnel;
using Rhea.Data.Mongo.Personnel;
using Rhea.Model;
using Rhea.Model.Personnel;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 部门业务类
    /// </summary>
    public class DepartmentBusiness
    {
        #region Field
        /// <summary>
        /// 部门Repository
        /// </summary>
        private IDepartmentRepository departmentRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 部门业务类
        /// </summary>
        public DepartmentBusiness()
        {
            this.departmentRepository = new MongoDepartmentRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Department> Get()
        {
            return this.departmentRepository.Get().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public Department Get(int id)
        {
            var data = this.departmentRepository.Get(id);
            if (data.Status == 1)
                return null;
            else
                return data;
        }
        #endregion //Method
    }
}
