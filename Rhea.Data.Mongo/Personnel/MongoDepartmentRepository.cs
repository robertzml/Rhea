using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Personnel;
using Rhea.Model;
using Rhea.Model.Personnel;

namespace Rhea.Data.Mongo.Personnel
{
    /// <summary>
    /// MongoDB 部门 Repository
    /// </summary>
    public class MongoDepartmentRepository : IDepartmentRepository
    {
        #region Field
        /// <summary>
        /// Repository对象
        /// </summary>
        private IMongoRepository<Department> repository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// MongoDB 部门 Repository
        /// </summary>
        public MongoDepartmentRepository()
        {
            this.repository = new MongoRepository<Department>(RheaServer.PersonnelDatabase);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Department> Get()
        {
            return this.repository.AsEnumerable();
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门代码</param>
        /// <returns></returns>
        public Department Get(int id)
        {
            var data = this.repository.Where(r => r.DepartmentId == id);
            if (data.Count() == 0)
                return null;
            else
                return data.First();
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="data">部门对象</param>
        /// <returns></returns>
        public ErrorCode Create(Department data)
        {
            try
            {
                bool dup = this.repository.Exists(r => r.DepartmentId == data.DepartmentId);
                if (dup)
                    return ErrorCode.DuplicateId;

                this.repository.Add(data);
            }
            catch(Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="data">部门对象</param>
        /// <returns></returns>
        public ErrorCode Update(Department data)
        {
            try
            {
                this.repository.Update(data);
            }
            catch(Exception)
            {
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
