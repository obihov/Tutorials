using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    class Listing21
    {
        HttpClient httpClient = new HttpClient();

        public void Example()
        {
            string urlString = "http://www.twitter.com";

            var data1 = string.Empty;
            var data2 = string.Empty;
            var status = HttpStatusCode.BadRequest;
            Task task1 = Task.Run(() =>
            {
                data1 = GetPageDataMethod1(urlString).GetAwaiter().GetResult();
            });
            Task task2 = Task.Run(() =>
            {
                data2 = GetPageDataMethod2(urlString).GetAwaiter().GetResult();
            });
            Task task3 = Task.Run(() =>
            {
                status = ChecksPageStatus(urlString).GetAwaiter().GetResult();
            });

            Task.WaitAll(task1, task2, task3);
            Console.WriteLine($"data1: {data1.Substring(0, 9)}");
            Console.WriteLine($"data2: {data1.Substring(0, 9)}");
            Console.WriteLine($"status: {status}");
        }

        private async Task<string> GetPageDataMethod1(string urlString)
        {
            //Method 1 read data
            HttpResponseMessage response = await httpClient.GetAsync(new Uri(urlString));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }

            return null;
        }

        private async Task<string> GetPageDataMethod2(string urlString)
        {
            //Method 2 read data
            string data = await httpClient.GetStringAsync(new Uri(urlString));
            return data;
        }

        private async Task<HttpStatusCode> ChecksPageStatus(string urlString)
        {
            //Method 1 to check status -- note I don't use await
            HttpResponseMessage response = await httpClient.GetAsync(new Uri(urlString));
            return response.StatusCode;
        }
    }
}
