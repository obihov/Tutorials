using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCAChapter1.ThreadPoolTutorial
{
    /// <summary>
    /// This example shows you how to write a multithreaded application using ThreadPool.
    /// Threadpool makes application responsive. 
    /// Threads are created on the fly or reused from the pool, thus always keeping the application responsive.
    /// Threadpool does not help much with scalability. 
    /// If all threads are busy, it just keeps on auto-creating new ones when more requests comes in and could max out all the threads available to process a task/workitem. This is likely to happen when incoming requests never stops processing or never times out on time to release the thread.
    /// Due to the limited number of threads in the pool, Threadpool may not be considered very good for parallelism. 
    /// Unlike ThreadPool, the Thread class have no limit on the maximum number of threads it can spin off and so has a better edge on parallelism (running multiple tasks at once)
    /// </summary>
    class ThreadPoolExample
    {
        private static ThreadLocal<string> _field = 
            new ThreadLocal<string>(() => Thread.CurrentThread.Name, true);
        private static ThreadLocal<ThreadInstance> _threadInstance = 
            new ThreadLocal<ThreadInstance>(() => 
            {
                return new ThreadInstance();
            });


        public static void PerformParallelTasks()
        {
            //Main thread kickoff Child Thread 1
            ThreadPool.QueueUserWorkItem((s) =>
            {
                _threadInstance.Value.CurrentThreadInfo.Name = "Download Thread";
                _threadInstance.Value.CurrentThreadInfo.IsBackground = false;           //main app don't close when foreground threads are still running     
                Download(_threadInstance.Value.FileName = "FileA0001.txt");
            });

            //Main thread kickoff Child Thread 2
            ThreadPool.QueueUserWorkItem(_ =>
            {
                _threadInstance.Value.CurrentThreadInfo.Name = "Email Thread";
                _threadInstance.Value.CurrentThreadInfo.IsBackground = false;           //main app don't close when foreground threads are still running 
                SendEmail(_threadInstance.Value.Receipient = "Obi");
            });

            ThreadPool.QueueUserWorkItem(CloseMessage); //or can do  WaitCallback callback = new WaitCallback(CloseMessage); and supply callback as the argument.

            //Main Thread
            _threadInstance.Value.CurrentThreadInfo.Name = "Main Thread";
            Console.WriteLine($"Main Thread Info: " + _threadInstance.Value.CurrentThreadInfo.Name);
        }

        private static void Download(string file)
        {
            Console.WriteLine("Downloading File {0}...", file);
        }

        private static void SendEmail(string receipient)
        {
            Console.WriteLine("Sending Email to {0}...", receipient);
        }

        private static void CloseMessage(object s)
        {
            Console.WriteLine("Good bye!");
        }

        private class ThreadInstance
        {
            //made it a getter only. Each thread should get its current thread. Do not set it, but can modify its properties like name, background, priority etc in each thread
            public Thread CurrentThreadInfo => Thread.CurrentThread;

            //below properties can be both getter and setters (i.e. accessor and mutator methods). Each thread by default gets the "Not Specified" message unless the change it.
            public string FileName { get; set; } = "Not Specified";
            public string Receipient { get; set; } = "Not Specified";
        }
    }

    
}
