using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    class HttpCli
    {
        public static async Task<HttpResponseMessage> Get(IHttp_Get_Interface question_data)
        {

            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36");
            httpRequestMessage.Method = new HttpMethod("GET");
            httpRequestMessage.RequestUri = new Uri(question_data.Url);

            HttpResponseMessage rsp = await client.SendAsync(httpRequestMessage);
            
            //HttpResponseMessage rsp = await client.GetAsync(question_data.Url);
            Console.WriteLine(rsp.StatusCode);
            //var data = await rsp.Content.ReadAsStringAsync();
            //Console.WriteLine(data);
            return rsp;
        }
    }

    interface IHttp_Get_Interface
    {
        string Url { get; set; }
    }
    interface IHttp_Post_Interface
    {
        string Url { get; set; }
        string PostData { get; set; }
    }
}
