using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public static class Listing1_4
    {
        /// <summary>
        /// Example: Using Thread.CurrentThread.Abort method to stop a thread. NOTE: Not a great way to stop a running thread. 
        /// Need two threads to do this - one thread captures user action and based on user action determines if the other thread continous its execution.
        /// </summary>
        public static void Example1()
        {
            //Main thread            
            Console.WriteLine("Executing Main thread");

            //New thread that runs continously
            Thread thread1 = new Thread(() =>
            {                
                while (true)
                {
                    Console.WriteLine("Executing New thread.");
                    Thread.Sleep(4000); //switches context and resumes back after 4 seconds.
                }
            });
            thread1.IsBackground = false;         
            thread1.Start();
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
            Console.WriteLine("Aborting thread.");

            //because this method is executed by another thread, it can happen at any time.
            //When it happens, a ThreadAbortException is thrown on the target threadThreadAbortException should be caught.
            thread1.Abort();    
        }

        public static void Example2()
        {
            //Main thread            
            Console.WriteLine("Executing Main thread");

            //global local variable to be used to capture user action and force all threads to stop
            var isStopped = false;

            //New thread that runs continously
            Thread thread1 = new Thread(() =>
            {
                while (isStopped is false)
                {
                    Console.WriteLine("Executing New thread.");                    
                    Thread.Sleep(4000); //switches context and resumes back after 4 seconds.
                }
            });
            thread1.IsBackground = false;
            thread1.Start();
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
            isStopped = true;
            Console.WriteLine("Aborting thread.");
            thread1.Join();
        }
    }
}
