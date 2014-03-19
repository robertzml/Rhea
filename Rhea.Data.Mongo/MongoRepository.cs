using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Rhea.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rhea.Data.Mongo
{
    /// <summary>
    /// MongoDB Repository
    /// </summary>
    /// <typeparam name="T">Repository类型</typeparam>
    /// <typeparam name="TKey">实体ID类型</typeparam>
    public class MongoRepository<T, TKey> : IRepository<T, TKey>
        where T : IEntity<TKey>
    {
        #region Field
        /// <summary>
        /// MongoCollection对象
        /// </summary>
        protected internal MongoCollection<T> collection;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 初始化 MongoDB Repository
        /// 采用配置文件内连接字符串
        /// </summary>
        /// <remarks>默认连接字符串键值为mongoConnection</remarks>
        public MongoRepository(string databaseName)
            : this(Util<TKey>.GetConfigConnectionString(), databaseName)
        {
        }

        /// <summary>
        /// 初始化 MongoDB Repository
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        public MongoRepository(string connectionString, string databaseName)
        {
            this.collection = Util<TKey>.GetCollection<T>(connectionString, databaseName);
        }

        /// <summary>
        /// 初始化 MongoDB Repository
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="collectionName">Collection名称</param>
        public MongoRepository(string connectionString, string databaseName, string collectionName)
        {
            this.collection = Util<TKey>.GetCollection<T>(connectionString, databaseName, collectionName);
        }

        /// <summary>
        /// 初始化 MongoDB Repository
        /// </summary>
        /// <param name="url">Mongo连接Url</param>
        public MongoRepository(MongoUrl url)
        {
            this.collection = Util<TKey>.GetCollection<T>(url);
        }

        /// <summary>
        /// 初始化 MongoDB Repository
        /// </summary>
        /// <param name="url">Mongo连接Url</param>
        /// <param name="collectionName">Collection名称</param>
        public MongoRepository(MongoUrl url, string collectionName)
        {
            this.collection = Util<TKey>.GetCollection<T>(url, collectionName);
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="_id">对象ID</param>
        /// <returns>实体T</returns>
        public virtual T GetById(TKey _id)
        {
            if (typeof(T).IsSubclassOf(typeof(MongoEntity)))
            {
                return this.collection.FindOneByIdAs<T>(new ObjectId(_id as string));
            }

            return this.collection.FindOneByIdAs<T>(BsonValue.Create(_id));
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>查询结果</returns>
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return this.collection.AsQueryable<T>().Where(predicate);
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>新增实体包括其ID</returns>
        public virtual T Add(T entity)
        {
            this.collection.Insert<T>(entity);

            return entity;
        }

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public virtual void Add(IEnumerable<T> entities)
        {
            this.collection.InsertBatch<T>(entities);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>更新后实体</returns>
        public virtual T Update(T entity)
        {
            this.collection.Save<T>(entity);

            return entity;
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.collection.Save<T>(entity);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="_id">实体ID</param>
        public virtual void Delete(TKey _id)
        {
            if (typeof(T).IsSubclassOf(typeof(MongoEntity)))
            {
                this.collection.Remove(Query.EQ("_id", new ObjectId(_id as string)));
            }
            else
            {
                this.collection.Remove(Query.EQ("_id", BsonValue.Create(_id)));
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual void Delete(ObjectId _id)
        {
            this.collection.Remove(Query.EQ("_id", _id));
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual void Delete(T entity)
        {
            this.Delete(entity._id);
        }

        /// <summary>
        /// 按条件删除实体
        /// </summary>
        /// <param name="predicate">条件</param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (T entity in this.collection.AsQueryable<T>().Where(predicate))
            {
                this.Delete(entity._id);
            }
        }

        /// <summary>
        /// 删除集合所有实体
        /// </summary>
        public virtual void DeleteAll()
        {
            this.collection.RemoveAll();
        }

        /// <summary>
        /// 集合计数
        /// </summary>
        /// <returns>实体数量</returns>
        public virtual long Count()
        {
            return this.collection.Count();
        }

        /// <summary>
        /// 检查实体对象是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>存在返回true, 否则返回false</returns>
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return this.collection.AsQueryable<T>().Any(predicate);
        }

        /// <summary>
        /// Lets the server know that this thread is about to begin a series of related operations that must all occur
        /// on the same connection. The return value of this method implements IDisposable and can be placed in a using
        /// statement (in which case RequestDone will be called automatically when leaving the using statement). 
        /// </summary>
        /// <returns>A helper object that implements IDisposable and calls RequestDone() from the Dispose method.</returns>
        /// <remarks>
        ///     <para>
        ///         Sometimes a series of operations needs to be performed on the same connection in order to guarantee correct
        ///         results. This is rarely the case, and most of the time there is no need to call RequestStart/RequestDone.
        ///         An example of when this might be necessary is when a series of Inserts are called in rapid succession with
        ///         SafeMode off, and you want to query that data in a consistent manner immediately thereafter (with SafeMode
        ///         off the writes can queue up at the server and might not be immediately visible to other connections). Using
        ///         RequestStart you can force a query to be on the same connection as the writes, so the query won't execute
        ///         until the server has caught up with the writes.
        ///     </para>
        ///     <para>
        ///         A thread can temporarily reserve a connection from the connection pool by using RequestStart and
        ///         RequestDone. You are free to use any other databases as well during the request. RequestStart increments a
        ///         counter (for this thread) and RequestDone decrements the counter. The connection that was reserved is not
        ///         actually returned to the connection pool until the count reaches zero again. This means that calls to
        ///         RequestStart/RequestDone can be nested and the right thing will happen.
        ///     </para>
        ///     <para>
        ///         Use the connectionstring to specify the readpreference; add "readPreference=X" where X is one of the following
        ///         values: primary, primaryPreferred, secondary, secondaryPreferred, nearest.
        ///         See http://docs.mongodb.org/manual/applications/replication/#read-preference
        ///     </para>
        /// </remarks>
        public virtual IDisposable RequestStart()
        {
            return this.collection.Database.RequestStart();
        }

        /// <summary>
        /// Lets the server know that this thread is done with a series of related operations.
        /// </summary>
        /// <remarks>
        /// Instead of calling this method it is better to put the return value of RequestStart in a using statement.
        /// </remarks>
        public virtual void RequestDone()
        {
            this.collection.Database.RequestDone();
        }
        #endregion //Method

        #region IQueryable<T>
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator&lt;T&gt; object that can be used to iterate through the collection.</returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of IQueryable is executed.
        /// </summary>
        public virtual Type ElementType
        {
            get { return this.collection.AsQueryable<T>().ElementType; }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of IQueryable.
        /// </summary>
        public virtual Expression Expression
        {
            get { return this.collection.AsQueryable<T>().Expression; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        public virtual IQueryProvider Provider
        {
            get { return this.collection.AsQueryable<T>().Provider; }
        }
        #endregion

        #region Property
        /// <summary>
        /// Collection对象
        /// </summary>
        public MongoCollection<T> Collection
        {
            get
            {
                return this.collection;
            }
        }
        #endregion //Property
    }

    /// <summary>
    /// MongoDB Repository 类
    /// </summary>
    /// <typeparam name="T">实体对象类型</typeparam>
    /// <remarks>实体主键为string类型</remarks>
    public class MongoRepository<T> : MongoRepository<T, string>, IMongoRepository<T>
        where T : IEntity<string>
    {
        /// <summary>
        /// 初始化 MongoRepository 类
        /// 采用配置文件内连接字符串
        /// </summary>
        /// <remarks>默认连接字符串键值为mongoConnection</remarks>
        public MongoRepository(string databaseName)
            : base(databaseName) { }

        /// <summary>
        /// 初始化 MongoRepository 类
        /// </summary>
        /// <param name="url">MongoDB连接字符串</param>
        public MongoRepository(MongoUrl url)
            : base(url) { }

        /// <summary>
        /// 初始化 MongoRepository 类
        /// </summary>
        /// <param name="url">MongoDB连接字符串</param>
        /// <param name="collectionName">Collection名称</param>
        public MongoRepository(MongoUrl url, string collectionName)
            : base(url, collectionName) { }

        /// <summary>
        /// 初始化 MongoRepository 类
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        public MongoRepository(string connectionString, string databaseName)
            : base(connectionString, databaseName) { }

        /// <summary>
        /// 初始化 MongoRepository 类
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="collectionName">Collection名称</param>
        public MongoRepository(string connectionString, string databaseName, string collectionName)
            : base(connectionString, databaseName, collectionName) { }
    }
}
