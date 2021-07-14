using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    /*
     * When working with asynchronous code, there is a known concept called SynchronizationContext, 
     * SynchronizationContext connects its application model to its threading model.
     * In a UI app, the SynchronizationContext ensures that all other threads in the application process, marshall back (syncs) with the main UI thread.
     * In a Web app, the marshalling (or syncing) will happen on the thread that has the client's cultural, prinicipla, and other information set (i.e. all threads sync back up to the web-app's ExecutionContext)
     * You can disable the SynchronizationContext in an async/await method simply by using ConfigureAwait(false).
     * This should improve the performance of your application, 
     * but keep in mind that if your result of an async/await process requires syncing back up to update something (like a button's label or page section) on UI's main thread or Web's ExecutionContext thread, 
     * then using the ConfigureAwait(false) will result to an exception.
     * */
    public class Listing20
    {        
        HttpClient httpClient = new HttpClient();

        public void Example()
        {
            string urlString = "http://www.twitter.com";
            Task.Run<HttpStatusCode>(() =>
            {
                var result = LoadPage(urlString).GetAwaiter().GetResult();
                return result;
            })
            .ContinueWith((t) =>
            {
                Console.WriteLine($"Status: {t.Result}");
            })
            .ConfigureAwait(false);

            Console.WriteLine($"Loading {urlString}...");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }

        private async Task<HttpStatusCode> LoadPage(string urlString)
        {
            //Method 1 read data
            HttpResponseMessage response = await httpClient.GetAsync(new Uri(urlString));
            if(response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = await response.Content.ReadAsStringAsync();
            }

            //Method 2 read data
            string data = await httpClient.GetStringAsync(new Uri(urlString));            

            return response.StatusCode;
        }
    }
}
