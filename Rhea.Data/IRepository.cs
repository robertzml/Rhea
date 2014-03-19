using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rhea.Data
{
    /// <summary>
    /// Repository接口定义
    /// </summary>
    /// <typeparam name="T">Repository类型</typeparam>
    /// <typeparam name="TKey">实体ID类型</typeparam>
    public interface IRepository<T, TKey> : IQueryable<T>
        where T : IEntity<TKey>
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="_id">对象ID</param>
        /// <returns>实体T</returns>
        T GetById(TKey _id);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>查询结果</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>新增实体包括其ID</returns>
        T Add(T entity);

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void Add(IEnumerable<T> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>更新后实体</returns>
        T Update(T entity);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="_id">实体ID</param>
        void Delete(TKey _id);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);

        /// <summary>
        /// 按条件删除实体
        /// </summary>
        /// <param name="predicate">条件</param>
        void Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 删除集合所有实体
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// 集合计数
        /// </summary>
        /// <returns>实体数量</returns>
        long Count();

        /// <summary>
        /// 检查实体对象是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>存在返回true, 否则返回false</returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Lets the server know that this thread is about to begin a series of related operations that must all occur
        /// on the same connection. The return value of this method implements IDisposable and can be placed in a using
        /// statement (in which case RequestDone will be called automatically when leaving the using statement). 
        /// </summary>
        /// <returns>A helper object that implements IDisposable and calls RequestDone() from the Dispose method.</returns>
        /// <remarks>
        /// Sometimes a series of operations needs to be performed on the same connection in order to guarantee correct
        /// results. This is rarely the case, and most of the time there is no need to call RequestStart/RequestDone.
        /// An example of when this might be necessary is when a series of Inserts are called in rapid succession with
        /// SafeMode off, and you want to query that data in a consistent manner immediately thereafter (with SafeMode
        /// off the writes can queue up at the server and might not be immediately visible to other connections). Using
        /// RequestStart you can force a query to be on the same connection as the writes, so the query won't execute
        /// until the server has caught up with the writes.
        /// A thread can temporarily reserve a connection from the connection pool by using RequestStart and
        /// RequestDone. You are free to use any other databases as well during the request. RequestStart increments a
        /// counter (for this thread) and RequestDone decrements the counter. The connection that was reserved is not
        /// actually returned to the connection pool until the count reaches zero again. This means that calls to
        /// RequestStart/RequestDone can be nested and the right thing will happen.
        /// </remarks>
        IDisposable RequestStart();

        /// <summary>
        /// Lets the server know that this thread is done with a series of related operations.
        /// </summary>
        /// <remarks>
        /// Instead of calling this method it is better to put the return value of RequestStart in a using statement.
        /// </remarks>
        void RequestDone();        
    }

    /// <summary>
    /// MongoDB Repository接口定义
    /// </summary>
    /// <typeparam name="T">Repository类型</typeparam>
    /// <remarks>实体使用字符串为ID</remarks>
    public interface IMongoRepository<T> : IQueryable<T>, IRepository<T, string>
        where T : IEntity<string>
    {
        MongoDB.Driver.MongoCollection<T> Collection { get; }
    }
   
}
