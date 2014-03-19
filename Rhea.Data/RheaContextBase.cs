using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model;

namespace Rhea.Data
{
    /// <summary>
    /// Rhea数据库相关基类
    /// </summary>
    /// <typeparam name="T">实体对象类型</typeparam>
    public abstract class RheaContextBase<T>  
        where T : IEntity<string>
    {
        #region Field
        /// <summary>
        /// 数据Repository接口
        /// </summary>
        protected IMongoRepository<T> repository;
        #endregion //Field
    }
}
