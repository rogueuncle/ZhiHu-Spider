using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.CompilerServices;

namespace Spider
{
    class HttpCli
    {
        private static object Lock = true;
        /// <summary>
        /// 当前访问计数
        /// </summary>
        public static int Count = 0;
        /// <summary>
        /// 最大允许的持续访问次数
        /// </summary>
        public static int MaxCount = 100;
        /// <summary>
        /// 延迟时间,毫秒
        /// </summary>
        public static int SleepTime = 60 * 1000;

        /// <summary>
        /// 更新协议头
        /// </summary>
        /// <param name="headers">httpclient对象的协议头对象</param>
        /// <returns></returns>
        private static bool Put_Headers(System.Net.Http.Headers.HttpRequestHeaders headers)
        {
            //httpRequestMessage.Headers.Add("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36");
            headers.Add("User-Agent", @"Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)");
            
            //headers.Add("X-Forwarded-Proto", @"114.114.114.114");
            //headers.Add("X-Forwarded-For", @"114.114.114.114");
            //headers.Add("X-Real-IP", @"114.114.114.114");
            return true;
        }

        /// <summary>
        /// 访问网页，获取返回值
        /// </summary>
        /// <param name="get_data">请求数据</param>
        /// <param name="timeout">超时时间，默认10秒钟</param>
        /// <returns></returns>
        public static HttpResponseMessage Get(IHttp_Get_Interface get_data,int timeout = 10*1000)
        {
            lock (Lock)
            {
                if (Count > MaxCount)
                {
                    Program.log("达到访问限制条件!");
                    Task.Delay(SleepTime).Wait();
                    Count = 0;
                }
                else
                {
                    Count++;
                }
            }


            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            Put_Headers(httpRequestMessage.Headers);
            httpRequestMessage.Method = new HttpMethod("GET");
            httpRequestMessage.RequestUri = new Uri(get_data.Url);

            Task<HttpResponseMessage> rsp = client.SendAsync(httpRequestMessage);
            if (rsp.Wait(timeout))
            {
                HttpResponseMessage RspMessage = rsp.Result;
                Program.log($"{get_data.EventType}\t获取成功\t{RspMessage.StatusCode}");
                return RspMessage;
            }
            else
            {
                Program.log($"{get_data.EventType}\t访问超时");
                return null;
            }
        }

        /// <summary>
        /// 访问网页，获取返回值
        /// </summary>
        /// <param name="get_data">请求数据</param>
        /// <param name="timeout">超时时间，默认10秒钟</param>
        /// <returns></returns>
        public static HttpResponseMessage Get(string url,string eventtype)
        {
            _Http_Get _httpget = new _Http_Get()
            {
                Url = url,
                EventType = eventtype
            };

            return Get(_httpget);
        }

        private struct _Http_Get : IHttp_Get_Interface
        {
            public string Url { get; set; }
            public string EventType { get; set; }
        }
    }
    

    interface IHttp_Get_Interface
    {
        string Url { get; set; }
        string EventType { get; set; }
    }
    interface IHttp_Post_Interface
    {
        string Url { get; set; }
        string PostData { get; set; }
    }
}
