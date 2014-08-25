using Rhea.Data;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 校区业务类
    /// </summary>
    public class CampusBusiness
    {
        #region Field
        /// <summary>
        /// 校区Repository
        /// </summary>
        private ICampusRepository campusRepository;

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
        /// 校区业务类
        /// </summary>
        public CampusBusiness()
        {
            this.campusRepository = new MongoCampusRepository();
            this.backupBusiness = new BackupBusiness();
            this.logBusiness = new LogBusiness();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有校区
        /// </summary>
        /// <remarks>状态不为1的对象</remarks>
        /// <returns></returns>
        public IEnumerable<Campus> Get()
        {
            return this.campusRepository.Get().Where(r => r.Status != 1);
        }

        /// <summary>
        /// 获取校区
        /// </summary>
        /// <param name="id">校区ID</param>
        /// <returns></returns>
        public Campus Get(int id)
        {
            var data = this.campusRepository.Get(id);
            if (data.Status == 1)
                return null;
            else
                return data;
        }

        /// <summary>
        /// 添加校区
        /// </summary>
        /// <param name="data">校区对象</param>
        /// <returns></returns>
        public ErrorCode Create(Campus data)
        {
            return this.campusRepository.Create(data);
        }

        /// <summary>
        /// 更新校区
        /// </summary>
        /// <param name="data">校区对象</param>
        /// <returns></returns>
        public ErrorCode Update(Campus data)
        {
            return this.campusRepository.Update(data);
        }

        /// <summary>
        /// 备份校区
        /// </summary>
        /// <param name="_id">校区系统ID</param>
        /// <returns></returns>
        public ErrorCode Backup(string _id)
        {
            ErrorCode result = this.backupBusiness.Backup(RheaServer.EstateDatabase, EstateCollection.Campus, EstateCollection.CampusBackup, _id);
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

            result = this.logBusiness.Log(RheaServer.EstateDatabase, EstateCollection.Campus, _id, log);
            return result;
        }
        #endregion //Method
    }
}
