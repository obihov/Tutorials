using MSCAChapter1.ThreadPerformance;
using MSCAChapter1.ThreadSleeping;
using System;
using System.Diagnostics;
using System.Threading;

namespace MSCAChapter1
{
    public static class Program
    {
        private static Stopwatch approachTimer1 = new Stopwatch();
        private static Stopwatch approachTimer2 = new Stopwatch();
        private static Stopwatch approachTimer3 = new Stopwatch();

        static void Main(string[] args)
        {
            #region This Naturally Shows Performance measures for different Thread implementation approaches
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("******************************************");
            Console.WriteLine("CODE REGION 1");
            Console.WriteLine("******************************************");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("--Approach 1: Run Multiple Threads Simultaneously--");
            approachTimer1.Start();
            ThreadPerformanceExample.Approach1();
            approachTimer1.Stop();
            Console.WriteLine();

            Console.WriteLine("--Approach 2: Run Multiple Threads Synchronously--");
            approachTimer2.Start();
            ThreadPerformanceExample.Approach2();
            approachTimer2.Stop();
            Console.WriteLine();

            Console.WriteLine("--Approach 3: Run Single Thread having multiple methods delegated to it--");
            approachTimer3.Start();
            ThreadPerformanceExample.Approach3();
            approachTimer3.Stop();
            Console.WriteLine();

            Console.WriteLine($"Time to complete Approach 1:\t{approachTimer1.ElapsedMilliseconds}ms");
            Console.WriteLine($"Time to complete Approach 2:\t{approachTimer2.ElapsedMilliseconds}ms");
            Console.WriteLine($"Time to complete Approach 3:\t{approachTimer3.ElapsedMilliseconds}ms");
            #endregion

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******************************************");
            Console.WriteLine("Press Any Key to Execute Next Code Region");
            Console.WriteLine("******************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.WriteLine();


            #region This example shows how to explictly force a thread to suspend. 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("******************************************");
            Console.WriteLine("CODE REGION 2");
            Console.WriteLine("******************************************");
            Console.ForegroundColor = ConsoleColor.White;

            //Thread1 suspends for 5secs to allow other threads execute for that time before it resumes again.
            Thread thread1 = new Thread(new ThreadStart(ThreadSleepingExample.Thread1));
            Thread thread2 = new Thread(new ThreadStart(ThreadSleepingExample.Thread2));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            #endregion

        }




        
    }
}
