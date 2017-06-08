using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoadTestyTester
{
    class ApiLoader
    {
        static readonly string urlToRuin = "https://elasticdatabasehackfunction.azurewebsites.net/api/TakeIt?code=fZrAB5N4ReXsYYjQa3JyPLJgQq7do34F9Mi0wusQ13CUc3V4KuXH5g==";
        static Stopwatch watch = new Stopwatch();
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            try
            {
                watch.Start();

                Task.Run(() => RunData()).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            watch.Stop();
            writeTime();

            Console.ReadLine();
        }

        public static async Task RunData()
        {
            List<Task<string>> tasks = new List<Task<string>>();
            for (var x = 0; x < 10000; x++)
                tasks.Add(callHttp());

            await Task.WhenAll(tasks);
        }
        
        static public async Task<string> callHttp()
        {
            string astr = await client.GetStringAsync(urlToRuin);
            writeTime();

            return astr;
        }

        static void writeTime()
        {
            TimeSpan ts = watch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}