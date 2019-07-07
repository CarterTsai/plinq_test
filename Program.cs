using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            var y = Enumerable.Range(1, 100);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var query = y.AsParallel()
                        .Select(async o => await callHttpPost(o))
                        .Select(t => t.Result)
                        .ToList();
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            foreach (var q in query)
            {
                Console.WriteLine(q);
            }
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        public class Args {
            public int id {get; set;}
        } 

        public static async Task<string> callHttpPost(int ids) {
            HttpClient client = new HttpClient();
            var obj = new Args {
                id = ids
            };

            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:3000", contentPost);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
