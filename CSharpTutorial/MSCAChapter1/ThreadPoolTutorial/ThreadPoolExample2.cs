using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSCAChapter1.ThreadPoolTutorial
{
    class ThreadPoolExample2
    {

        public static void PerformParallelTasks()
        {
            //Below we make a single thread that handles the synchronous execution of two methods using ThreadPool - Greet and PrintDate
            WaitCallback callback = new WaitCallback(Greet);
            callback += new WaitCallback(PrintDate);
            ThreadPool.QueueUserWorkItem(callback);

            //Below we make multiple threads that handles the execution of two methods asynchronously or in parallel using ThreadPool 
            //this is a good example for spinning new threads or reusing existing threads to handle multiple requests
            WaitCallback methodsToExecute = new WaitCallback(Greet);
            methodsToExecute += new WaitCallback(PrintDate);
            foreach (WaitCallback m in methodsToExecute.GetInvocationList()) //see alternate version below that uses var and DynamicInvoke whe Delegate is unkonwn.
            {
                Console.WriteLine(m.Method.Name);
                ThreadPool.QueueUserWorkItem((s) => m(s));
            }
            foreach (var m in methodsToExecute.GetInvocationList())
            {
                Console.WriteLine(m.Method.Name);
                ThreadPool.QueueUserWorkItem((s) => m.DynamicInvoke(s));
            }


            //Main Thread - Below is like Thread.Wait() with Task, or Thread.Join() with ThreadClass, it keeps the Application alive. Once hit the application end and all background threads will end. Can prevent this if each thread you create it set to a foreground threaed.
            Console.ReadKey();
        }

        public static void Greet(object s)
        {
            Console.WriteLine("Hello Obi");
        }

        public static void PrintDate(object s)
        {
            Console.WriteLine($"The date and time is {DateTime.Now}");
        }
    }
}
