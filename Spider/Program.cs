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

namespace Spider
{
    class Program
    {

        public static BlockingCollection<HaveQuestions> HaveQuestions = new BlockingCollection<HaveQuestions>();
        static async Task Main(string[] args)
        {
            //Question_Struct question_Struct = new Question_Struct("29843358", 0);

            //Question.question_queue.Enqueue(question_Struct);
            //Question.question_queue.Add(question_Struct);
            
            HaveQuestions data = new HaveQuestions("29843358");
            HaveQuestions.Add(data);

            var t = Task.Run(() => { Question.Run(); });
            var t1 = Task.Run(() => { HRange(); });
        
            t1.Wait();
            t.Wait();

            //var h = new Task(HRanges,TaskCreationOptions.LongRunning);
            //h.Start();

            //var r1 = new Task(QRun,TaskCreationOptions.LongRunning);
            //r1.Start();

            //var r2 = new Task(QRun, TaskCreationOptions.LongRunning);
            //r2.Start();

            //var r3 = new Task(QRun, TaskCreationOptions.LongRunning);
            //r3.Start();

            //h.Wait();
            //r1.Wait();
            //r2.Wait();
            //r3.Wait();
            Console.ReadKey();
        }
        public static async void QRun()
        {
            await Question.Run();
        }

        public static async void HRanges()
        {
            await HRange();
        }

        public static async Task HRange()
        {
            Console.WriteLine("联想线程启动!");
            while (true)
            {
                var data = HaveQuestions.Take();
                Console.WriteLine("获取到一个联想对象");
                for (int i = 0; i < 3; i++)
                {
                    var rsp = await HttpCli.Get(data);
                    if (rsp.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        continue;
                    }
                    else
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
                                Console.WriteLine($"添加至队列,id:{_id},count:{_answer_count}");
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
                                //System.Threading.Thread.Sleep(10 * 1000);
                                await Task.Delay(3 * 1000);
                            }
                        }
                    }
                }
            }
            
        }
    }


}
