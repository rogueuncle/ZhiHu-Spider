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

    struct Question_Struct
    {
        public Question_Struct(string question_id, int offset, string include, int limit = 5, string sort_by = "default", string platform = "desktop", string url = "")
        {
            this.question_id = question_id;
            this.include = include;
            //data[*].is_normal,admin_closed_comment,reward_info,is_collapsed,annotation_action,annotation_detail,collapse_reason,is_sticky,collapsed_by,suggest_edit,comment_count,can_comment,content,editable_content,voteup_count,reshipment_settings,comment_permission,created_time,updated_time,review_info,relevant_info,question,excerpt,relationship.is_authorized,is_author,voting,is_thanked,is_nothelp,is_labeled,is_recognized,paid_info,paid_info_content;data[*].mark_infos[*].url;data[*].author.follower_count,badge[*].topics
            this.limit = limit;
            this.offset = offset;
            this.platform = platform;
            this.sort_by = sort_by;
            this.url = url != "" ? url : $"https://www.zhihu.com/api/v4/questions/{question_id}/answers?include={include}&limit={limit}&offset={offset}&platform={platform}&sort_by={sort_by}";
        }

        public string question_id;
        public string include;
        public int limit;
        public int offset;
        public string platform;
        public string sort_by;
        public string url;
    }
}
