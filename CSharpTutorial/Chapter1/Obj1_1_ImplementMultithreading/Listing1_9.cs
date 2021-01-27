using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing1_9
    {
        /// <summary>
        /// You may use the generic Task<returnType> task object to return value types, reference types (including collections)
        /// </summary>
        public static void Example1()
        {
            Task<int> completedTaskInt = Task.Run<int>(() => 400);
            completedTaskInt.Wait();

            Task<string[]> completedTaskString = Task.Run<string[]>(() =>
            {
                return new string[3] { "BMW", "Honda", "Mercedes" };
            });
            completedTaskString.Wait();
        }

        /// <summary>
        /// Like the Join, call the Wait method on a running task to block other threads from executing until actively running thread has complete its execution of the task.
        /// </summary>
        public static void Example2()
        {
            
            Task<int> completedTaskInt = Task.Run<int>(() =>
            {
                int finalAggregate = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    finalAggregate += 5;
                    Console.WriteLine(finalAggregate);
                }
                return finalAggregate;
            });
            completedTaskInt.Wait();
            Console.WriteLine(completedTaskInt.Result);
        }

        /// <summary>
        /// Calling the result property without using a Wait intially will block other threads from executing until actively running thread has complete its execution of the task.
        /// </summary>
        public static void Example3()
        {
            Task<int> completedTaskInt = Task.Run<int>(() => 400);
            Console.WriteLine("Result of task: " + completedTaskInt.Result);
        }
    }
}
