using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Newtonsoft.Json;

using Shared;

namespace LoadTestyTester
{
    class ApiLoader
    {
        const int _numberOfAPIRequests = 10000;
        const string _urlToRuin = "https://elasticdatabasehackfunction.azurewebsites.net/api/TakeIt?code=fZrAB5N4ReXsYYjQa3JyPLJgQq7do34F9Mi0wusQ13CUc3V4KuXH5g==";
        static readonly Stopwatch _watch = new Stopwatch();
        static readonly HttpClient _client = CreateHttpClient();
        static readonly JsonSerializer _serializer = new JsonSerializer();

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
            List<Task<UserProfile>> tasks = new List<Task<UserProfile>>();
            for (var x = 0; x < _numberOfAPIRequests; x++)
                tasks.Add(GetDataObjectFromAPI<UserProfile>(_urlToRuin, x));

            await Task.WhenAll(tasks);

            WriteTime();
            Console.WriteLine($"All {_numberOfAPIRequests} Downloads Complete");
        }

        static async Task<T> GetDataObjectFromAPI<T>(string apiUrl, int downloadNumber)
        {
            Console.WriteLine($"Download {downloadNumber} Started");
            WriteTime();

            var downloadStopWatch = new Stopwatch();
            downloadStopWatch.Start();

            try
            {
                using (var stream = await _client.GetStreamAsync(apiUrl).ConfigureAwait(false))
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    if (json == null)
                        return default(T);

                    downloadStopWatch.Stop();
                    var downloadTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                downloadStopWatch.Elapsed.Hours, downloadStopWatch.Elapsed.Minutes, downloadStopWatch.Elapsed.Seconds,
                downloadStopWatch.Elapsed.Milliseconds / 10);

                    Console.WriteLine($"Download {downloadNumber} Completed in {downloadTime}");
                    WriteTime();
                    Console.WriteLine();

                    return await Task.Run(() => _serializer.Deserialize<T>(json)).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
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
                Timeout = TimeSpan.FromSeconds(6000)
			};

            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return client;
		}
    }
}