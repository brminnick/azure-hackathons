using System;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace LoadTestyTester
{
    class ApiLoader
    {
        static readonly string _urlToRuin = "https://elasticdatabasehackfunction.azurewebsites.net/api/TakeIt?code=fZrAB5N4ReXsYYjQa3JyPLJgQq7do34F9Mi0wusQ13CUc3V4KuXH5g==";
        static readonly Stopwatch _watch = new Stopwatch();
        static readonly HttpClient _client = CreateHttpClient();

        static void Main(string[] args)
        {
            try
            {
                _watch.Start();

                Task.Run(() => RunData()).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            _watch.Stop();
            WriteTime();

            Console.ReadLine();
        }

        public static async Task RunData()
        {
            List<Task<string>> tasks = new List<Task<string>>();
            for (var x = 0; x < 10000; x++)
                tasks.Add(CallHttp());

            await Task.WhenAll(tasks);
        }
        
        static public async Task<string> CallHttp()
        {
            string astr = await _client.GetStringAsync(_urlToRuin);
            WriteTime();

            return astr;
        }

        static void WriteTime()
        {
            TimeSpan ts = _watch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

		static HttpClient CreateHttpClient()
		{
			var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip })
			{
				Timeout = TimeSpan.FromSeconds(60)
			};

			client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

			return client;
		}
    }
}