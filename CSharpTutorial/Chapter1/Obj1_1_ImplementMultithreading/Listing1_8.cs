using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing1_8
    {
        /// <summary>
        /// This example shows to perform a multithreaded operation that does not return a value using the Task class.
        /// </summary>
        public static void Example1()
        {
            Task t = Task.Run(() =>
            {
                var i = 0;
                do
                {
                    Console.WriteLine("Running operations on another thread.");
                    i++;
                    Thread.Sleep(2000);
                } while (i < 10);
            });
            t.Wait();   //like Thread.Join() blocks main/calling thread. Even though you coded a thread.sleep with your task, the Wait blocks any context switching to another thread until current thread has finished running.
            Console.WriteLine("Running operations on Main thread.");
        }
        
        /// <summary>
        /// This example shows to perform a multithreaded operation that returns a value using the Task class.
        /// </summary>
        public static void Example2()
        {
            Task<int> t = Task<int>.Run(() =>
            {
                var age = 50;
                return age;
            });
            Console.WriteLine("Child task returned: " + t.Result);  //calling t.Result is like Join, Wait, GetAwaiter, await. It blocks the current thread, meaning nothing can be executed on the current thread until operation from running thread has finished.
            Console.WriteLine("Running operations on Main thread");
        }

        /// <summary>
        /// This example shows how to use the ContinueWith method to optimize a Task such that reading return value from a task operation doesn't block current threads as would when using the Result property to read returned values.
        /// </summary>
        public static void Example3()
        {
            Task<int> t = Task<int>.Run(() =>
            {
                var age = 90;
                Thread.Sleep(5000); //simulate a long processing operation.
                return age;
            });

            //ContinueWith will prevent other thread (in this example the main thread) from blocking when trying to read the result of a task.
            t.ContinueWith(r =>
            {
                Console.WriteLine("Task returned: " + r.Result);
            }).Wait();
            


            //Consider calling a Wait before exiting out of the main thread. This way running task gets more time to complete before program ends.
            //You may consider using a ThreadLocal or ThreadStatic to capture the CurrentThread background state as foreground, which too would help keep the thread for executing the task alive.
            Console.WriteLine("Running operations on Main thread");
        }

        /// <summary>
        /// This example shows how to combine multiple ContinueWith methods to read data or do something else with the returned value of a task.
        /// </summary>
        public static void Example4()
        {
            Task<int> t = Task<int>.Run(() =>
            {
                var age = 90;
                Thread.Sleep(5000); //simulate a long processing operation.
                return age;
            });

            //combine the ContinueWith method.
            var completedTask = t.ContinueWith<bool>(r =>
            {
                return r.Result < 100;
            });

            completedTask.ContinueWith(r =>
            {
                if (r.Result)
                {
                    Console.WriteLine("Younger than 100.");
                }
                else
                {
                    Console.WriteLine("Older than 100.");
                }
            });

            completedTask.Wait();
            Console.WriteLine("Running operations on Main thread");
        }

    }
}