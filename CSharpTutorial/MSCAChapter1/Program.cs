using MSCAChapter1.BackgroundForegroundThread;
using MSCAChapter1.ParameterizedThread;
using MSCAChapter1.StoppingThread;
using MSCAChapter1.TaskTutorial;
using MSCAChapter1.ThreadPerformance;
using MSCAChapter1.ThreadPoolTutorial;
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

        [ThreadStatic]
        private static string PersonName;

        private static ThreadLocal<string> field = new ThreadLocal<string>(() => Thread.CurrentThread.Name, true);

        static void Main(string[] args)
        {
            //Thread performance example
            ExecuteCode1();
            PromptUserInput();

            //Thread sleep example
            ExecutedCode2();
            PromptUserInput();

            //Thread sleep varying example
            ExecuteCode3();
            PromptUserInput();

            //Parameterized thread example
            ExecuteCode4();
            PromptUserInput();

            //Foreground thread example
            ExecuteCode6();
            PromptUserInput();

            //Stopping a Thread using Thread.Abort method. Avoid this approach.Use shared variable instead.
            ExecuteCode7();
            PromptUserInput();

            //Stopping a Thread using shared variable. Best approach to use to stop a thread while it's running.
            ExecuteCode8();
            PromptUserInput();

            //Uniquely associating a variable to different threads using ThreadStatic attribute
            ExecuteCode9();
            PromptUserInput();

            //Using ThreadLocal to get and set values uniquely for each thread (including main thread)
            ExecuteCode10();
            PromptUserInput();

            //Using ThreadPool to spin off new threads or use existing threads from the pool to execute a task/workitem.
            ExecuteCode11();
            PromptUserInput();

            //Using Task class to spin off new threads
            ExecuteCode12();
            PromptUserInput();

            //Background thread example
            /** NOTE for running below code:
             * This code will print out all messages in childthread, which is not intended for this example.
             * This happens only when you have other lines of code in the main thread running like below codes. 
             * The Solution is to place ExecuteCode5(); call at the very bottom of the main method. Make sure no other loc is executed after that call.
             * */
            ExecuteCode5();
        }

        private static void ExecuteCode12()
        {
            //ExecuteTask1 and ExecuteTask2 shows how tasks can be created and run
            TaskExample1.ExecuteTask1();
            TaskExample1.ExecuteTask2();
            TaskExample1.ExecuteTask3();
            TaskExample1.ExecuteTask4();
            TaskExample1.ExecuteTask5();
            TaskExample1.ExecuteTask6();
            TaskExample1.ExecuteTask7().Wait();
            //ExecuteTask3 shows how to get the result of a task 

        }

        private static void ExecuteCode11()
        {
            ThreadPoolExample.PerformParallelTasks();
        }

        private static void ExecuteCode10()
        {
            //with the ThreadLocal variable, we can set a value that would relate to the Thread.CurrentThread.Name returned in the field variable

            //set current thread name's value for main thread
            field.Value = "Main";

            Thread thread1 = new Thread(new ThreadStart(
                    () =>
                    {
                        field.Value = "Thread 1";           //set current thread name's value for thread1
                        var thread1Context = field.Value;
                    }
                ));
            thread1.Start();
            //thread1.Name = "Thread 1";
            thread1.Join();

            Thread thread2 = new Thread(new ThreadStart(
                    () =>
                    {
                        field.Value = "Thread 2";           //set current thread name's value for thread2
                        var thread2Context = field.Value;
                    }
                ));
            thread2.Start();
            //thread2.Name = "Thread 2";
            thread2.Join();

            Console.ReadKey();
        }

        private static void ExecuteCode9()
        {
            InitializeConsoleMessage(9);

            PersonName = "Unassigned";

            /*
             * The example below show that a class field decorated as ThreadStatic can be used by different threads, as well as, the main thread to uniquely intialize variable to the field.
             * The field will have unique value for each thread. Altering the field in one thread will not affect its use in another thread.
             * */

            Console.WriteLine($"Main thread prints person name as {PersonName}.");
            Thread t1 = new Thread(new ParameterizedThreadStart(
                    (p) =>
                    {
                        PersonName = p.ToString();
                        Console.WriteLine($"{Thread.CurrentThread.Name} prints person name as {PersonName}...");
                    }
                ));
            t1.Name = "Thread 1";
            t1.Start("Obi");
            t1.Join();

            Console.WriteLine($"Main thread prints person name as {PersonName}.");
            Thread t2 = new Thread(new ParameterizedThreadStart(
                    (p) =>
                    {
                        PersonName = p.ToString();
                        Console.WriteLine($"{Thread.CurrentThread.Name} prints person name as {PersonName}...");
                    }
                ));
            t2.Name = "Thread 2";
            t2.Start("Sona");
            t2.Join();
        }

        private static void ExecuteCode8()
        {
            InitializeConsoleMessage(8);

            //a shared variable accessed by both calling (main) and target (child) thread. 
            //Can be used to keep a target thread running, and can be used by main thread to stop a running a target thread without throwing an exception, that
            //typically could happen if a Thread.Abort() call was used instead in the main thread to end a running child thread.
            bool stopped = false;

            //avoid using a ParameterizedThreadStart delegate approach because the shared variable you will pass as an object to a method will never get changed after 
            //the user presses a key. The method in the ParameterizedThreadStart delegate will not update the state of the shared variable because 
            //it's not in the scope of the main thread.
            Thread thread = new Thread(new ThreadStart(
                    () =>
                    {
                        var counter = 1;
                        while (!stopped)
                        {
                            ThreadStop.Run(counter++);
                        }
                    }
                ));
            thread.Name = "AbortThreadExample2";
            thread.Start();
            Console.WriteLine("Press any key to end running thread.");

            //cpu will always switch between the child thread and main thread. When it switches to main thread, it listens for a user input.
            //when it switches back to child thread, it checks the while loop before executing.
            //so if at the time the cpu is switched to the main thread and the user presses a key, it will set the stopped variable to true, which
            //when it switches back to the child thread, the while loop will evaluate to false and the embedded code is not executed.
            Console.ReadKey();

            //Main thread setting the stopped variable to true.
            stopped = true; 
            Thread.Sleep(1000);
            Console.WriteLine("Main thread executing...");
        }

        

        private static void ExecuteCode7()
        {
            InitializeConsoleMessage(7);

            Thread thread = new Thread(new ThreadStart(
                    () =>
                    {
                        var counter = 1;
                        ThreadStop.Run(counter++);
                    }
                ));
            thread.Name = "AbortThreadExample1";
            thread.Start();

            thread.Abort();
            Console.WriteLine("Main thread executing...");
        }

        private static void ExecuteCode6()
        {
            InitializeConsoleMessage(6);

            #region Example shows a foreground thread. No need to use thread.Join to keep alive while running a foreground thread. Automatically apps don't exit while a foreground thread is actively running.
            Thread thread = new Thread(new ThreadStart(BackgroundForeground.SomeMethodToRun));
            thread.IsBackground = false;
            thread.Name = "A foreground thread";
            thread.Start();


            //Note without a thread.join, the below (line of code) loc will execute in the main thread.
            //This could very well happen before any loc in the child thread executes.
            //If the child thread was a background thread, then the app will exit after it has executed the below loc in its main thread.
            //But in a child thread that is a foreground thread, then the app will automatically wait for all loc in the child thread to execute even though it may have already executed the loc in its main thread.
            //Use a Join, to force main thread from executing any of its loc until child thread has finished. This is useful in background threads.
            //thread.Join();
            Console.WriteLine("Main thread executing...");
            #endregion
        }

        private static void ExecuteCode5()
        {
            InitializeConsoleMessage(5);

            #region Example shows a background thread. To keep app alive while a background thread is running, you must use a thread.Join statement.
            Thread thread = new Thread(new ThreadStart(BackgroundForeground.SomeMethodToRun));
            thread.IsBackground = true;
            thread.Name = "A background thread";
            thread.Start();

            //Note without a thread.join, the below (line of code) loc will execute in the main thread.
            //This could very well happen before any loc in the child thread executes.
            //If the child thread was a background thread, then the app will exit after it has executed the below loc in its main thread.
            //But in a child thread that is a foreground thread, then the app will automatically wait for all loc in the child thread to execute even though it may have already executed the loc in its main thread.
            //Use a Join, to force main thread from executing any of its loc until child thread has finished. This is useful in background threads.
            //thread.Join();
            Console.WriteLine("Main thread executing...");
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
