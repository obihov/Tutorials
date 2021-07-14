using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter1.Obj1_1_ImplementMultithreading
{
    public class Listing12
    {
        /// <summary>
        /// A task object with Children tasks
        /// </summary>
        public static void Example1()
        {

            //Below tasks starts immediately, however will not complete until after all of its child tasks have either ran to completion/faulted or cannceld.
            Task<List<string>> parentTask = Task.Run<List<string>>(() =>
            {
                List<string> fruits = new List<string>();

                Task child1 = new Task(() =>
                {
                    fruits.Add("Mango");
                    Thread.Sleep(2000);
                }, TaskCreationOptions.AttachedToParent);
                child1.Start();

                Task child2 = new Task(() =>
                {
                    fruits.Add("Apple");
                    Thread.Sleep(1200);
                }, TaskCreationOptions.AttachedToParent);
                child2.Start();

                Task child3 = new Task(() =>
                {
                    fruits.Add("Banana");
                    Thread.Sleep(800);
                }, TaskCreationOptions.AttachedToParent);
                child3.Start();

                //You can choose to wait on all child task complete, but if you have a continuation on the parent task, then there's no need for the below code.
                //Note a continuation is attached to the parent task, then you must wait on the continuation task to allow time for all the parent's child task to complete.
                //Task.WaitAll(child1, child2, child3);                
                return fruits;
            });

            var completedTask = parentTask.ContinueWith((pTask) =>
            {
                var fruits = pTask.Result;
                foreach (var fruit in fruits)
                {
                    Console.WriteLine("Name: " + fruit);
                }
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            //waits for parent task and all its associated child task (if attachedToParent) to complete.
            completedTask.Wait();

            Console.WriteLine("End...");
        }
    }
}
