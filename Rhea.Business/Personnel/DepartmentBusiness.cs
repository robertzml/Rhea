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

        /// <summary>
        /// 备份业务
        /// </summary>
        private BackupBusiness backupBusiness;

        /// <summary>
        /// 日志业务
        /// </summary>
        private LogBusiness logBusiness;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 部门业务类
        /// </summary>
        public DepartmentBusiness()
        {
            this.departmentRepository = new MongoDepartmentRepository();
            this.backupBusiness = new BackupBusiness();
            this.logBusiness = new LogBusiness();
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
            if (data == null || data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="data">部门对象</param>
        /// <returns></returns>
        public ErrorCode Update(Department data)
        {
            return this.departmentRepository.Update(data);
        }

        /// <summary>
        /// 备份校区
        /// </summary>
        /// <param name="_id">校区系统ID</param>
        /// <returns></returns>
        public ErrorCode Backup(string _id)
        {
            ErrorCode result = this.backupBusiness.Backup(RheaServer.PersonnelDatabase, PersonnelCollection.Department, PersonnelCollection.DepartmentBackup, _id);
            return result;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_id">校区系统ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        public ErrorCode Log(string _id, Log log)
        {
            ErrorCode result = this.logBusiness.Create(log);
            if (result != ErrorCode.Success)
                return result;

            result = this.logBusiness.Log(RheaServer.PersonnelDatabase, PersonnelCollection.Department, _id, log);
            return result;
        }
        #endregion //Method
    }
}
