using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void Run()
        {
            while (true)
            {
                Question_Struct data = question_queue.Dequeue();   //获取一个队列参数


            }
        }
    }

    struct Question_Struct: IHttp_Get_Interface
    {
        public Question_Struct(string question_id, int offset, int limit = 5, string include = "", string sort_by = "default", string platform = "desktop", string url = "")
        {
            Question_id = question_id;
            this.Include = include != "" ? include : "data[*].is_normal,admin_closed_comment,reward_info,is_collapsed,annotation_action,annotation_detail,collapse_reason,is_sticky,collapsed_by,suggest_edit,comment_count,can_comment,content,editable_content,voteup_count,reshipment_settings,comment_permission,created_time,updated_time,review_info,relevant_info,question,excerpt,relationship.is_authorized,is_author,voting,is_thanked,is_nothelp,is_labeled,is_recognized,paid_info,paid_info_content;data[*].mark_infos[*].url;data[*].author.follower_count,badge[*].topics";
            //
            this.Limit = limit;
            this.Offset = offset;
            this.Platform = platform;
            this.Sort_by = sort_by;
            this.Url = url != "" ? url : $"https://www.zhihu.com/api/v4/questions/{question_id}/answers?include={include}&limit={limit}&offset={offset}&platform={platform}&sort_by={sort_by}";
        }

        //private string question_id;
        //private string include;
        //private int limit;
        //private int offset;
        //private string platform;
        //private string sort_by;
        //private string url;

        public string Question_id { get; set; }
        public string Include { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Platform { get; set; }
        public string Sort_by { get; set; }
        public string Url { get; set; }
    }


}
