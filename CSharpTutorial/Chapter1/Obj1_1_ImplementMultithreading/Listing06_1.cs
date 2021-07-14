using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing06_1
    {
        
        /// <summary>
        /// Different context for the current executing thread.
        /// </summary>
        public static void Example()
        {
            Thread threadContext = Thread.CurrentThread; //get
            CultureInfo culturalInfo = Thread.CurrentThread.CurrentCulture; //get, set
            IPrincipal securityContext = Thread.CurrentPrincipal; //get, set
            //ThreadPriority priority = ThreadPriority.Normal;            
        }

        /// <summary>
        /// This example is focused on the use of the ExecutionContext.SuppressFlow() method.
        /// This method offers the ability to prevent the flow of a thread's context into that of its child.
        /// For example, the thread context (i.e. Thread.CurrentThread) of Main, can be prevented from flowing into t or any other thread initiated off it
        /// using the ExecutionContext.SuppressFlow() method.
        /// Doing this can help improve performance by eliminating the overhead when new threads are created, which by default flows data from the old thread to the new.
        /// </summary>
        public static void Example2()
        {
            Task<int>[] tasks = new Task<int>[2];
            
            //Below task is child of the main thread.
            var t = Task.Run(() =>
            {
                //thread context data flow from one thread to another thread (usually between parent and child thread) can be costly on the CPU and memory.
                //this will help suppress the flow of the thread context data from the main thread to the current thread.
                ExecutionContext.SuppressFlow();

                Console.WriteLine("Child thread of the main-thread executing...");

                //Note, we can call the ExecutionContext.SuppressFlow() in below child thread of the t thread to prevent flow of its context data into its child threads.
                Task<int> task1 = new Task<int>(() => 10, TaskCreationOptions.AttachedToParent);    //child thread of t.
                task1.Start();
                tasks[0] = task1;
                Task<int> task2 = new Task<int>(() => 200, TaskCreationOptions.AttachedToParent);   //child thread of t
                task2.Start();
                tasks[1] = task2;
                
                //here we just process each child task of t and print their values to the console.
                while(tasks.Length > 0)
                {
                    var completedTask = Task.WaitAny(tasks);
                    var returnValue = tasks[completedTask].Result;
                    Console.WriteLine("Final result: " + returnValue);

                    var copy = tasks.ToList();
                    copy.RemoveAt(completedTask);
                    tasks = copy.ToArray();
                }
            });
            t.Wait();

            Console.WriteLine("Main thread executing...");
            
        }
    }
}
