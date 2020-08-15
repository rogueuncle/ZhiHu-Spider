using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Data.SqlClient;
using System.Collections.Concurrent;

namespace Spider
{
    class Question
    {
        /// <summary>
        /// 问题队列
        /// </summary>
        public static BlockingCollection<Question_Struct> question_queue = new BlockingCollection<Question_Struct>();

        public static async Task Run()
        {
            Program.log("处理问题线程启动");

            using (SqlConnection conn = Sql.Get_Connection())
            {
                while (true)
                {
                    Question_Struct question_data = question_queue.Take();
                    Program.log($"获取到一个问题对象,{question_data.Question_id}\t{Question.question_queue.Count}");

                    #region 判断当前问题是否已经爬取
                    var _state = await Sql.InQuestions(conn, question_data.Question_id);
                    if (_state)
                    {
                        continue;
                    }
                    #endregion

                    await DownloadPage(question_data, conn);
                }
            }
            
        }
        private static async Task<bool> DownloadPage(Question_Struct question_data, SqlConnection conn, int retry = 3)
        {
            Program.log($"下载页面 {question_data.Question_id}\t{question_data.Offset},{question_data.Limit}");
            
            #region 下载页面
            HttpResponseMessage rsp = await _Http_Get(question_data,retry);
            if (rsp is null)  return false;

            if (rsp.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Program.log($"{question_data.Question_id}\t{question_data.Offset}\t{question_data.Limit}\t{rsp.StatusCode}  下载失败,重试...");
                return await DownloadPage(question_data,conn, retry - 1);
            }
            string rspdata = await rsp.Content.ReadAsStringAsync();
            #endregion

            #region 解析js
            //var x = JsonSerializer.Deserialize<json_class.Root>(ddd);
            JsonDocument json_obj = JsonDocument.Parse(rspdata);   //解析js
            #endregion

            #region 解析data
            JsonElement json_data = json_obj.RootElement.GetProperty("data");  //获取data

            if (json_data.GetArrayLength() != 0)
            {
                #region 读取题目信息并保存
                Js_Question question = Js2Question(json_data[0]);    //获取问题的详细信息

                if (question_data.IsNew)
                {
                    try
                    {
                        await Sql.Save_Question(question, conn);
                    }
                    catch (Exception e)
                    {
                        Program.log($"保存问题失败\t{e.Message}");
                        return false;
                    }
                    
                }
                
                
                #endregion

                #region 读取详细信息
                for (int i = 0; i < json_data.GetArrayLength(); i++)
                {
                    //处理作者信息
                    Js_Author author = Js2Author(json_data[i]);
                    await Sql.Update_Author(author, conn);  //更新或插入作者信息

                    //处理回答信息
                    Js_Answer answer = Js2Answer(json_data[i],author.Id,question.Id);
                    await Sql.Save_Answer(answer, conn);  //保存回答信息
                }
                #endregion
            }
            #endregion

            #region 解析游标信息
            json_data = json_obj.RootElement.GetProperty("paging");  //获取data
            Js_Paging paging = Js2Paging(json_data);
            if (paging.Is_end != true && (question_data.Offset + question_data.Limit) < 100)
            {
                
                Question_Struct new_question_struct = new Question_Struct(question_data.Question_id, question_data.Offset + question_data.Limit,isnew:false);

                new_question_struct.Offset = question_data.Offset + question_data.Limit;
                return await DownloadPage(new_question_struct, conn);
            }
            #endregion

            return true;
        }

        
        private static async Task<HttpResponseMessage> _Http_Get(Question_Struct inputdata,int num = 3)
        {
            if (num < 0)
            {
                return null;
            }
            #region 发送http请求并读取返回值
            HttpResponseMessage rsp;
            try
            {
                rsp = HttpCli.Get(inputdata);
            }
            catch (Exception e)
            {
                Program.log($"Http请求发生错误,问题id:{inputdata.Question_id}\t{e.Message}");
                return await _Http_Get(inputdata, num - 1);
            }

            if (rsp is null) return null;

            if (rsp.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Program.log($"问题不存在,{inputdata.Question_id}");
                return null;
            }
            if (rsp.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                Program.log($"403访问被拒绝\t执行次数{HttpCli.Count}\t问题id:{inputdata.Question_id}");
                //question_queue.Add(inputdata);  //把当前数据再次添加到队列中，等待重试,在获取第二页的时候会导致错误
                return null;
            }

            return rsp;
            #endregion
        }

        /// <summary>
        /// 提取js对象内的关于问题的内容
        /// </summary>
        /// <param name="json_obj">json对象</param>
        /// <returns>返回Question对象</returns>
        private static Js_Question Js2Question(JsonElement json_obj)
        {
            JsonElement question_obj = json_obj.GetProperty("question");
            Js_Question question = new Js_Question();
            
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
        private static Js_Author Js2Author(JsonElement json_obj)
        {
            JsonElement Author_obj = json_obj.GetProperty("author");
            Js_Author author = new Js_Author();

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
    
        private static Js_Answer Js2Answer(JsonElement json_obj,string Author_Id,int Question_Id)
        {
            Js_Answer answer = new Js_Answer()
            {
                Id = json_obj.GetProperty("id").GetInt32(),
                Author_Id = Author_Id,
                Question_Id = Question_Id,

                Type = json_obj.GetProperty("type").GetString(),
                Answer_Type = json_obj.GetProperty("answer_type").GetString(),
                Url = json_obj.GetProperty("url").GetString(),
                Is_Collapsed = json_obj.GetProperty("is_collapsed").GetBoolean(),
                Created_Time = json_obj.GetProperty("created_time").GetInt32(),
                Updated_Time = json_obj.GetProperty("updated_time").GetInt32(),
                Extras = json_obj.GetProperty("extras").GetString(),
                Is_Copyable = json_obj.GetProperty("is_copyable").GetBoolean(),
                Is_Normal = json_obj.GetProperty("is_normal").GetBoolean(),
                Voteup_Count = json_obj.GetProperty("voteup_count").GetInt32(),
                Comment_Count = json_obj.GetProperty("comment_count").GetInt32(),
                Is_Sticky = json_obj.GetProperty("is_sticky").GetBoolean(),
                Admin_Closed_Comment = json_obj.GetProperty("admin_closed_comment").GetBoolean(),
                Comment_Permission = json_obj.GetProperty("comment_permission").GetString(),
                Reshipment_Settings = json_obj.GetProperty("reshipment_settings").GetString(),
                Content = json_obj.GetProperty("content").GetString(),
                Editable_Content = json_obj.GetProperty("editable_content").GetString(),
                Excerpt = json_obj.GetProperty("excerpt").GetString(),
                Collapsed_By = json_obj.GetProperty("collapsed_by").GetString(),
                Collapse_Reason = json_obj.GetProperty("collapse_reason").GetString(),
                Annotation_Action = json_obj.GetProperty("annotation_action").GetString(),
                Is_Labeled = json_obj.GetProperty("is_labeled").GetBoolean(),
            };


            return answer;
        }

        private static Js_Paging Js2Paging(JsonElement json_obj)
        {
            Js_Paging paging = new Js_Paging()
            {
                Is_end = json_obj.GetProperty("is_end").GetBoolean(),
                Is_start = json_obj.GetProperty("is_start").GetBoolean(),
                Next = json_obj.GetProperty("next").GetString(),
                Previous = json_obj.GetProperty("previous").GetString(),
                Totals = json_obj.GetProperty("totals").GetInt32(),
            };
            return paging;
            
        }
    }

    public struct Question_Struct: IHttp_Get_Interface
    {
        
        public Question_Struct(string question_id, int offset, int limit = 5, string include = "", string sort_by = "default", string platform = "desktop", string url = "",bool isnew=true)
        {
            this.Question_id = question_id;
            this.Include = include != "" ? include : "data%5B%2A%5D.is_normal%2Cadmin_closed_comment%2Creward_info%2Cis_collapsed%2Cannotation_action%2Cannotation_detail%2Ccollapse_reason%2Cis_sticky%2Ccollapsed_by%2Csuggest_edit%2Ccomment_count%2Ccan_comment%2Ccontent%2Ceditable_content%2Cvoteup_count%2Creshipment_settings%2Ccomment_permission%2Ccreated_time%2Cupdated_time%2Creview_info%2Crelevant_info%2Cquestion%2Cexcerpt%2Crelationship.is_authorized%2Cis_author%2Cvoting%2Cis_thanked%2Cis_nothelp%2Cis_labeled%2Cis_recognized%2Cpaid_info%2Cpaid_info_content%3Bdata%5B%2A%5D.mark_infos%5B%2A%5D.url%3Bdata%5B%2A%5D.author.follower_count%2Cbadge%5B%2A%5D.topics";
            this.Limit = limit;
            this.Offset = offset;
            this.Platform = platform;
            this.Sort_by = sort_by;
            this.Url = url != "" ? url : $"http://www.zhihu.com/api/v4/questions/{question_id}/answers?include={Include}&limit={limit}&offset={offset}&platform={platform}&sort_by={sort_by}";
            this.IsNew = isnew;
            this.EventType = "获取答案列表";
        }

        public string Question_id { get; set; }
        public string Include { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Platform { get; set; }
        public string Sort_by { get; set; }
        public string Url { get; set; }
        public bool IsNew { get; }
        public string EventType { get; set; }
    }

    public struct HaveQuestions : IHttp_Get_Interface
    {
        public HaveQuestions(string question_id, int limit = 5, string include = "")
        {
            this.Question_id = question_id;
            this.Limit = limit;
            this.Include = include != "" ? include : "data%5B*%5D.answer_count%2Cauthor%2Cfollower_count";
            this.Url = $"http://www.zhihu.com/api/v4/questions/{question_id}/similar-questions?include={this.Include}&limit={limit}";
            this.EventType = "联想问题";
        }
        public string Question_id { get; set; }
        public string Url { get; set; }
        public string Include { get; set; }
        public int Limit { get; set; }
        public string EventType { get; set; }
    }
}
