using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{    
    public class Listing19
    {
        ThreadLocal<int> ThreadLocal = new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);

        public void Example1()
        {
            //As a golden-rule, keep an async method to a given task and a given task to a given non-task method.
            //So (async method to await) -> (task method to execute) -> (non-task method)
            DownloadAsync().GetAwaiter();
            UploadAsync().GetAwaiter();

            Console.WriteLine("Press any key to close application.");
            Console.ReadKey();
        }

        /// <summary>
        /// Represents a long-running I/O task operation that requires waiting for it to complete.
        /// </summary>
        /// <returns></returns>
        public async Task DownloadAsync()
        {
            Console.WriteLine("Starting download...");
            var isComplete = await DownloadTask();
            if (isComplete)
            {
                Console.WriteLine("Download is complete.");
            }
        }

        /// <summary>
        /// Represents a long-running I/O task operation that requires waiting for it to complete.
        /// </summary>
        /// <returns></returns>
        public async Task UploadAsync()
        {
            Console.WriteLine("Starting upload...");
            var isComplete = await UploadTask();
            if (isComplete)
            {
                Console.WriteLine("Upload is complete.");
            }
        }

        /// <summary>
        /// Represents a long-running I/O task operation. Should be awaited (auto-continuation) to avoid blocking thread.
        /// </summary>
        /// <returns></returns>
        public Task<bool> DownloadTask()
        {
            var task = Task.Run<bool>(() =>
            {
                Download();
                return true;
            });
            return task;
        }

        /// <summary>
        /// Represents a long-running I/O task operation. Should be awaited (auto-continuation) to avoid blocking thread.
        /// </summary>
        /// <returns></returns>
        public Task<bool> UploadTask()
        {
            var task = Task.Run<bool>(() =>
            {
                Upload();
                return true;
            });
            return task;
        }

        public void Download()
        {
            Thread.Sleep(5000);
        }

        public void Upload()
        {
            Thread.Sleep(10000);
        }
    }
}
