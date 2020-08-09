using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Spider.json_class;
using System.Data.SqlClient;

namespace Spider
{
    class Question
    {
        /// <summary>
        /// 问题的先进先出
        /// </summary>
        public static Queue<Question_Struct> question_queue = new Queue<Question_Struct>();

        /// <summary>
        /// 爬取数据的主入口
        /// </summary>
        public static async Task Run()
        {
            SqlConnection conn = Sql._Get_Connection();
            while (true)
            {
                Question_Struct inputdata = question_queue.Dequeue();   //获取一个队列参数
                
                #region 验证当前问题是否已经被获取
                SqlCommand cur = conn.CreateCommand();
                cur.CommandText = "select count(id) from Question where id = @id";
                cur.Parameters.AddWithValue("@id", inputdata.Question_id);
                int _question_count = (int)await cur.ExecuteScalarAsync();
                if(_question_count == 1)
                {
                    Sql.Put_SqlConnection(conn);
                    continue;
                }
                #endregion

                HttpResponseMessage rsp = await _Http_Get(inputdata);
                if (rsp.StatusCode != System.Net.HttpStatusCode.OK || rsp is null) 
                {
                    Console.WriteLine();
                    continue;
                } 

                string rspdata = await rsp.Content.ReadAsStringAsync();

                #region 解析js并读取参数值
                //var x = JsonSerializer.Deserialize<json_class.Root>(ddd);
                JsonDocument json_obj = JsonDocument.Parse(rspdata);   //解析js

                JsonElement data = json_obj.RootElement.GetProperty("data");  //获取data

                if (data.GetArrayLength() == 0) continue;

                json_class.Question question = Js2Question(data[0]);    //获取问题的详细信息
                //验证问题信息是否在数据库中



                for (int i = 0; i < data.GetArrayLength(); i++)
                {
                    json_class.Author author = Js2Author(data[i]);
                    //验证作者信息是否在数据库中
                }
                #endregion


            }
        }

        private static async Task<HttpResponseMessage> _Http_Get(Question_Struct inputdata,byte num = 3)
        {
            if (num < 0)
            {
                return null;
            }
            #region 发送http请求并读取返回值
            HttpResponseMessage rsp = await HttpCli.Get(inputdata);
            if (rsp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            string rspdata = await rsp.Content.ReadAsStringAsync();

            return rsp;
            #endregion
        }

        /// <summary>
        /// 提取js对象内的关于问题的内容
        /// </summary>
        /// <param name="json_obj">json对象</param>
        /// <returns>返回Question对象</returns>
        private static json_class.Question Js2Question(JsonElement json_obj)
        {
            JsonElement question_obj = json_obj.GetProperty("question");
            json_class.Question question = new json_class.Question();
            
            question.Id = question_obj.GetProperty("id").GetInt32();
            question.Type = question_obj.GetProperty("type").GetString();
            question.Title = question_obj.GetProperty("title").GetString();
            question.Question_Type = question_obj.GetProperty("question_type").GetString();
            question.Created = question_obj.GetProperty("created").GetInt32();
            question.Updated_Time = question_obj.GetProperty("updated_time").GetInt32();
            question.Url = question_obj.GetProperty("url").GetString();
            question.Relationship = question_obj.GetProperty("url").ToString();

            return question;

        }

        /// <summary>
        /// 提取js对象内的关于作者的内容
        /// </summary>
        /// <param name="json_obj">json对象</param>
        /// <returns>Author</returns>
        private static json_class.Author Js2Author(JsonElement json_obj)
        {
            JsonElement Author_obj = json_obj.GetProperty("author");
            json_class.Author author = new json_class.Author();

            author.Id = Author_obj.GetProperty("id").GetString();
            author.Url_Token = Author_obj.GetProperty("url_token").GetString();
            author.Name = Author_obj.GetProperty("name").GetString();
            author.Avatar_Url = Author_obj.GetProperty("avatar_url").GetString();
            author.Avatar_Url_Template = Author_obj.GetProperty("avatar_url_template").GetString();
            author.Is_Org = Author_obj.GetProperty("is_org").GetBoolean();
            author.Type = Author_obj.GetProperty("type").GetString();
            author.Url = Author_obj.GetProperty("url").GetString();
            author.User_Type = Author_obj.GetProperty("user_type").GetString();
            author.Headline = Author_obj.GetProperty("headline").GetString();
            author.Gender = Author_obj.GetProperty("gender").GetInt32();
            author.Is_Advertiser = Author_obj.GetProperty("is_advertiser").GetBoolean();
            
            try
            {
                author.Follower_Count = Author_obj.GetProperty("follower_count").GetInt32();
            }
            catch (Exception)
            {
                author.Follower_Count = 0;
            }
            
            author.Is_Followed = Author_obj.GetProperty("is_followed").GetBoolean();
            author.Is_Privacy = Author_obj.GetProperty("is_privacy").GetBoolean();

            return author;
        }
    
        private static json_class.Answer Js2Answer(JsonElement json_obj)
        {
            json_class.Answer answer = new json_class.Answer()
            {
                Id = 1,
            };


            return answer;
        }
    }

    struct Question_Struct: IHttp_Get_Interface
    {
        
        public Question_Struct(string question_id, int offset, int limit = 5, string include = "", string sort_by = "default", string platform = "desktop", string url = "")
        {
            this.Question_id = question_id;
            this.Include = include != "" ? include : "data%5B%2A%5D.is_normal%2Cadmin_closed_comment%2Creward_info%2Cis_collapsed%2Cannotation_action%2Cannotation_detail%2Ccollapse_reason%2Cis_sticky%2Ccollapsed_by%2Csuggest_edit%2Ccomment_count%2Ccan_comment%2Ccontent%2Ceditable_content%2Cvoteup_count%2Creshipment_settings%2Ccomment_permission%2Ccreated_time%2Cupdated_time%2Creview_info%2Crelevant_info%2Cquestion%2Cexcerpt%2Crelationship.is_authorized%2Cis_author%2Cvoting%2Cis_thanked%2Cis_nothelp%2Cis_labeled%2Cis_recognized%2Cpaid_info%2Cpaid_info_content%3Bdata%5B%2A%5D.mark_infos%5B%2A%5D.url%3Bdata%5B%2A%5D.author.follower_count%2Cbadge%5B%2A%5D.topics";
            this.Limit = limit;
            this.Offset = offset;
            this.Platform = platform;
            this.Sort_by = sort_by;
            this.Url = url != "" ? url : $"https://www.zhihu.com/api/v4/questions/{question_id}/answers?include={Include}&limit={limit}&offset={offset}&platform={platform}&sort_by={sort_by}";
        }

        public string Question_id { get; set; }
        public string Include { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Platform { get; set; }
        public string Sort_by { get; set; }
        public string Url { get; set; }
    }


}
