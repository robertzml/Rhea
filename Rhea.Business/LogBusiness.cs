using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Common;
using Rhea.Data;
using Rhea.Data.Mongo;
using Rhea.Model;

namespace Rhea.Business
{
    /// <summary>
    /// 日志业务类
    /// </summary>
    public class LogBusiness
    {
        #region Field
        /// <summary>
        /// 日志Repository
        /// </summary>
        private ILogRepository logRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 日志业务类
        /// </summary>
        public LogBusiness()
        {
            this.logRepository = new MongoLogRepository();
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log> Get()
        {
            return this.logRepository.Get();
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="id">日志ID</param>
        /// <returns></returns>
        public Log Get(string id)
        {
            return this.logRepository.Get(id);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="data">日志对象</param>
        /// <returns></returns>
        public ErrorCode Create(Log data)
        {
            return this.logRepository.Create(data);
        }


        public ErrorCode Log(MongoEntity entity, Log data)
        {
            string collectionName;

            Type type = entity.GetType();
            var att = Attribute.GetCustomAttribute(type, typeof(CollectionName));
            if (att != null)
            {
                collectionName = ((CollectionName)att).Name;
            }
            else
            {
                while (!type.BaseType.Equals(typeof(MongoEntity)))
                {
                    type = type.BaseType;
                }

                collectionName = type.Name;
            }



            return ErrorCode.Success;
        }
        #endregion //Method
    }
}
