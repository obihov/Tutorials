using MSCAChapter1.ParameterizedThread;
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
            ////Thread performance example
            //ExecuteCode1();
            //PromptUserInput();

            ////Thread sleep example
            //ExecutedCode2();
            //PromptUserInput();

            //Thread sleep varying example
            //ExecuteCode3();
            //PromptUserInput();

            //Parameterized thread example
            ExecuteCode4();
        }

        private static void ExecuteCode1()
        {
            InitializeConsoleMessage(1);

            #region This Naturally Shows Performance measures for different Thread implementation approaches
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
        }

        private static void ExecutedCode2()
        {
            InitializeConsoleMessage(2);

            #region This example shows how to explictly force a thread to suspend. 
            //Thread1 suspends for 5secs to allow other threads execute for that time before it resumes again.
            Thread thread1 = new Thread(new ThreadStart(ThreadSleepingExample.Thread1));
            Thread thread2 = new Thread(new ThreadStart(ThreadSleepingExample.Thread2));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            #endregion
        }

        private static void ExecuteCode3()
        {
            InitializeConsoleMessage(3);

            #region This example shows how to explictly force a thread to suspend. 
            //Thread1 suspends for 5secs to allow other threads execute for that time before it resumes again.
            Thread thread1 = new Thread(new ThreadStart(ThreadSleepingVaryingExample.SmallDataSet));
            Thread thread2 = new Thread(new ThreadStart(ThreadSleepingVaryingExample.LargeDataSet));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            #endregion
        }

        private static void PromptUserInput()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("******************************************");
            Console.WriteLine("Press Any Key to Execute Next Code Region");
            Console.WriteLine("******************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void InitializeConsoleMessage(int codeInExecution)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("******************************************");
            Console.WriteLine($"CODE REGION {codeInExecution}");
            Console.WriteLine("******************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ExecuteCode4()
        {
            InitializeConsoleMessage(4);

            #region This example shows you how to use an overloaded method of Thread that accepts a parameter that can be passed to the executed method
            Thread thread = new Thread(new ParameterizedThreadStart(ParameterThreads.PrintHello));
            thread.Start("5");
            thread.Join();
            #endregion
        }



    }
}
