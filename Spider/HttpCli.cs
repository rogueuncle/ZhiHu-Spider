using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    class HttpCli<T>
    {
        public static async void Get(T data)
        {
            if(data is Question_Struct question_data)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage rsp = await client.GetAsync(question_data.url);
                rsp.StatusCode
                
            }
        }
    }
}
