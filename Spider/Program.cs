using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Concurrent;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

namespace Spider
{
    class Program
    {

        private static StreamWriter file = new StreamWriter("./log.txt", true);

        /// <summary>
        /// 联想队列
        /// </summary>
        public static BlockingCollection<HaveQuestions> HaveQuestions = new BlockingCollection<HaveQuestions>();
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 5; i++)
            {
                tasks.Add( Task.Run(async () => { await Question.Run(); }));
            }

            var h = Task.Run(async () => { await HRange(); });
            var s = Task.Run(async () => { await HSeed(); });

            foreach (var task in tasks) task.Wait();
            h.Wait();
            s.Wait();

            Program.log("线程运行完毕");

            Console.ReadKey();
        }

        /// <summary>
        /// 联想线程
        /// </summary>
        /// <returns></returns>
        public static async Task HRange()
        {
            Program.log("联想线程启动!");
            SqlConnection conn = Sql.Get_Connection();
            while (true)
            {
                var data = HaveQuestions.Take();
                Program.log($"获取到一个联想对象,{HaveQuestions.Count}\t{Question.question_queue.Count}");

                if (await Sql.InQuestions(conn, data.Question_id)) continue;

                Question.question_queue.Add(new Question_Struct(data.Question_id, 0));

                for (int i = 0; i < 3; i++)
                {
                    HttpResponseMessage rsp = HttpCli.Get(data);
                    if (rsp is null)
                    {
                        HaveQuestions.Add(data);
                        continue;
                    }
                    if (rsp.StatusCode == System.Net.HttpStatusCode.NotFound) continue;
                    if (rsp.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        HaveQuestions.Add(data);
                        continue;
                    }
                    if (rsp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string rspdata = await rsp.Content.ReadAsStringAsync();
                        JsonDocument json_obj = JsonDocument.Parse(rspdata);
                        JsonElement data_arr = json_obj.RootElement.GetProperty("data");
                        for (int ii = 0; ii < data_arr.GetArrayLength(); ii++)
                        {
                            string _id = data_arr[ii].GetProperty("id").GetInt32().ToString();
                            int _answer_count = data_arr[ii].GetProperty("answer_count").GetInt32();
                            if (_answer_count > 0)
                            {
                                HaveQuestions.Add(new HaveQuestions(_id));  //添加问题id至联想队列
                                Question.question_queue.Add(new Question_Struct(_id, 0));  //添加问题id至获取答案队列
                                Program.log($"添加至队列,id:{_id},count:{_answer_count}");
                            }
                        }

                        while (true)
                        {
                            if (Question.question_queue.Count == 0)  //等待问题队列消耗完毕
                            {
                                break;
                            }
                            else
                            {
                                await Task.Delay(3 * 1000);
                            }
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// 种子线程
        /// </summary>
        /// <returns></returns>
        public static async Task HSeed()
        {
            Program.log("种子线程启动!");
            //string url = "https://www.zhihu.com/explore";
            //string find_s_s = "<script id=\"js-initialData\" type=\"text/json\">";
            //string find_e_s = "</script>";
            Random random = new Random((int)DateTime.Now.Ticks);
            
            while (true)
            {
                //var rsp = await HttpCli.Get(url);
                //if (rsp.StatusCode != System.Net.HttpStatusCode.OK)
                //{
                //    continue;
                //}
                //else
                //{
                //    string rspdata = await rsp.Content.ReadAsStringAsync();

                //    int find_s = rspdata.IndexOf(find_s_s);
                //    if (find_s == -1)
                //    {
                //        Console.WriteLine("json 未找到");
                //        continue;
                //    }

                //    int find_e = rspdata.IndexOf(find_e_s, find_s);
                //    if (find_e == -1)
                //    {
                //        Console.WriteLine("json 未找到");
                //        continue;
                //    }

                //    rspdata = rspdata.Substring(find_s + find_s_s.Length, find_e - find_s - find_s_s.Length);


                //    JsonDocument json_obj = JsonDocument.Parse(rspdata);

                //    JsonElement initialState = json_obj.RootElement.GetProperty("initialState");
                //    JsonElement entities = initialState.GetProperty("explore").GetProperty("roundtables").GetProperty("entities");
                //    foreach(var item in entities.EnumerateObject())
                //    {
                //        Console.WriteLine(item.Name);
                //        var questions = item.Value.GetProperty("questions");
                //        for (int i = 0; i < questions.GetArrayLength(); i++)
                //        {
                //            int id = questions[i].GetProperty("id").GetInt32();
                //            HaveQuestions data = new HaveQuestions(id.ToString());
                //            HaveQuestions.Add(data);
                //        }


                //        HaveQuestions.Add(new HaveQuestions(random.Next(19620867, 452241719).ToString()));
                //        HaveQuestions.Add(new HaveQuestions(random.Next(19620867, 452241719).ToString()));
                //        HaveQuestions.Add(new HaveQuestions(random.Next(19620867, 452241719).ToString()));

                //    }
                //}

                for (int i = 0; i < 5; i++)
                {
                    HaveQuestions.Add(new HaveQuestions(random.Next(10086, 452241719).ToString()));
                }

                await Task.Delay(10 * 1000);

                while(true)
                {
                    if (HaveQuestions.Count == 0)
                    {
                        Program.log("队列为空，重新采取种子");
                        break;
                    }
                    else
                    {
                        await Task.Delay(3 * 1000);
                    }
                }
            }
        }

        public static void log(string text)
        {

            string str = $"{Task.CurrentId}  {DateTime.Now}\t{text}";
            Console.WriteLine(str);
            file.WriteLine(str);
            file.Flush();
        }

    }

    
}
