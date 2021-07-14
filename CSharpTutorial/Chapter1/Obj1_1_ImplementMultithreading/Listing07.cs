using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing07
    {
        private static void DoSomething1(object s) => Console.WriteLine("Executed Method 1. Data is {s}");


        /// <summary>
        /// Queueing a method in ThreadPool without passing argument.
        /// </summary>
        public static void Example1()
        {            
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoSomething1));
            ThreadPool.QueueUserWorkItem(DoSomething1);
            ThreadPool.QueueUserWorkItem((s) => Console.WriteLine("Executed Method 3."));            
        }

        /// <summary>
        /// Queueing a method in ThreadPool and passing argument.
        /// </summary>
        public static void Example2()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoSomething1), 10);   //passed 10 as args
            ThreadPool.QueueUserWorkItem(DoSomething1, 20); //passed 20 as args
            ThreadPool.QueueUserWorkItem((s) => Console.WriteLine("Executed Method 3."), 30); //passed 30 as args
        }

        [ThreadStatic]
        private static int SomeField = 0;

        /// <summary>
        /// Using a ThreadStatic field to capture unique copy of data in a same-shared variable between different ThreadPools.
        /// </summary>
        public static void Example3()
        {
            //Thread A
            ThreadPool.QueueUserWorkItem((s) =>
            {
                while(SomeField < 10)
                {
                    SomeField++;    //at 0 the variable will be 1, so at 9 the variable will be 10 etc.
                }
                Console.WriteLine($"Thread A - {SomeField}");   //prints 10
            });

            //Thread B
            ThreadPool.QueueUserWorkItem((s) =>
            {
                while (SomeField < 20)
                {
                    SomeField++;    //at 0 the variable will be 1, so at 19 the variable will be 20 etc.
                }
                Console.WriteLine($"Thread B - {SomeField}");   //prints 20
            });

            //Thread Main
            Console.WriteLine($"Thread Main - {SomeField}");    //prints 0
        }
                

        private static ThreadLocal<int> LocalField = new ThreadLocal<int>(() => 0);

        /// <summary>
        /// Using a ThreadLocal field to capture unique copy of data in a same-shared variable between different ThreadPools.
        /// </summary>
        public static void Example4()
        {
            //Thread A
            ThreadPool.QueueUserWorkItem((stateInfo) =>
            {
                var limit = (int)stateInfo; //stateinfo represents an object that the queued method can use.
                while (LocalField.Value < limit)
                {
                    LocalField.Value++;
                }
                Console.WriteLine($"Thread A - {LocalField.Value}");
            }, 10);

            //Thread B
            ThreadPool.QueueUserWorkItem((stateInfo) =>
            {
                while (LocalField.Value < 20)
                {
                    LocalField.Value++;
                }
                Console.WriteLine($"Thread B - {LocalField.Value}");
            });

            //Thread Main
            Console.WriteLine($"Thread Main - {LocalField.Value}");
        }

    }
}
