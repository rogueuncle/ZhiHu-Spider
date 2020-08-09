using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spider
{
    public static class Sql
    {
        private static List<SqlConnection> sql_pool = new List<SqlConnection>();
        private static object Lock;

        public const string connection_str = "";   //数据库连接字符串
        private const int pool_num = 10;   //连接池的上限
        
        /// <summary>
        /// 生成新的数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection _Get_Connection()
        {
            SqlConnection conn = new SqlConnection(connection_str);
            return conn;
        }

        /// <summary>
        /// 从池中获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection Get_SqlConnection()
        {
            lock (Lock)
            {
                if (sql_pool.Count >= 1)
                {
                    SqlConnection conn = sql_pool[0];
                    sql_pool.RemoveAt(0);
                    return conn;
                }
                else
                {
                    return _Get_Connection();
                }
            }
        }

        /// <summary>
        /// 返回数据库连接到连接池中
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool Put_SqlConnection(SqlConnection conn)
        {
            lock (Lock)
            {
                if (sql_pool.Count >= pool_num)
                {
                    conn.Close();
                    conn.Dispose();
                    return true;
                }
                else
                {
                    sql_pool.Add(conn);
                    return true;
                }
            }
        }
    }
}
