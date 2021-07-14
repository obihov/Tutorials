using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing11
    {
        /// <summary>
        /// A task object using one or more ContinueWith methods to process a task when it's ready for further processing
        /// </summary>
        public static void Example1()
        {
            Task<bool> taskA = Task.Run<bool>(() =>
            {
                
                Console.WriteLine("Please select a key from the keyboard other than the Spacebar key.");

                try
                {
                    var userInput = Console.ReadKey().Key;
                    if (userInput == ConsoleKey.Spacebar)
                    {
                        throw new Exception("Wrong key entered. Do not select the Spacebar key.");
                    }
                }
                catch
                {
                    throw;
                }

                Thread.Sleep(2000); //simulates a delay
                return true;

            });

            //continues when taskA halt due to being canceled 
            taskA.ContinueWith((o) =>
            {
                Console.WriteLine("Operation was canceled.");
            }, TaskContinuationOptions.OnlyOnCanceled).Wait();

            //continues when taskA halt due to an exception 
            taskA.ContinueWith((o) =>
            {
                Console.WriteLine("Exception occured." + o.Exception.Message);
            }, TaskContinuationOptions.OnlyOnFaulted).Wait();

            //continues when taskA is ran to completion 
            taskA.ContinueWith((o) =>
            {
                Console.WriteLine("Successful.");
            }, TaskContinuationOptions.OnlyOnRanToCompletion).Wait();

            //note, you can have one or more ContinueWith, but always keep a wait on them. 
            //No need to keep a wait on the original task (i.e. taskA) simply do so on any of its ContinueWith call
        }
    }
}
