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
            Question_Struct question_Struct = new Question_Struct("29843358", 0);

            //Question.question_queue.Enqueue(question_Struct);
            Question.question_queue.Add(question_Struct);
            await Question.Run(true);
            
        }
    }
}
