using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public static class Listing03
    {
        /// <summary>
        /// Example: Using ParameterizedThreadStart delegate to pass value to a thread's worker method through the Start method.
        /// </summary>
        public static void Example1()
        {
            //Main thread            
            Console.WriteLine("Executing Main thread");
                        
            //New thread 1
            Thread thread1 = new Thread(new ParameterizedThreadStart(WorkerMethod));
            thread1.Start(2020);
            thread1.Join();

            //New thread 2
            Thread thread2 = new Thread((o) =>
            {
                Console.WriteLine("Executing New thread 2.");
                var year = o.ToString();
                Console.WriteLine(year);
            });
            thread2.Start(2021);
            thread2.Join();

        }

        private static void WorkerMethod(object o)
        {
            Console.WriteLine("Executing New thread 1");
            var year = o.ToString();
            Console.WriteLine(year);
        }
    }
}
