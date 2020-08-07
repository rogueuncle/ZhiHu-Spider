using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;




namespace Spider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Question_Struct question_Struct = new Question_Struct("412076145", 0);
            HttpResponseMessage data = await HttpCli.Get(question_Struct);

            string ddd = await data.Content.ReadAsStringAsync();

            var x = JsonSerializer.Deserialize<json_class.Root>(ddd);
        }
    }
}
