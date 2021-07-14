using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing09
    {
        /// <summary>
        /// A task object that can do something and return a value.
        /// </summary>
        public static void Example1()
        {
            Task<int> taskA = Task.Run<int>(() =>
            {
                return 10;
            });

            //Calling Result (just like Join, Wait, await, GetAwaiter) will block any calling thread until target thread has finished.
            Console.WriteLine("Task returned: " + taskA.Result);
        }
    }
}
