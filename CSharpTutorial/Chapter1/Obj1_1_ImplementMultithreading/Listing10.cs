using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing10
    {
        /// <summary>
        /// A task object that can do something, wait for result when ready and do more things depending on the result returned.
        /// </summary>
        public static void Example1()
        {
            Task<bool> taskA = Task.Run<bool>(() =>
            {
                return true;
            });

            taskA.ContinueWith((t) =>
            {
                if (t.Result)
                {
                    Console.WriteLine("Completed.");
                }
                else
                {
                    Console.WriteLine("Not Completed.");
                }
            }).Wait();

            //note, you can have one or more ContinueWith, but always keep a wait on them. 
            //No need to keep a wait on the original task (i.e. taskA) simply do so on any of its ContinueWith call
        }
    }
}
