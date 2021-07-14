using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public static class Listing02
    {
        /// <summary>
        /// Example: Using a background thread without a Join or Wait statement may exit the application immediately if main ends.
        /// </summary>
        public static void Example1()
        {
            Thread newThread = new Thread(() =>
            {
                for(int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Running new thread.");
                    Thread.Sleep(2000);
                }
            });
            newThread.IsBackground = true;
            newThread.Start();

            Console.WriteLine("Main thread exiting...");
        }

        /// <summary>
        /// Example: Call the join or wait method before the application's main thread ends. This will keep the background thread alive.
        /// </summary>
        public static void Example2()
        {
            Thread newThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Running new thread.");
                    Thread.Sleep(2000);
                }
            });
            newThread.IsBackground = true;
            newThread.Start();
            newThread.Join();

            Console.WriteLine("Main thread exiting...");
        }

        /// <summary>
        /// Example: Set the IsBackground property of the thread to false, to make thread a foreground. 
        /// Foreground threads remain alive even when main-thread is completed. No need for Join and Wait statements when using a foreground thread.
        /// </summary>
        public static void Example3()
        {
            Thread newThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Running new thread.");
                    Thread.Sleep(2000);
                }
            });
            newThread.IsBackground = false;
            newThread.Start();

            Console.WriteLine("Main thread exiting...");
        }
    }
}
