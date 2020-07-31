using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1._1ExampleThreadClass
{
    internal static class ThreadClassExample
    {       

        static public void Run()
        {
            Example1();
            Example2();
            Example3();
            Example4();
            Example5();
        }

        //Some Methods
        static private void Greet1() => Console.WriteLine($"Hello world.");
        static private void Greet2(object name) => Console.WriteLine($"Hello {name.ToString()}.");


        /// <summary>
        /// This example just shows how to create a simple thread.
        /// </summary>
        static private void Example1()
        {
            //Version 1 - pointing the ThreadStart delegate to a named method.
            Thread thread1 = new Thread(new ThreadStart(Greet1));

            //Version 2 - pointing the ThreadStart delegate to an anonymous method.
            Thread thread2 = new Thread(new ThreadStart(() => Console.WriteLine("A simple thread.")));
        }

        /// <summary>
        /// This example shows how to run a thread.
        /// </summary>
        static private void Example2()
        {
            //Notice, Greet2 method must take an object type as parameter.
            Thread thread1 = new Thread(new ThreadStart(Greet1));
            thread1.Start();
        }

        /// <summary>
        /// This example shows how to create and run a thread that requires an input parameter. 
        /// </summary>
        static private void Example3()
        {
            //Notice, Greet2 method must take an object type as parameter.
            Thread thread1 = new Thread(new ParameterizedThreadStart(Greet2));
            thread1.Start("Obinna");
        }

        /// <summary>
        /// Create, run and wait for a thread to finish. Waiting blocks other thread like main-thread and/or any other thread to be processed.
        /// </summary>
        static private void Example4()
        {
            Thread thread1 = new Thread(new ThreadStart(Greet1));
            thread1.Start();

            //Below like forces main-thread and other threads to be blocked (i.e. not able to execute at this point)
            thread1.Join();

            //Main-thread.
            Console.WriteLine("Executing the main-thread now...");
        }

        /// <summary>
        /// Create, run and wait for multiple threads to finish. Waiting blocks other thread like main-thread and/or any other thread to be processed.
        /// </summary>
        static private void Example5()
        {
            Thread thread1 = new Thread(new ThreadStart(Greet1));            
            Thread thread2 = new Thread(new ParameterizedThreadStart(Greet2));

            //Both thread1 and thread2 runs in parallel. 
            thread1.Start();
            thread2.Start("jacob");

            //Await both thread1 and thread2 till they're completed. Main thread and other thread is blocked (i.e. not able to execute at this time while being blocked).
            thread1.Join();

            //Main-thread
            Console.WriteLine("Executing the main-thread now...");

            thread2.Join();

            //Main-thread
            Console.WriteLine("Executing the main-thread now...");
        }
    }





}
