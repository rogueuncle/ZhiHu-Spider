using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider
{
    public static class Sql
    {
        public const string connection_str = "Server=(local);Database=ZhiHu;User Id=sa;Password=sa123456789;";   //数据库连接字符串

        /// <summary>
        /// 生成新的数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection Get_Connection()
        {
            SqlConnection conn = new SqlConnection(connection_str);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 保存题目信息
        /// </summary>
        /// <param name="question_data">题目信息对象</param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static async Task<bool> Save_Question(Js_Question question_data, SqlConnection conn)
        {
            SqlCommand cur = conn.CreateCommand();

            string _sql_s = "";
            string _sql_e = "";
            foreach (var item in question_data.GetType().GetProperties())
            {
                _sql_s += $"{item.Name},";
                _sql_e += $"'{item.GetValue(question_data)}',";
            }

            cur.CommandText = $"insert into Question({_sql_s.Substring(0, _sql_s.Length - 1)}) values({_sql_e.Substring(0, _sql_e.Length - 1)})";

            Program.log($"插入问题,id:{question_data.Id}");

            try
            {
                int state = await cur.ExecuteNonQueryAsync();
                return state > 0;
            }
            catch (Exception e)
            {
                Program.log($"插入失败,id:{question_data.Id}\t{e.Message}");
                //MessageBox.Show(question_data.Id.ToString());
                throw;
            }
            finally
            {
                cur.Dispose();
            }
        }

        public static async Task<bool> Save_Answer(Js_Answer answer_data, SqlConnection conn)
        {
            Program.log($"保存回答信息\t{answer_data.Question_Id}\t{answer_data.Id}");
            SqlCommand cur = conn.CreateCommand();

            string _sql_s = "";
            string _sql_e = "";
            foreach (var item in answer_data.GetType().GetProperties())
            {
                _sql_s += $"{item.Name},";
                _sql_e += $"'{item.GetValue(answer_data)}',";
            }

            cur.CommandText = $"insert into Answer({_sql_s.Substring(0, _sql_s.Length - 1)}) values({_sql_e.Substring(0, _sql_e.Length - 1)})";

            int state = await cur.ExecuteNonQueryAsync();
            return state > 0;

        }

        public static async Task<bool> Update_Author(Js_Author author_data, SqlConnection conn)
        {
            Program.log($"更新作者信息 id:{author_data.Id}");
            SqlCommand cur = conn.CreateCommand();

            string _sql = "select Name,Follower_Count from Author where Id = @id";
            cur.CommandText = _sql;
            cur.Parameters.AddWithValue("@Id", author_data.Id).SqlDbType = System.Data.SqlDbType.NVarChar;

            SqlDataReader data = await cur.ExecuteReaderAsync();
            bool _state = data.Read();
            if (_state)
            {
                //作者存在，更新作者信息
                string _Name = data.GetString(0);
                int _Follower_Count = data.GetInt32(1);
                data.Close();

                if (_Name != author_data.Name || _Follower_Count != author_data.Follower_Count)
                {
                    cur.CommandText = "update Author set Name=@Name,Follower_Count=@Follower_Count where Id = @Id";
                    cur.Parameters.AddWithValue("@Name", author_data.Name);
                    cur.Parameters.AddWithValue("@Follower_Count", author_data.Follower_Count);
                    await cur.ExecuteNonQueryAsync();
                }
                
            }
            else
            {
                data.Close();
                //当前作者不存在
                _sql = "insert into Author(Id,Url_Token,Name,Avatar_Url,Avatar_Url_Template,Is_Org," +
                    "Type,Url,User_Type,Headline,Gender,Is_Advertiser,Follower_Count,Is_Followed,Is_Privacy) " +
                    "values(@Id,@Url_Token,@Name,@Avatar_Url,@Avatar_Url_Template,@Is_Org," +
                    "@Type,@Url,@User_Type,@Headline,@Gender,@Is_Advertiser,@Follower_Count,@Is_Followed,@Is_Privacy)";
                cur.CommandText = _sql;
                cur.Parameters.Clear();

                SqlParameter Id_SqlParameter = new SqlParameter("@Id", System.Data.SqlDbType.NVarChar);
                Id_SqlParameter.Value = author_data.Id;
                cur.Parameters.Add(Id_SqlParameter);

                cur.Parameters.AddWithValue("@Url_Token", author_data.Url_Token);
                cur.Parameters.AddWithValue("@Name", author_data.Name);
                cur.Parameters.AddWithValue("@Avatar_Url", author_data.Avatar_Url);
                cur.Parameters.AddWithValue("@Avatar_Url_Template", author_data.Avatar_Url_Template);

                SqlParameter Is_Org_SqlParameter = new SqlParameter("@Is_Org", System.Data.SqlDbType.Bit);
                Is_Org_SqlParameter.Value = author_data.Is_Org;
                cur.Parameters.Add(Is_Org_SqlParameter);

                cur.Parameters.AddWithValue("@Type", author_data.Type);
                cur.Parameters.AddWithValue("@Url", author_data.Url);
                cur.Parameters.AddWithValue("@User_Type", author_data.User_Type);
                cur.Parameters.AddWithValue("@Headline", author_data.Headline);
                cur.Parameters.AddWithValue("@Gender", author_data.Gender);
                cur.Parameters.AddWithValue("@Is_Advertiser", author_data.Is_Advertiser).SqlDbType = System.Data.SqlDbType.Bit;
                cur.Parameters.AddWithValue("@Is_Followed", author_data.Is_Followed).SqlDbType = System.Data.SqlDbType.Bit;
                cur.Parameters.AddWithValue("@Is_Privacy", author_data.Is_Privacy).SqlDbType = System.Data.SqlDbType.Bit;

                cur.Parameters.AddWithValue("@Follower_Count", author_data.Follower_Count).SqlDbType = System.Data.SqlDbType.Int;

                await cur.ExecuteNonQueryAsync();
            }

            data.Close();
            cur.Dispose();
            return true;

        }

        /// <summary>
        /// 检测指定问题id是否存在于问题数据库内
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <param name="Question_id">问题id</param>
        /// <returns>存在返回true</returns>
        public static async Task<bool> InQuestions(SqlConnection conn, string Question_id)
        {

            SqlCommand cur = conn.CreateCommand();
            cur.CommandText = "select count(id) from Question where id = @id";
            cur.Parameters.AddWithValue("@id", Question_id);
            int _question_count = (int)await cur.ExecuteScalarAsync();
            cur.Dispose();
            if (_question_count == 1)
            {
                Program.log($"{Question_id} 已存在!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
