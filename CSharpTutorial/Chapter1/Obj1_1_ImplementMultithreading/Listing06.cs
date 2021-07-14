using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public static class Listing06
    {
        private static ThreadLocal<Thread> ThreadInfo = new ThreadLocal<Thread>(() =>
        {
            return Thread.CurrentThread;
        });

        private static ThreadLocal<Stopwatch> stopwatch = new ThreadLocal<Stopwatch>(() =>
        {
            return new Stopwatch();
        });

        public static void Example1()
        {
            Thread thread1 = new Thread(() =>
            {
                Console.WriteLine($"Thread1 ID - {ThreadInfo.Value.ManagedThreadId}");
            });
            thread1.IsBackground = false;
            thread1.Start();

            Thread thread2 = new Thread(() =>
            {
                Console.WriteLine($"Thread2 ID - {ThreadInfo.Value.ManagedThreadId}");
            });
            thread2.IsBackground = false;
            thread2.Start();
        }

        public static void Example2()
        {
            //Stopwatch stopwatch = new Stopwatch();

            Thread thread1 = new Thread(() =>
            {
                stopwatch.Value.Start();
                long incrementalSum = 0;
                for(int i = 1; i <= 10000; i++)
                {
                    incrementalSum += i;
                    Thread.Sleep(1);
                }
                Console.WriteLine($"Thread1 - {incrementalSum},\tElasped time - {stopwatch.Value.ElapsedMilliseconds / 1000} seconds.");
                stopwatch.Value.Stop();
            });
            thread1.IsBackground = false;
            thread1.Priority = ThreadPriority.Highest;
            thread1.Start();

            Thread thread2 = new Thread(() =>
            {
                stopwatch.Value.Start();
                long incrementalSum = 0;
                for (int i = 1; i <= 1000; i++)
                {
                    incrementalSum += i;
                    Thread.Sleep(1);
                }
                Console.WriteLine($"Thread2 - {incrementalSum},\tElasped time - {stopwatch.Value.ElapsedMilliseconds / 1000} seconds.");
                stopwatch.Value.Stop();
            });
            thread2.IsBackground = false;
            thread2.Priority = ThreadPriority.Lowest;
            thread2.Start();
        }
    }
}
