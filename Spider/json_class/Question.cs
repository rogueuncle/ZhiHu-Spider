using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Spider.json_class
{
    public class Answer
    {
        /// <summary>
        /// 回答id
        /// </summary>
        public int Id { get; set; }
        public string Author_Id { get; set; }
        public int Question_Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Answer_Type { get; set; }
        /// <summary>
        /// api网址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Collapsed { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int Created_Time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public int Updated_Time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Extras { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Copyable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Normal { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int Voteup_Count { get; set; }
        /// <summary>
        /// 评论数据
        /// </summary>
        public int Comment_Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Sticky { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Admin_Closed_Comment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comment_Permission { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Reshipment_Settings { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Editable_Content { get; set; }
        /// <summary>
        /// 内容摘录
        /// </summary>
        public string Excerpt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Collapsed_By { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Collapse_Reason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Annotation_Action { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Labeled { get; set; }
    }


    public class Author
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string Url_Token { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar_Url { get; set; }
        /// <summary>
        /// 头像模板
        /// </summary>
        public string Avatar_Url_Template { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Org { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string User_Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Headline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Advertiser { get; set; }
        /// <summary>
        /// 粉丝数量
        /// </summary>
        public int Follower_Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Is_Privacy { get; set; }
    }


    public class Question
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 问题标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 问题分类
        /// </summary>
        public string Question_Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int Created { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public int Updated_Time { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }

        public string Relationship { get; set; }
        
    }

    public class Paging
    {
        public bool Is_end;
        public bool Is_start;
        public string Next;
        public string Previous;
        public int Totals;

    }
}
