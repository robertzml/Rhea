using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Rhea.Data.Server
{
    /// <summary>
    /// SQL Server 数据库
    /// </summary>
    public class SqlServerContext
    {
        #region Field
        /// <summary>
        /// 数据库连接字符串
        /// </summary>        
        private string connectionString; // = @"server=localhost;uid=ucapaneus;pwd=capaneus123456;database=capaneus";

        /// <summary>
        /// 参数
        /// </summary>
        private Dictionary<string, object> parameters;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// 是否打开
        /// </summary>
        private bool isOpen;

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errorMessage = "";
        #endregion //Field

        #region Constructor
        public SqlServerContext()
        {
            Init("");
        }

        public SqlServerContext(string connectionString)
        {
            Init(connectionString);
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        private void Init(string connectionString)
        {
            if (connectionString != "")
                this.connectionString = connectionString;
            else
                LoadConnectionString();

            this.connection = new SqlConnection(this.connectionString);
            this.parameters = new Dictionary<string, object>();
            this.isOpen = false;
        }

        /// <summary>
        /// 载入配置文件连接字符串
        /// </summary>
        private void LoadConnectionString()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            if (!this.isOpen)
            {
                try
                {
                    connection.Open();
                    this.isOpen = true;
                }
                catch (SqlException e)
                {
                    this.errorMessage = e.Message;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            this.connection.Close();
            this.isOpen = false;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        public void AddParameter(string key, object value)
        {
            this.parameters.Add(key, value);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        public void ExecuteNonQuery(string queryStr)
        {
            this.Open();
            try
            {
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand(queryStr, connection))
                    {
                        command.Transaction = transaction;
                        foreach (KeyValuePair<string, object> kvp in this.parameters)
                        {
                            command.Parameters.Add(new SqlParameter("?" + kvp.Key, kvp.Value));
                        }

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            catch (SqlException e)
            {
                this.errorMessage = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                this.Close();
                this.parameters.Clear();
            }
        }

        /// <summary>
        /// 执行多条SQL语句
        /// </summary>
        /// <param name="queryStrs">SQL语句数组</param>
        public void ExecuteBatchNonQuery(string[] queryStrs)
        {
            this.Open();
            try
            {
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    foreach (string sql in queryStrs)
                    {
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (SqlException e)
            {
                this.errorMessage = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                this.Close();
                this.parameters.Clear();
            }
        }

        /// <summary>
        /// 执行插入语句并得到自动编号
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>自动编号</returns>
        public int ExecuteInsert(string sql)
        {
            int result = 0;

            this.Open();
            try
            {
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Transaction = transaction;
                        foreach (KeyValuePair<string, object> kvp in this.parameters)
                        {
                            command.Parameters.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
                        }

                        command.ExecuteNonQuery();

                        command.CommandText = @"select @@identity";
                        result = Convert.ToInt32(command.ExecuteScalar());
                    }
                    transaction.Commit();
                }
            }
            catch (SqlException e)
            {
                this.errorMessage = e.Message;
                return 0;
            }
            finally
            {
                this.Close();
                this.parameters.Clear();
            }

            return result;
        }

        /// <summary>
        /// 执行SQL语句并返回所有结果
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        /// <returns>返回DataTable</returns>
        public DataTable ExecuteQuery(string queryStr)
        {
            DataTable dt = new DataTable();
            this.Open();
            try
            {
                using (SqlCommand command = new SqlCommand(queryStr, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    foreach (KeyValuePair<string, object> kvp in this.parameters)
                    {
                        command.Parameters.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
                    }

                    adapter.Fill(dt);
                }
            }
            catch (SqlException e)
            {
                this.errorMessage = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                this.Close();
                this.parameters.Clear();
            }
            return dt;
        }

        /// <summary>
        /// 执行SQL语句并返回第一行
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        /// <returns>返回DataRow</returns>
        public DataRow ExecuteRow(string queryStr)
        {
            DataRow row;
            this.Open();
            try
            {
                using (SqlCommand command = new SqlCommand(queryStr, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    foreach (KeyValuePair<string, object> kvp in this.parameters)
                    {
                        command.Parameters.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
                    }

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count == 0)
                        row = null;
                    else
                        row = dt.Rows[0];
                }
            }
            catch (SqlException e)
            {
                this.errorMessage = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                this.Close();
                this.parameters.Clear();
            }
            return row;
        }

        /// <summary>
        /// 执行SQL语句并返回结果第一行的第一列
        /// </summary>
        /// <param name="queryStr">SQL语句</param>
        /// <returns>返回值</returns>
        public Object ExecuteScalar(string queryStr)
        {
            Object obj;
            this.Open();
            try
            {
                using (SqlCommand command = new SqlCommand(queryStr, connection))
                {
                    foreach (KeyValuePair<string, object> kvp in this.parameters)
                    {
                        command.Parameters.Add(new SqlParameter("@" + kvp.Key, kvp.Value));
                    }

                    obj = command.ExecuteScalar();
                }
            }
            catch (SqlException e)
            {
                this.errorMessage = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                this.Close();
                this.parameters.Clear();
            }
            return obj;
        }
        #endregion //Method

        #region Property
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
        }
        #endregion //Property
    }
}
